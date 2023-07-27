using Examples;
using Examples.Combat;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] public GameObject? enemy;
    [SerializeField] List<GameObject> patrolList;
    [SerializeField] Transform currentWeaponLenth;
    [SerializeField] float speed = 5f;
    [SerializeField] float agroDistance;
    [SerializeField] float attackDistance = 2f;


    [SerializeField] bool ignoreAlls = false;

    Sound someSound;

    [SerializeField] float damageHit = 2f;

    string tags;

    [SerializeField] Transform leftBorder; 
    [SerializeField] Transform rightBorder;

    Rigidbody2D rigidbody;
    Animator animator;
    Animation animation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        enemy = GetComponent<GameObject>();
        enemy = null;      
    }

    public void Update()
    {

       
        //if(enemy != null)
        //{
        //    float NewDistToEnemy = rigidbody.Distance(enemy.GetComponent<BoxCollider2D>()).distance;
        //    Debug.Log("Phisical dist: " + NewDistToEnemy);

        //    print("-----");

        //    float distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
        //    Debug.Log("Default dist: " + distToEnemy);
        //}
        //else
        //{
        //    CheckSpace();
        //}

        if(enemy != null && enemy.tag == "Player")
        {
            //Physics2D.IgnoreLayerCollision(0, 1, true);
            ignoreAlls = true;
        }
        else
        {
            ignoreAlls = false;
        }
        
       
        if (gameObject.GetComponent<Health>().healthPoints != 0)
        {
            if (enemy != null && enemy.GetComponent<Health>().healthPoints != 0)
            {
                float distToEnemy = rigidbody.Distance(enemy.GetComponent<BoxCollider2D>()).distance;
                Debug.Log("Default dist: " + distToEnemy);
                StandartBehaviour(distToEnemy);
               
                if (distToEnemy >= agroDistance)
                {
                    Debug.Log("we start coroutine");
                    CheckSpace();
                    StartCoroutine(TimeAfterNullTarget());

                }
                else if (distToEnemy <= agroDistance)
                {
                    Debug.Log("we stop coroutine");

                    CheckSpace();
                    StopAllCoroutines();

                }
            }
            else
            {
                animator.SetBool("IsAttack", false);

                CheckSpace();
            }

            if (rigidbody.velocity.x != 0)
            {
                animator.SetBool("Movex", true);
            }
            else
            {
                animator.SetBool("Movex", false);
            }


        }
        else
        {
            //StopCoroutine(TimeAfterNullTarget());
            StopHunting();
            return;
        }
        
    }
    
    IEnumerator TimeAfterNullTarget()
    {
        StartHunting();
        yield return new WaitForSeconds(3);
        StopHunting();
        enemy = null;

    }
    
    public void StandartBehaviour(float? distToEnemy)
    {
        StopCoroutine(TimeAfterNullTarget());
        if (distToEnemy <= agroDistance && StoppingAfterAttack((float)distToEnemy) != true)
        {
            animator.SetBool("IsAttack", false);
            StartHunting();
        }
        else if (StoppingAfterAttack((float)distToEnemy))
        {
            animator.SetBool("IsAttack", true);
        }
        else
        {
            animator.SetBool("IsAttack", false);
            //StartCoroutine(TimeAfterNullTarget());
        }
    }

    private IEnumerator PatrolPath()
    {
        //yield return new WaitForSeconds(2);
        float a = Random.value;
        //Debug.Log("Random value:" + a);
        if(a > 0.5)
        {
            Debug.Log(a * 10);
            yield return new WaitForSeconds(a * 10);
            //Debug.Log("left");
            //float? distToLeftBorder = Vector2.Distance(transform.position, leftBorder.position);
            //float randomMovement = Random.RandomRange(-agroDistance, agroDistance);

            //Debug.Log("Distance to left border:" + distToLeftBorder);
            //Debug.Log("Random movement:" + randomMovement);
            //Debug.Log("Transform position + Random movement:" + transform.position.x + randomMovement);

            //if(distToLeftBorder > -transform.right.x)
            //{
            //    rigidbody.velocity = new Vector2(-speed, 0);
            //    transform.localScale = new Vector2(1, 1);
            //}
            //else if(distToLeftBorder < 1)
            //{
            //    rigidbody.velocity = new Vector2(0, 0);
            //}

        }
        else
        {

            Debug.Log("right");
            float? distToRightBorder = Vector2.Distance(transform.position, rightBorder.position);

        }


    }
    
    void CheckSpace()
    {
        /*
         * This a very fucking bad variant, it makes a lot of fucking bugs
         */
        //Future will be OverlapCircleNonAlloc
        foreach (var a in Physics2D.RaycastAll(transform.position, new Vector2(-transform.localScale.x, 0), agroDistance))
        {
            tags = a.transform.tag;
            //Debug.Log(tags);
            switch (tags)
            {
                case "Player":
                    enemy = a.transform.gameObject;
                    break;

                case "Item":
                    enemy = a.transform.gameObject;
                    break;              

                default:
                    print("Non");
                    break;
            }
        }

        foreach (var ab in Physics2D.OverlapCircleAll(transform.position, agroDistance))
        {
            tags = ab.tag;
            //Debug.Log(tags);
            switch (tags)
            {                            
                case "Sound":
                    if (!ignoreAlls)
                    {
                        enemy = ab.gameObject;
                    }                                    
                    break;

                    default:
                    print("nothing");
                    break;
            }
        }      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision != null && collision.transform.tag == "Player") { enemy = collision.gameObject; }
    }

    private bool StoppingAfterAttack(float distToEnemy)
    {
        if (distToEnemy <= attackDistance-0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Attack()
    {
        Debug.Log("Is SwordAttack");
        StopHunting();
        var a = Physics2D.OverlapCircle(currentWeaponLenth.position, 0.1f);
        if (a)
        {       
            if(enemy != null)
            {
                enemy.GetComponent<Health>().TakeDamage(damageHit, Weapons.Null);
            }           
        }
    }

    public void StartHunting()
    {
        if(enemy.transform.position.x <= transform.position.x)
        {          
            
            rigidbody.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if(enemy.transform.position.x >= transform.position.x)
        {         
            
            rigidbody.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
    }

    void StopHunting()
    {
        rigidbody.velocity = new Vector2(0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(gameObject.transform.position, agroDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, currentWeaponLenth.position);
        Gizmos.DrawWireSphere(currentWeaponLenth.position, 0.1f);

        Gizmos.color = Color.white;
        Gizmos.DrawRay(transform.position, new Vector2(-transform.localScale.x, 0));
    }   
}
