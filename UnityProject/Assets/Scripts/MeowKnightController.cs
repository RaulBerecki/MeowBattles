using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowKnightController : MonoBehaviour
{
    [SerializeField] Ground gnd;
    public Rigidbody2D rb;
    public float jumpForce, speed,way;
    float Horizontal,realspeed,dodgeCooldown,attackCooldown,damageCooldown;
    public int statement;
    string[] controls;
    [SerializeField] Body body;
    Animator playerAnimator;
    public bool isDodging;
    //Attack variables
    gameController GM;
    public Health enemyHealth, health;
    bool isAttacking;
    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        attackCooldown = .3f;
        damageCooldown = .2f;
        GM = GameObject.Find("GameManager").GetComponent<gameController>();
        dodgeCooldown = .8f;
        isDodging = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        controls = new string[4];
        if(PlayerPrefs.GetInt("Player1")==1)
        {
            controls[0] = "Horizontal1";
            controls[1] = "Jump1";
            controls[2] = "Dodge1";
            controls[3] = "Attack1";
            statement = 1;
            PlayerPrefs.SetInt("Player1", 0);
            body.player = "Player1";
            enemyHealth = GM.players[1].GetComponent<Health>();
            enemy = GM.players[1];
        }
        else if(PlayerPrefs.GetInt("Player2")==1)
        {
            controls[0] = "Horizontal2";
            controls[1] = "Jump2";
            controls[2] = "Dodge2";
            controls[3] = "Attack2";
            statement = 2;
            PlayerPrefs.SetInt("Player2", 0);
            body.player = "Player2";
            enemyHealth = GM.players[0].GetComponent<Health>();
            enemy = GM.players[0];
        }
        realspeed = speed;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Movement();
        Animations();
        HealthLook();
    }
    void Movement()
    {
        way = transform.localScale.x;
        if(!isDodging)
            Horizontal = Input.GetAxisRaw(controls[0]);
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
        if (gnd.isGrounded)
            speed = realspeed;
        else if (!gnd.isGrounded)
            speed = realspeed *.75f;
        if (gnd.isGrounded && Input.GetButtonDown(controls[1]) &&!isDodging)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (Horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if(Horizontal <0)
            transform.localScale = new Vector3(-1, 1, 1);
        if (!isDodging && Input.GetButtonDown(controls[2]) && gnd.isGrounded)
        {
            isDodging = true;
        }
        if (isDodging)
        {
            rb.velocity = new Vector2(way * speed * 1.3f, rb.velocity.y);
            dodgeCooldown -= Time.deltaTime;
        }
            
        if (dodgeCooldown <= 0)   
        {
            isDodging = false;
            dodgeCooldown = 0.8f;
        }
    }
    void Animations()
    {
        if (Horizontal != 0 && gnd.isGrounded && !isDodging && !isAttacking && !health.isDamaged)
            playerAnimator.Play("Run");
        else if (!gnd.isGrounded && !isDodging && !isAttacking && !health.isDamaged)
            playerAnimator.Play("Jump");
        else if (Horizontal == 0 && gnd.isGrounded && !isAttacking && !health.isDamaged)
            playerAnimator.Play("Idle");
        else if (isDodging && !health.isDamaged)
            playerAnimator.Play("Dodge");
        else if (isAttacking && !health.isDamaged)
            playerAnimator.Play("Attack");
        else if (health.isDamaged)
            playerAnimator.Play("TakeDamage");
    }
    void HealthLook()
    {
        if(health.isDamaged)
        {
            damageCooldown -= Time.deltaTime;
        }
        if (damageCooldown <= 0)
        {
            health.isDamaged = false;
            damageCooldown = .2f;
        }
    }
    void Attack()
    {
        if (isAttacking)
        {
            attackCooldown -= Time.deltaTime;
            Horizontal = 0;
        }
            
        if(attackCooldown<=0)
        {
            isAttacking = false;
            attackCooldown = .3f;
        }
        if(Input.GetButtonDown(controls[3]) && isAttacking == false && gnd.isGrounded)
        {
            isAttacking = true;
            if (((way>0 && enemy.transform.position.x-transform.position.x<=1.25f && enemy.transform.position.x - transform.position.x > 0f) || (way < 0 && enemy.transform.position.x - transform.position.x >= -1.25f && enemy.transform.position.x - transform.position.x < 0f)) && enemy.transform.position.y - transform.position.y<=.5f)
            {
                enemyHealth.health -= Random.RandomRange(10, 25);
                enemyHealth.isDamaged = true;
            }
        }
    }
}
