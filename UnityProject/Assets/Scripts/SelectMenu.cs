using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SelectMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timeLeftText;
    [SerializeField] Animator player1, player2;
    [SerializeField] string[] characters;
    [SerializeField] Animator NextSceneAnimator;
    public bool start = false;
    int ply1,ply2;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 5;
        ply1 = 1;
        ply2 = 1;
        StartCoroutine("IntroWaiting");
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
            timer -= Time.deltaTime;
        timeLeftText.text = "You start in " + (int)timer;
        if(Input.GetKeyDown(KeyCode.A))
        {
            if (ply1 == 1)
            {
                ply1 = 4;
            }
            else
                ply1--;
            player1.Play(characters[ply1 - 1]);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (ply1 == 4)
            {
                ply1 = 1;
            }
            else
                ply1++;
            player1.Play(characters[ply1 - 1]);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (ply2 == 1)
            {
                ply2 = 4;
            }
            else
                ply2--;
            player2.Play(characters[ply2 - 1]);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (ply2 == 4)
            {
                ply2 = 1;
            }
            else
                ply2++;
            player2.Play(characters[ply2 - 1]);
        }
        if (timer<=0)
        {
            StartCoroutine("OutroWaiting");
            start = true;
            timer = 1;
        }
    }
    public IEnumerator IntroWaiting()
    {
        yield return new WaitForSeconds(1.2f);
        start = true;
    }
    public IEnumerator OutroWaiting()
    {
        NextSceneAnimator.Play("NextScene");
        yield return new WaitForSeconds(1.2f);
        PlayerPrefs.SetInt("Player1", ply1);
        PlayerPrefs.SetInt("Player2", ply2);
        Application.LoadLevel("SampleScene");
    }
}
