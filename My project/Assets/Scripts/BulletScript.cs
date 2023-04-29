using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float startSpeed = 1f;
    Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        rb.AddForce(new Vector2(1f, 0f)* startSpeed* Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
