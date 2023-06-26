using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEditor;
using UnityEngine.PlayerLoop;
using System.ComponentModel;
using UnityEditor.PackageManager;
using UnityEditor.Animations;
using System;
using Examples.Combat;

public class Health : MonoBehaviour
{
    [SerializeField] public float healthPoints = 100f;
    [SerializeField] public float maxHealthPoints;

    
    // Start is called before the first frame update
    bool isDead = false;

    [SerializeField]Armor_Logic armorLogic;
    [SerializeField] Current_Armor current_Armor;

    [SerializeField]Animator animator;
    [SerializeField]AnimatorController animatorController;
    
    [SerializeField] bool isTrigerred = false;

    [SerializeField] List<GameObject> dropList;
 
    public void Start()
    {
       
        armorLogic = GetComponent<Armor_Logic>();
        animator = GetComponent<Animator>();
        animatorController = GetComponent<AnimatorController>();
       
        current_Armor = GameObject.FindGameObjectWithTag("CurrentArmor").GetComponentInChildren<Current_Armor>();
        
        
    }

    public void Update()
    {
        //if (isDead) { return; }
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthPoints = healthPoints - 5f;
        }
    
        armorLogic = current_Armor.armor;
        if(gameObject.tag == "Player")
        {
            animatorController = armorLogic.armorAnimatorController;
            animator.runtimeAnimatorController = animatorController;
            
        }

        
    }

    [SerializeField] SpriteRenderer spriteRenderer;

    public bool IsDead()
    {
        Debug.Log(isDead);
        return isDead;
    }
    public void TakeDamage(float damage, Weapons weaponEnum, Weapon weapon)
    {

    

        if(weaponEnum == Weapons.Fists)

        healthPoints = Mathf.Max(healthPoints - damage, 0);
        if (healthPoints == 0)

        {
            
            Debug.Log("Fists Attack");
        }
        else if (weaponEnum == Weapons.Knife)
        {
            damage = damage + 10;
            Debug.Log("Knife Attack");
        }
        else if(weaponEnum == Weapons.Null)
        {
            Debug.Log("Null");
        }
        //if (isDead) { return ; }
        if (!IsDead()) 
        {
            Debug.Log("Hited");
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

            healthPoints = Mathf.Max((float)(healthPoints - damage * armorLogic.armorNumber), 0);
            if (healthPoints == 0)
            {
                
                if (!isTrigerred)
                {
                    Debug.Log("His Triggered2");
                    animator.SetTrigger("isDead1");
                }
                    Debug.Log("His Triggered");
                    isTrigerred = true;
                    
                   
                
                
                
            }
            spriteRenderer.color = Color.red;
            Invoke("ReturnDefaultColor", 0.3f);
        }
    }

    private SpriteRenderer ReturnDefaultColor()
    {
        spriteRenderer.color = Color.white;
        return spriteRenderer;
    }

    private IEnumerator Die()
    {
        if (!isDead)
        {                      
            yield return new WaitForSeconds(0);
            
            isDead = true;
            Destroy(gameObject);      
            DropList(dropList);
        }
    }

    private void DropList(List<GameObject> list)
    {
        

        list = dropList;
        foreach (GameObject item in list)
        {           
            Instantiate(item, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }   
    }
}
