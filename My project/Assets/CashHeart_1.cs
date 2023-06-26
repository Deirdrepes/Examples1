using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashHeart_1 : MonoBehaviour
{
    //[SerializeField] TextMeshPro m_TextMeshPro;
    [SerializeField] CashHeart_UI cash_UI;
    [SerializeField] GameObject gameObject1;
    [SerializeField] const int dropAmount = 1;
    [SerializeField] string tag1;
    void Start()
    {
        //cash_UI = FindObjectOfType<CashHeart_UI>();
        if(gameObject1 == null)
        {
             gameObject1 = GameObject.FindGameObjectWithTag(tag1);
        }
        cash_UI = gameObject1.GetComponent<CashHeart_UI>();     
    }

    
    void Update()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Player keep it");
            cash_UI.currentAmount += dropAmount;
            cash_UI.UpdateData();
            Destroy(gameObject);
        }
    }
}
