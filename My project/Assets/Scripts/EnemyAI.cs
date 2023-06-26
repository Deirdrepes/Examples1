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

public class EnemyAI : MonoBehaviour, IHear
{

    [SerializeField] public GameObject? enemy;
    [SerializeField] List<GameObject> patrolList;
    [SerializeField] Transform currentWeaponLenth;
    [SerializeField] float speed = 5f;
    [SerializeField] float agroDistance;
    [SerializeField] float attackDistance = 2f;



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
        //enemy = FindObjectOfType<PlayerMove>().gameObject;
        
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



        if (gameObject.GetComponent<Health>().healthPoints != 0)
        {
            if (enemy != null && enemy.GetComponent<Health>().healthPoints != 0)
            {
                float distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                Debug.Log("Default dist: " + distToEnemy);
                StandartBehaviour(distToEnemy);
                //Debug.Log("Distance to enemy: "+distToEnemy);
                //Debug.Log("Box Collider Size: "+enemy.GetComponent<BoxCollider2D>().size.x / 2);
                if (distToEnemy >= agroDistance)
                {
                    Debug.Log("we start coroutine");
                    CheckSpace();
                    StartCoroutine(TimeAfterNullTarget());

                }
                else if (distToEnemy <= agroDistance + enemy.GetComponent<BoxCollider2D>().size.x)
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
        if (distToEnemy <= agroDistance + enemy.GetComponent<BoxCollider2D>().size.x && StoppingAfterAttack((float)distToEnemy) != true)
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
        //Debug.Log("Check Space");
        var a = Physics2D.OverlapCircle(gameObject.transform.position, agroDistance);

        var b = Physics2D.Raycast(transform.position, -transform.right, agroDistance);
     
        if (a)
        {
            Debug.Log("Check Space");
            tags = a.tag;
            switch(tags)
            {
                case "Player":
                    {     
                        enemy = a.gameObject;
                        
                        
                    }
                    break;

                case "Item":
                    {
                        enemy = a.gameObject;                  
                    }
                    break;


                case "Sound":
                    {
                        enemy = a.gameObject;
                    }
                    break;
                //default:
                //    {
                //        print("Y");
                //        enemy = null;
                //        return;
                //    }
                   
            }
          
        }
        
    }



    private bool StoppingAfterAttack(float distToEnemy)
    {
        if (distToEnemy <= attackDistance)
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
        Debug.Log("Is Attack");
        StopHunting();
        var a = Physics2D.OverlapCircle(currentWeaponLenth.position, 0.1f);
        if (a)
        {       
            if(enemy != null)
            {
                enemy.GetComponent<Health>().TakeDamage(damageHit, Weapons.Null, null);
            }           
        }
    }

    public void StartHunting()
    {
        //if(enemy.transform.position.y < transform.position.y)
        //{
        //    rigidbody.ClosestPoint(transform.position);
        //}


        if(enemy.transform.position.x < transform.position.x)
        {          
            
            rigidbody.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if(enemy.transform.position.x > transform.position.x)
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
        Gizmos.DrawRay(transform.position, -transform.right);
    }

    public void RespondToSound(Sound sound)
    {
        print("Hello");
        enemy.transform.position = sound.pos;
    }
}
