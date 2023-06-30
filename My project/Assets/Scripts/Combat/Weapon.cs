using Examples;
using Examples.Combat;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class Weapon : MonoBehaviour, IItem
{
    public bool IsDestroyed { get; set; }
    public string Path { get; set; }
    [SerializeField] public float damage;
    [SerializeField] public float meleeRange;

    public GameObject bulletOfRangeWeapon;

    [SerializeField] public AnimationClip animationClip;

    public Weapons weapons;
    public Tiers tiers;

    Current_Weapon current_Weapon;

    // Start is called before the first frame update
    void Start()
    {
       
       // weaponVector.Set(GetComponentInParent<Transform>().localPosition.x + xPos, GetComponentInParent<Transform>().localPosition.y + yPos);
        current_Weapon = FindObjectOfType<Current_Weapon>().GetComponentInChildren<Current_Weapon>();
        RemoverCloneNaming();
        //weaponVector = GetComponent<Vector2>(); 
    }

    public void RemoverCloneNaming()
    {
        if (gameObject.name.Contains("(Clone)"))
        {
            gameObject.name = gameObject.name.Substring(0, gameObject.name.LastIndexOf('_') + 1);
            Path = gameObject.name;
        }
        else
        {
            Path = gameObject.name;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
        
        if (IsDestroyed)
        {
            Invoke("InteractionInvoke", 0.3f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemKeeper(collision);
    }
    public void ItemKeeper(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player keep it");

            current_Weapon.weapon = Resources.Load<Weapon>(Path);

            IsDestroyed = true;
        }
    }
    public void InteractionInvoke()
    {
        Destroy(gameObject);
    }
}
