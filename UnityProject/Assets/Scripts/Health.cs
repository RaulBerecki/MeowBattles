using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public bool isDamaged;
    public int health;
    // Start is called before the first frame update
    void Start()
    {
        isDamaged = false;
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
