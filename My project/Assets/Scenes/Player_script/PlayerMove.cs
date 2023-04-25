using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;

    public float speed = 2f;

    private Vector2 moveVector;
    private bool faceRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Walk();
        Reflect();
        Jump();
        CheckingGround();
    }

    void Walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        anim.SetFloat("Movex", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }

    void Reflect()
    {
        if (moveVector.x > 0)
        {
            if (!faceRight)
            {
                transform.localScale = new Vector2(1, 1);
                faceRight = true;
            }
        }
        else if (moveVector.x < 0)
        {
            if (faceRight)
            {
                transform.localScale = new Vector2(-1, 1);
                faceRight = false;
            }
        }
    }

    public float jumpForce = 7f;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    public bool onGround;

    public Transform GroundCheck;

    public float checkRadius = 0.5f;

    public LayerMask Ground;


    void CheckingGround()
    {

        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }


}