using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current_Weapon : MonoBehaviour
{
    
    public Weapon_Knife? weaponKnife;
    public Weapon_Sword? weaponSword;
    [SerializeField] object defaultWeapon;
    public string somePth = "default_Weapon_";

    public Transform GameObject;
    // Start is called before the first frame update
    void Awake()
    {
        
        weaponKnife = GetComponent<Weapon_Knife>();
        weaponSword = GetComponent<Weapon_Sword>();
        //defaultWeapon = Resources.Load<Weapon_Knife>("Default_Armor_");
        //defaultWeapon = Resources.Load(somePth);
        weaponKnife = (Weapon_Knife)defaultWeapon;
        weaponSword = (Weapon_Sword)defaultWeapon;
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject = FindObjectOfType<Current_Weapon>().GetComponent<Transform>();
        ///transform.position = weapon.transform.position;
        //GameObject.position = transform.position;
        gameObject.name = weaponKnife.name;
    }
}
