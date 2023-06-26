using UnityEditor.Animations;
using UnityEngine;

namespace Examples.Combat
{


    public class Fighter : MonoBehaviour
    {

        public Weapons currentWeaponType;
        [SerializeField] public Transform currentWeaponLenth;        

        [SerializeField] public int weaponIndex;
        [SerializeField] float weaponDamage = 10f;
        bool isHited = false;

        
        [SerializeField]Health health1;
        Animator anim1;

        [SerializeField] AnimatorController animatorController;
        [SerializeField] Armor_Logic armor;
        [SerializeField] Current_Armor current_Armor;

        [SerializeField] Weapon weapon;
        [SerializeField] Current_Weapon current_Weapon;

        float timeSinceLastAttack = Mathf.Infinity;
        [SerializeField] float timeBetweenAttacks = 2f;
        // Start is called before the first frame update
        void Start()
        {                  
            currentWeaponLenth = FindObjectOfType<Current_Weapon>().transform;

            
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



                if (Input.GetKeyDown(KeyCode.F))
                {
                    Attack();

                }
            }           
        }

        void Attack()
        {
             if(currentWeaponType == Weapons.Fists)
            {
                animatorController = weapon.armorAnimatorController;
                
                
            }

            anim1.SetTrigger("isAttack");


            var a = Physics2D.OverlapCircle(currentWeaponLenth.position, 0.1f);
            
            if (a && a != null)
            {               
                a.GetComponent<Health>().TakeDamage(weaponDamage, currentWeaponType, weapon);
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