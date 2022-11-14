using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeowtarController : MonoBehaviour
{
    [SerializeField] Ground gnd;
    public Rigidbody2D rb;
    public float jumpForce, speed;
    float Horizontal, realspeed, dodgeCooldown;
    public int statement;
    string[] controls;
    [SerializeField] Body body;
    Animator playerAnimator;
    public bool isDodging;
    // Start is called before the first frame update
    void Start()
    {
        dodgeCooldown = 1f;
        isDodging = false;
        PlayerPrefs.SetInt("Player1", 1);
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        controls = new string[4];
        if (PlayerPrefs.GetInt("Player1") == 1)
        {
            controls[0] = "Horizontal1";
            controls[1] = "Jump1";
            controls[2] = "Dodge1";
            controls[3] = "Attack1";
            statement = 1;
            PlayerPrefs.SetInt("Player1", 0);
            body.player = "Player1";
        }
        if (PlayerPrefs.GetInt("Player2") == 1)
        {
            controls[0] = "Horizontal2";
            controls[1] = "Jump2";
            controls[2] = "Dodge2";
            controls[3] = "Attack2";
            statement = 2;
            PlayerPrefs.SetInt("Player2", 0);
            body.player = "Player2";
        }
        realspeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Animations();
    }
    void Movement()
    {
        Debug.Log(Horizontal);
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
            dodgeCooldown = 1f;
        }
    }
    void Animations()
    {
        if (Horizontal != 0 && gnd.isGrounded && !isDodging)
            playerAnimator.Play("Run");
        else if (!gnd.isGrounded && !isDodging)
            playerAnimator.Play("Jump");
        else if (Horizontal == 0 && gnd.isGrounded)
            playerAnimator.Play("Idle");
        else if (isDodging)
            playerAnimator.Play("Dodge");
    }
    void Attack()
    {

    }
}
