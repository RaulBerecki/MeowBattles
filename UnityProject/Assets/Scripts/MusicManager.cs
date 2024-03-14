using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;
    [SerializeField] AudioClip[] musics;
    AudioSource audioSource;
    int musicLeft;
    int[] musicList,musicRemaining;
    
    // Start is called before the first frame update
    void Start()
    {
        musicList = new int[3];
        musicRemaining = new int[musics.Length];
        InitMusicList();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (!audioSource.isPlaying || Input.GetKeyDown(KeyCode.M))
        {
            PlayNextSong();
        }
    }
    void PlayNextSong()
    {
        for(int i=0;i<musicList.Length-1;i++)
        musicList[i] = musicList[i+1];
        if (musicList[0] == -1)
            InitMusicList();
        else{ 
            if (musicLeft > 0)
            {
                int musicIndex = Random.RandomRange(0, musicRemaining.Length);
                while (musicRemaining[musicIndex] == 0)
                    musicIndex = Random.RandomRange(0, musicRemaining.Length);
                musicList[1] = musicIndex;
                musicLeft--;
                musicRemaining[musicIndex] = 0;
            }
            audioSource.clip = musics[musicList[0]];
            audioSource.Play(0);
        }
    }
    void InitMusicList()
    {
        for (int i = 0; i < musics.Length; i++)
            musicRemaining[i] = 1;
        int index = 0;
        musicList[2] = -1;
        while (index < 2)
        {
            int musicIndex = Random.RandomRange(0, musicRemaining.Length);
            while (musicRemaining[musicIndex] == 0)
                musicIndex = Random.RandomRange(0, musicRemaining.Length);
            musicList[index] = musicIndex;
            musicRemaining[musicIndex] = 0;
            index++;
        }
        musicLeft = musicRemaining.Length-2;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musics[musicList[0]];
        audioSource.Play(0);
    }
}
