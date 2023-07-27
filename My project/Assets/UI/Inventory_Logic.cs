using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_Logic : MonoBehaviour
{
    IItem item;
    [SerializeField] public Dictionary<int, GameObject> inventoryStucture;
    [SerializeField] public List<GameObject> items;
    [SerializeField] public GameObject[] gameObjects;
    // Start is called before the first frame update
    void Start()
    {
        gameObjects = new GameObject[6];
        //inventoryStucture.Add(0, 2);
        print(inventoryStucture);
        //items.Add(item);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
