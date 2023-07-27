using Examples.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float damage;
    [SerializeField] float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerMove playerMove = FindObjectOfType<PlayerMove>();
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity.Set(1, 0);
        if (playerMove.faceRight)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        //rb.AddForce(new Vector2 (transform.right.x*100, 0) * Time.deltaTime);
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("Hitted Apple");
        if(collision.transform.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(damage, Weapons.Range);
        }       
        Destroy(gameObject);
    }
    
}

