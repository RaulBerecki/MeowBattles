using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameController : MonoBehaviour
{
    public GameObject[] characters,players;
    [SerializeField] Transform spawn1, spawn2;
    CameraController camC;
    // Start is called before the first frame update
    void Start()
    {
        camC = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        players = new GameObject[2];
        players[0]=Instantiate(characters[PlayerPrefs.GetInt("Player1") - 1], spawn1.position, transform.rotation);
        players[1]=Instantiate(characters[PlayerPrefs.GetInt("Player2") - 1], spawn2.position, transform.rotation);
        camC.target1 = players[0].transform;
        camC.target2 = players[1].transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
