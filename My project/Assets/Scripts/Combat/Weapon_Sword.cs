using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Sword : MonoBehaviour, Weapon, IItem
{
    [SerializeField] private float damage;
    [SerializeField] public AnimationClip animClip;
    [SerializeField] private Sprite spriteUI;

    [SerializeField] public Tier tier;
    [SerializeField] public bool IsDestroyed { get; set; }
    [SerializeField] public string Path { get; set; }
    [SerializeField] public float Damage { get => damage; set => damage.ToString(); }
    [SerializeField] public float MeleeRange { get; set; }
    [SerializeField] public AnimationClip animationClip { get => animClip; set => animClip.ToString(); }
    [SerializeField] public Sprite SpriteUI { get => spriteUI; set => spriteUI.ToString(); }
    public GameObject bulletOfRangeWeapon { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [SerializeField] Current_Weapon current_Weapon;
    // Start is called before the first frame update
    void Start()
    {
        RemoverCloneNaming();
        var b = FindObjectsByType<Current_Weapon>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).GetValue(0);

        current_Weapon = (Current_Weapon)b;
        var a = Damage;
        print(a);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDestroyed)
        {
            Invoke("InteractionInvoke", 0.3f);
        }
        print(Path);
        //print(Path);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemKeeper(collision);
    }
    public void ItemKeeper(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Player keep it");

            current_Weapon.weaponSword = Resources.Load<Weapon_Sword>(Path);

            IsDestroyed = true;
        }
    }
    public void InteractionInvoke()
    {
        Destroy(gameObject);
    }
}