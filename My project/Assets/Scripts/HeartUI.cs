using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public Transform heart1;
    public Transform heart2;
    public Transform heart3;

    [SerializeField]private int firstValue;
    [SerializeField]private int secondValue;

    [SerializeField] private Health health; 
    [SerializeField] private GameObject gameObject1;
    // Start is called before the first frame update
    void Start()
    {
        heart1.gameObject.SetActive(true);
        heart2.gameObject.SetActive(false);
        heart3.gameObject.SetActive(false);     
    }

    // Update is called once per frame
    void Update()
    {
        UILogic();    
    }

    private void UILogic()
    {
        gameObject1 = GameObject.FindGameObjectWithTag("Player");
        health = gameObject1.GetComponent<Health>();

        if (firstValue >= health.healthPoints)
        {
            heart1.gameObject.SetActive(false);
            heart2.gameObject.SetActive(true);
            if (firstValue <= health.healthPoints)
            {
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
            }
        }
        if (secondValue >= health.healthPoints)
        {
            heart2.gameObject.SetActive(false);
            heart3.gameObject.SetActive(true);
            if (secondValue <= health.healthPoints)
            {
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
            }
        }
    }
}
