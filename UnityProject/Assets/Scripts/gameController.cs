using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameController : MonoBehaviour
{
    [SerializeField] Sprite[] avatars;
    [SerializeField] Image[] inGameAvatars,fills;
    public GameObject[] characters,players;
    [SerializeField] Transform spawn1, spawn2;
    CameraController camC;
    public bool finished;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        finished = false;
        timer = 2f;
        camC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        players = new GameObject[2];
        inGameAvatars[0].sprite = avatars[PlayerPrefs.GetInt("Player1") - 1];
        inGameAvatars[1].sprite = avatars[PlayerPrefs.GetInt("Player2") - 1];
        players[0]=Instantiate(characters[PlayerPrefs.GetInt("Player1") - 1], spawn1.position, transform.rotation);
        players[1]=Instantiate(characters[PlayerPrefs.GetInt("Player2") - 1], spawn2.position, transform.rotation);
        camC.target1 = players[0].transform;
        camC.target2 = players[1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (finished)
            timer -= Time.deltaTime;
        if (timer <= 0)
            Application.LoadLevel("SelectScene");
        fills[0].fillAmount = players[0].GetComponent<Health>().health/100f;
        fills[1].fillAmount = players[1].GetComponent<Health>().health/100f;
    }
}
