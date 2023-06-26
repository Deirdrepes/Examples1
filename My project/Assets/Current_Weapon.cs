using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current_Weapon : MonoBehaviour
{
    public Weapon weapon;
    [SerializeField] private Weapon defaultWeapon;

    public Transform GameObject;
    // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<Weapon>();
        defaultWeapon = Resources.Load<Weapon>("Default_Weapon_");
        weapon = defaultWeapon;
    }

    // Update is called once per frame
    void Update()
    {
       GameObject = FindObjectOfType<Current_Weapon>().GetComponent<Transform>();
        transform.position = weapon.transform.position;
        GameObject.position = transform.position;
    }
}
