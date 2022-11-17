using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectMenu : MonoBehaviour
{
    public GameObject[] menu1, menu2;
    int ply1, ply2;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 5;
        ply1 = ply2 = 1;
        menu1[0].SetActive(true);
        menu2[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.A))
        {
            menu1[ply1 - 1].SetActive(false);
            if (ply1 == 1)
            {
                ply1 = 4;
            }
            else
                ply1--;
            menu1[ply1 - 1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            menu1[ply1 - 1].SetActive(false);
            if (ply1 == 4)
            {
                ply1 = 1;
            }
            else
                ply1++;
            menu1[ply1 - 1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            menu2[ply2 - 1].SetActive(false);
            if (ply2 == 1)
            {
                ply2 = 4;
            }
            else
                ply2--;
            menu2[ply2 - 1].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            menu2[ply2 - 1].SetActive(false);
            if (ply2 == 4)
            {
                ply2 = 1;
            }
            else
                ply2++;
            menu2[ply2 - 1].SetActive(true);
        }
        if(timer<=0)
        {
            PlayerPrefs.SetInt("Player1", ply1);
            PlayerPrefs.SetInt("Player2", ply2);
            Application.LoadLevel("SampleScene");
        }
    }
}
