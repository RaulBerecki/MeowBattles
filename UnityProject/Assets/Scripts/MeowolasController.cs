using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowolasController : MonoBehaviour
{
    [SerializeField] Ground gnd;
    public Rigidbody2D rb;
    public float jumpForce, speed;
    float Horizontal, realspeed, dodgeCooldown,damageCooldown;
    public int statement;
    string[] controls;
    [SerializeField] Body body;
    Animator playerAnimator;
    public bool isDodging,isDead;
    //Attack variables
    gameController GM;
    Health enemyHealth, health;
    // Start is called before the first frame update
    void Start()
    {
        isDead = false;
        GM = GameObject.Find("GameManager").GetComponent<gameController>();
        dodgeCooldown = .8f;
        damageCooldown = .2f;
        isDodging = false;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        controls = new string[4];
        if (PlayerPrefs.GetInt("Player1") == 4)
        {
            controls[0] = "Horizontal1";
            controls[1] = "Jump1";
            controls[2] = "Dodge1";
            controls[3] = "Attack1";
            statement = 1;
            PlayerPrefs.SetInt("Player1", 0);
            body.player = "Player1";
            enemyHealth = GM.players[1].GetComponent<Health>();
        }
        if (PlayerPrefs.GetInt("Player2") == 4)
        {
            controls[0] = "Horizontal2";
            controls[1] = "Jump2";
            controls[2] = "Dodge2";
            controls[3] = "Attack2";
            statement = 2;
            PlayerPrefs.SetInt("Player2", 0);
            body.player = "Player2";
            enemyHealth = GM.players[0].GetComponent<Health>();
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
            //Attack();
            Movement();
        }
        Animations();
    }
    void Movement()
    {
        float way = transform.localScale.x;
        if (!isDodging)
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
            Horizontal = 0;
            isDodging = false;
            dodgeCooldown = .8f;
        }
    }
    void Animations()
    {
        if (Horizontal != 0 && gnd.isGrounded && !isDodging && !isDead)
            playerAnimator.Play("Run");
        else if (!gnd.isGrounded && !isDodging && !isDead)
            playerAnimator.Play("Jump");
        else if (Horizontal == 0 && gnd.isGrounded && !isDead)
            playerAnimator.Play("Idle");
        else if (isDodging && !isDead)
            playerAnimator.Play("Dodge");
        else if (isDead)
            playerAnimator.Play("Dead");
    }
    void HealthLook()
    {
        if (health.isDamaged)
        {
            damageCooldown -= Time.deltaTime;
        }
        if (damageCooldown <= 0)
        {
            health.isDamaged = false;
            damageCooldown = .2f;
        }
        if (health.health <= 0)
            isDead = true;
    }
    void Attack()
    {

    }
}
