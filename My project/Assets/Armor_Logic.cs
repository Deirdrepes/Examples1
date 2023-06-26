using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Armor_Logic : MonoBehaviour, IItem
{
    [SerializeField][Range(0f, 1f)] public float armorNumber = 0f;
    [SerializeField] public AnimatorController armorAnimatorController;

    //Armor_Logic armor;
    Current_Armor current_Armor;
    
    public bool IsDestroyed { get; set; }

    public string Path { get; set; }

    //[SerializeField] public SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        RemoverCloneNaming();
        //armor = GetComponent<Armor_Logic>();
        current_Armor = GameObject.FindGameObjectWithTag("CurrentArmor").GetComponentInChildren<Current_Armor>();
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

            current_Armor.armor = Resources.Load<Armor_Logic>(Path);

            IsDestroyed = true;
        }
    }

    public void InteractionInvoke()
    {
        Destroy(gameObject);
    }
}
