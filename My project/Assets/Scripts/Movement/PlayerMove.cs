using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anim;
    public SpriteRenderer sr;

    public float speed = 2f;
    public float jumpForce = 7f;

    private Vector2 moveVector;
    private bool faceRight = true;

    [SerializeField] AnimatorController animatorController;
    [SerializeField] public Armor_Logic armor;

    [SerializeField] public Current_Armor current_Armor;

    [SerializeField] LayerMask layerMask;
    [SerializeField] GameObject bottomPart;

    [SerializeField] Health Health;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        animatorController = GetComponent<AnimatorController>();
        armor = GetComponent<Armor_Logic>();
        current_Armor = GameObject.FindGameObjectWithTag("CurrentArmor").GetComponentInChildren<Current_Armor>();
        Health = gameObject.GetComponent<Health>();
    }

    //void Start()
    //{
    //    //animatorController = armor.armorAnimatorController;
    //    //sr.sprite = armor.spriteRenderer.sprite;
    //    //anim.runtimeAnimatorController = animatorController;
    //}

    void Update()
    {
        if (Health.healthPoints != 0)
        {

            Debug.Log("we can move");
            ArmorAnimationLogic();
            Walk();
            Reflect();
            Jump();
        }
        else
        {
            Debug.Log("we can`t move");
            return;
        }
    }

    private void ArmorAnimationLogic()
    {
        armor = current_Armor.armor;

        animatorController = armor.armorAnimatorController;
        //sr.sprite = armor.spriteRenderer.sprite;
        anim.runtimeAnimatorController = animatorController;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       
        if(collision.gameObject.tag == "Item")
        {
            
            Debug.Log("Yea it`s item");         
            if(Physics2D.OverlapCircle(bottomPart.transform.position, 1f))
            {
                Destroy(collision.gameObject.GetComponentInChildren<GameObject>());
            }
        }
    }

    private void FixedUpdate()
    {
        OnGround();
    }

    bool OnGround()
    {
        if(Physics2D.OverlapCircle(bottomPart.transform.position, 0.1f, layerMask))
        {
            return true;
        }
        else
        {
            return false;
        }
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

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && OnGround())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}