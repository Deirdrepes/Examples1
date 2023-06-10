using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] GameObject enemy;
    [SerializeField] Transform currentWeaponLenth;
    [SerializeField] float speed = 5f;
    [SerializeField] float agroDistance = 2f;
    [SerializeField] float attackDistance = 2f;

    [SerializeField] float damageHit = 2f;

    Rigidbody2D rigidbody;
    Animator animator;
    Animation animation;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animation = GetComponent<Animation>();
        //enemy = GetComponent<GameObject>();
        enemy = FindObjectOfType<PlayerMove>().gameObject;
    }

    private void Update()
    {
        enemy = FindObjectOfType<PlayerMove>().gameObject;
        float distToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
        //float distForAttack = Vector2.Distance(transform.position, enemy.transform.position)*attackDistance;
        if(enemy == null)
        {
            return;
        }
        if (distToEnemy < agroDistance && StoppingAfterAttack(distToEnemy) != true)
        {
            animator.SetBool("IsAttack", false);
            StartHunting();
        }
        else if (StoppingAfterAttack(distToEnemy))
        {
            animator.SetBool("IsAttack", true);
        }
        else
        {
            animator.SetBool("IsAttack", false);
            StopHunting();
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
            enemy.GetComponent<Health>().TakeDamage(damageHit);
        }
    }

    public void StartHunting()
    {
        if(enemy.transform.position.y < transform.position.y)
        {
            rigidbody.ClosestPoint(transform.position);
        }


        if(enemy.transform.position.x < transform.position.x)
        {
            rigidbody.velocity = new Vector2(-speed, 0);
        }
        else if(enemy.transform.position.x > transform.position.x)
        {
            rigidbody.velocity = new Vector2(speed, 0);
        }
    }

    void StopHunting()
    {
        rigidbody.velocity = new Vector2(0, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, agroDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, currentWeaponLenth.position);
        Gizmos.DrawWireSphere(currentWeaponLenth.position, 0.1f);
    }
}
