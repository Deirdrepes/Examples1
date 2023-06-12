using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor_Logic : MonoBehaviour
{
    [SerializeField] public float armorNumber;
    [SerializeField] SpriteRenderer armorRenderer;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        armorNumber = Mathf.RoundToInt(armorNumber);
        //armorRenderer = Sprite;
        
    }
}
