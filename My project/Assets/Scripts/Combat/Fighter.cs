using System;
using UnityEditor.Animations;
using UnityEngine;

namespace Examples.Combat
{


    public class Fighter : MonoBehaviour
    {

        public Weapons currentWeaponType;
        [SerializeField] public Transform currentKnifeLenth;
        [SerializeField] public Transform currentSwordLenth;
        [SerializeField] public Transform currentRangeWeaponLenth;        
        //[SerializeField] public Transform currentWeaponLenth;        
        
        //[SerializeField] public int weaponIndex;
        [SerializeField] float weaponDamage;

        [SerializeField] float radiusOfKnifeAttack;
        [SerializeField] float radiusOfSwordAttack;
        bool isHited = false;

        public GameObject testBullet;
        
        [SerializeField]Health health1;
        Animator anim1;

        [SerializeField] AnimatorController animatorController;

        [SerializeField]Weapon weapon;

        [SerializeField] Armor_Logic armor;
        [SerializeField] Current_Armor current_Armor;

        [SerializeField] Current_Weapon current_Weapon;

        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float timeBetweenAttacks = 2f;
        // Start is called before the first frame update
        void Start()
        {
            weapon = GetComponent<Weapon>();
            //currentKnifeLenth = FindObjectOfType<Current_Weapon>().transform;

            
            
            anim1 = GetComponent<Animator>();
            animatorController = GetComponent<AnimatorController>();
          
           
            health1 = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();


            #region
            //Armor Initiolization
            armor = GetComponent<Armor_Logic>();
            current_Armor = GameObject.FindGameObjectWithTag("CurrentArmor").GetComponent<Current_Armor>();
            #endregion
        }

        // Update is called once per frame
        void Update()
        {
            
            if(health1.healthPoints != 0)
            {
                timeSinceLastAttack += Time.deltaTime;
                
                armor = current_Armor.armor;

                animatorController = armor.armorAnimatorController;
                anim1.runtimeAnimatorController = animatorController;



                if (Input.GetKeyDown(KeyCode.X))
                {
                    KnifeAttack();                   
                }
                if (Input.GetKeyDown(KeyCode.F))
                {                   
                    SwordAttack();
                }
                if (Input.GetKeyDown(KeyCode.C))
                {
                    RangeAttack();               
                }
            }           
        }

        void KnifeAttack()
        {
            weaponDamage = 10;
            anim1.SetTrigger("isKnifeAttack");


            var a = Physics2D.OverlapCircle(currentKnifeLenth.position, radiusOfKnifeAttack);

            if (a && a != null)
            {
                a.GetComponent<Health>().TakeDamage(weaponDamage, currentWeaponType, weapon);              
                timeSinceLastAttack = 0;
            }
        }

        void SwordAttack()
        {
            weaponDamage = 20;
            anim1.SetTrigger("isSwordAttack");

            var a = Physics2D.OverlapCircleAll(currentSwordLenth.position, radiusOfSwordAttack);

            foreach(var b in a)
            {
                
                    b.GetComponent<Health>().TakeDamage(weaponDamage, currentWeaponType, weapon);
                    timeSinceLastAttack = 0;
                
            }
        }

        void RangeAttack()
        {
            //must be weapon.bulletOfRangeWeapon
            Instantiate(testBullet, currentRangeWeaponLenth.transform.position, new Quaternion());
        }



        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, currentSwordLenth.position);
            Gizmos.DrawWireSphere(currentSwordLenth.position, radiusOfSwordAttack);

            Gizmos.color = Color.black;
            Gizmos.DrawLine(transform.position, currentKnifeLenth.position);
            Gizmos.DrawWireSphere(currentKnifeLenth.position, radiusOfKnifeAttack);
        }
    }
}