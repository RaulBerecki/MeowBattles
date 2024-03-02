using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject nextSceneImage;
    [SerializeField] GameObject[] panels;
    [SerializeField] Slider slider;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("music").GetComponent<AudioSource>();
        slider.value = audioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = slider.value;
    }
    public void Play()
    {
        Debug.Log("Play");
        StartCoroutine("PlayScene");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public IEnumerator PlayScene()
    {
        Debug.Log("StartCourotine");
        nextSceneImage.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        Application.LoadLevel("SelectScene");
    }
    public void Settings()
    {
        panels[0].SetActive(false);
        panels[1].SetActive(true);
    }
    public void BackToMainMenu()
    {
        panels[1].SetActive(false);
        panels[0].SetActive(true);
    }
}
