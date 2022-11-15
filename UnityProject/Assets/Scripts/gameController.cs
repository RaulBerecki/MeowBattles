using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    [SerializeField] GameObject[] characters,players;
    [SerializeField] Transform spawn1, spawn2;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(PlayerPrefs.GetInt("Player1"));
        Debug.Log(PlayerPrefs.GetInt("Player2"));
        players = new GameObject[2];
        players[0]=Instantiate(characters[PlayerPrefs.GetInt("Player1") - 1], spawn1.position, transform.rotation);
        players[1]=Instantiate(characters[PlayerPrefs.GetInt("Player2") - 1], spawn2.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
