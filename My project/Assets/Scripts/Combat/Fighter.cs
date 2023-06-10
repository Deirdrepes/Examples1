using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.Combat
{

    public class Fighter : MonoBehaviour
    {
        [SerializeField] public Transform currentWeaponLenth;
        [SerializeField] public Transform weaponVector;

        [SerializeField] public int weaponIndex;
        [SerializeField] float weaponDamage = 10f;
        bool isHited = false;

        Rigidbody2D weaponRigidbody;
        Weapon weapon1;
        Health health1;
        Animator anim1;

        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float timeBetweenAttacks = 2f;
        // Start is called before the first frame update
        void Start()
        {
            currentWeaponLenth = GetComponent<Transform>();
            //currentWeaponLenth = null;
            weaponVector = GetComponent<Transform>();
            weaponVector = FindObjectOfType<Weapon>().transform;

            weaponRigidbody = GetComponent<Rigidbody2D>();
            anim1 = GetComponent<Animator>();
            weapon1 = GetComponent<Weapon>();
            health1 = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            currentWeaponLenth = weaponVector.transform;
            
            if(Input.GetKeyDown(KeyCode.F))
            {
                Attack();
                
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision + "check");
        }

        void Attack()
        {
            Debug.Log("hit");
            anim1.SetTrigger("isAttack");
            var a = Physics2D.OverlapCircle(currentWeaponLenth.position, 0.1f);
            //timeSinceLastAttack > timeBetweenAttacks &&
            if (a && a != null)
            {               
                a?.GetComponent<Health>().TakeDamage(weaponDamage);
                //weaponRigidbody.AddForce(currentWeaponLenth.right, ForceMode2D.Impulse);
                
                timeSinceLastAttack = 0;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, currentWeaponLenth.position);
            Gizmos.DrawWireSphere(currentWeaponLenth.position, 0.1f);
        }
    }
}