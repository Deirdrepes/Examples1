using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeUI : MonoBehaviour
{
    [SerializeField] Current_Weapon current_Weapon;
    [SerializeField] Image children;
    [SerializeField] Sprite defaultSpriteUI;
    [SerializeField] private GameObject gameObject1;
    // Start is called before the first frame update
    void Start()
    {
        //var b = FindObjectsByType<Current_Weapon>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).GetValue(2);
        gameObject1 = GameObject.FindGameObjectWithTag("Player");
        //current_Weapon = (Current_Weapon)b;
    }

    // Update is called once per frame
    void Update()
    {
        //Sprite sprite = children.GetComponent<Image>().sprite;
        //children.sprite = current_Weapon.weaponKnife.GetComponent<SpriteRenderer>().sprite;
        
        
        
    }

    private void OnGUI()
    {   
        if(children.sprite == null)
        {
            children.sprite = defaultSpriteUI;
        }
        if (gameObject1)
        {
            var b = FindObjectsByType<Current_Weapon>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).GetValue(0);

            current_Weapon = (Current_Weapon)b;
            children.sprite = current_Weapon.weaponKnife.SpriteUI;
        }
        else
        {
            children.sprite = null;
            current_Weapon = null;
            gameObject1 = GameObject.FindGameObjectWithTag("Player");
        }
        
    }
}
