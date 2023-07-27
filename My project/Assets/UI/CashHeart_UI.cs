using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CashHeart_UI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_TextMeshPro;
    [SerializeField] public int currentAmount;
    // Start is called before the first frame update
    
    void Start()
    {
        currentAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateData();
    }

    public void UpdateData()
    {
        m_TextMeshPro.text = currentAmount.ToString();     
    }
}
