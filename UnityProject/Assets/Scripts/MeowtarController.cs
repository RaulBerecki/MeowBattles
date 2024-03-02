using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowtarController : MonoBehaviour
{
    [SerializeField] Ground gnd;
    public Rigidbody2D rb;
    public float jumpForce, speed;
    float Horizontal, realspeed, dodgeCooldown,damageCooldown, attackCooldown;
    public int statement;
    string[] controls;
    [SerializeField] Body body;
    Animator playerAnimator;
    public bool isDodging,isDead,isAttacking;
    //Attack variables
    gameController GM;
    Health enemyHealth, health;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform attackcoord;
    string enemy;
    CameraController Camcontroller;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        isAttacking = false;
        attackCooldown = .3f;
        Camcontroller = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        GM = GameObject.Find("GameManager").GetComponent<gameController>();
        damageCooldown = .2f;
        dodgeCooldown = 1f;
        isDodging = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        controls = new string[4];
        if (PlayerPrefs.GetInt("Player1") == 2)
        {
            controls[0] = "Horizontal1";
            controls[1] = "Jump1";
            controls[2] = "Dodge1";
            controls[3] = "Attack1";
            statement = 1;
            PlayerPrefs.SetInt("Player1", 0);
            body.player = "Player1";
            enemyHealth = GM.players[1].GetComponent<Health>();
            enemy = "Player2";
        }
        else if (PlayerPrefs.GetInt("Player2") == 2)
        {
            controls[0] = "Horizontal2";
            controls[1] = "Jump2";
            controls[2] = "Dodge2";
            controls[3] = "Attack2";
            statement = 2;
            PlayerPrefs.SetInt("Player2", 0);
            body.player = "Player2";
            enemyHealth = GM.players[0].GetComponent<Health>();
            enemy = "Player1";
        }
        realspeed = speed;
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead && !GM.paused)
        {
            HealthLook();
            Attack();
            Movement();
        }
        Animations();
    }
    void Movement()
    {
        float way = transform.localScale.x;
        if (!isDodging && !isAttacking && !health.isDamaged)
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
        if (!isDodging && Input.GetButtonDown(controls[2]) && gnd.isGrounded && Horizontal != 0)
        {
            isDodging = true;
        }
        if (isDodging && !health.isDamaged)
        {
            rb.velocity = new Vector2(way * speed * 1.3f, rb.velocity.y);
            dodgeCooldown -= Time.deltaTime;
        }

        if (dodgeCooldown <= 0)
        {
            Horizontal = 0;
            isDodging = false;
            dodgeCooldown = 1f;
        }
    }
    void Animations()
    {
        if (Horizontal != 0 && gnd.isGrounded && !isDodging && !isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Run");
        else if (!gnd.isGrounded && !isDodging && !isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Jump");
        else if (Horizontal == 0 && gnd.isGrounded && !isAttacking && !health.isDamaged && !isDead)
            playerAnimator.Play("Idle");
        else if (isDodging && !health.isDamaged && !isDead)
            playerAnimator.Play("Dodge");
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
            isDodging = false;
            dodgeCooldown = .8f;
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
            Camcontroller.target1 = GameObject.FindGameObjectWithTag(enemy).transform;
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
        if (Input.GetButtonDown(controls[3]) && isAttacking == false)
        {
            GameObject obj=Instantiate(fireball, attackcoord.position, transform.rotation);
            obj.GetComponent<FireballController>().enemyhealth = enemyHealth;
            obj.GetComponent<FireballController>().enemy = enemy;
            obj.GetComponent<FireballController>().direction = transform.localScale.x;
            isAttacking = true;
        }
    }
}
