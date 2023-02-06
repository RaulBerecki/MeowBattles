using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowthurController : MonoBehaviour
{
    [SerializeField] Ground gnd;
    public Rigidbody2D rb;
    public float jumpForce, speed;
    float Horizontal, realspeed, dodgeCooldown,damageCooldown,attackCooldown,way;
    public int statement;
    string[] controls;
    [SerializeField] Body body;
    Animator playerAnimator;
    public bool isDodging,isDead;
    //Attack variables
    gameController GM;
    Health enemyHealth, health;
    bool isAttacking;
    GameObject enemy;
    CameraController Camcontroller;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        attackCooldown = .5f;
        damageCooldown = .2f;
        Camcontroller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        GM = GameObject.Find("GameManager").GetComponent<gameController>();
        dodgeCooldown = 1f;
        isDodging = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        controls = new string[4];
        if (PlayerPrefs.GetInt("Player1") == 3)
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
        else if (PlayerPrefs.GetInt("Player2") == 3)
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
        if (!isDead)
        {
            HealthLook();
            Attack();
            Movement();
        }
        Animations();
    }
    void Movement()
    {
        way = transform.localScale.x;
        if (!isAttacking && !health.isDamaged)
            Horizontal = Input.GetAxisRaw(controls[0]);
        rb.velocity = new Vector2(Horizontal * speed, rb.velocity.y);
        if (gnd.isGrounded)
            speed = realspeed;
        else if (!gnd.isGrounded)
            speed = realspeed * .75f;
        if (gnd.isGrounded && Input.GetButtonDown(controls[1]) && !isDodging)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        if (Horizontal > 0)
            transform.localScale = new Vector3(1, 1, 1);
        else if (Horizontal < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }
    void Animations()
    {
        if (Horizontal != 0 && gnd.isGrounded && !isDodging && !isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Run");
        else if (!gnd.isGrounded && !isDodging && !isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Jump");
        else if (Horizontal == 0 && gnd.isGrounded && !isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Idle");
        else if (isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Attack");
        else if (health.isDamaged && !isDead)
            playerAnimator.Play("TakeDamage");
        else if (isDead)
            playerAnimator.Play("Dead");
    }
    void HealthLook()
    {
        if (health.isDamaged)
        {
            damageCooldown -= Time.deltaTime;
            rb.velocity = new Vector2(0, 0);
        }
        if (damageCooldown <= 0)
        {
            health.isDamaged = false;
            damageCooldown = .2f;
        }
        if (health.health <= 0 || transform.position.y < -20)
        {
            isDead = true;
            GM.finished = true;
            Camcontroller.target1 = enemy.transform;
        }
    }
    void Attack()
    {
        if (isAttacking)
        {
            attackCooldown -= Time.deltaTime;
            Horizontal = 0;
        }

        if (attackCooldown <= 0 || health.isDamaged)
        {
            isAttacking = false;
            attackCooldown = .3f;
        }
        if (Input.GetButtonDown(controls[3]) && isAttacking == false && gnd.isGrounded)
        {
            isAttacking = true;
            if (((way > 0 && enemy.transform.position.x - transform.position.x <= 1.75f && enemy.transform.position.x - transform.position.x > 0f) || (way < 0 && enemy.transform.position.x - transform.position.x >= -1.75f && enemy.transform.position.x - transform.position.x < 0f)) && enemy.transform.position.y - transform.position.y <= .5f)
            {
                enemyHealth.health -= Random.RandomRange(10, 25);
                enemyHealth.isDamaged = true;
            }
        }
    }
}
