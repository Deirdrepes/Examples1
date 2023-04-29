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

        Weapon weapon1;
        Health health1;

        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float timeBetweenAttacks = 2f;
        // Start is called before the first frame update
        void Start()
        {
            currentWeaponLenth = GetComponent<Transform>();
            currentWeaponLenth = null;
            weaponVector = GetComponent<Transform>();
            weaponVector = FindObjectOfType<Weapon>().transform;

            weapon1 = GetComponent<Weapon>();
            health1 = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            currentWeaponLenth = weaponVector.transform;
            
            if(Input.GetKey(KeyCode.F))
            {
                Attack();
                Debug.Log("hit");
            }
        }

        void FixedUpdate()
        {

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            Debug.Log(collision + "check");
        }

        void Attack()
        {
            var a = Physics2D.OverlapCircle(currentWeaponLenth.position, 0.1f);
            if (timeSinceLastAttack > timeBetweenAttacks && a)
            {        
                a.GetComponent<Health>().TakeDamage(weaponDamage);
              
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