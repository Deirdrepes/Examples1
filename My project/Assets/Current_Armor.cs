using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class Current_Armor : MonoBehaviour
{
    public Armor_Logic armor;
    [SerializeField] private Armor_Logic defaultArmor;
    // Start is called before the first frame update
    void Start()
    {
        armor = GetComponent<Armor_Logic>();
        defaultArmor = Resources.Load<Armor_Logic>("Default_Armor_");        
        armor = defaultArmor;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
