using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballController : MonoBehaviour
{
    public float direction, speed;
    public string enemy;
    Rigidbody2D rb;
    bool isUsed;
    public Health enemyhealth;
    // Start is called before the first frame update
    void Start()
    {
        isUsed = false;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag==enemy && !isUsed)
        {
            enemyhealth.isDamaged = true;
            enemyhealth.health -= Random.RandomRange(10, 23);
            isUsed = true;
        }
    }
}
