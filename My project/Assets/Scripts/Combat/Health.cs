using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEditor;
using UnityEngine.PlayerLoop;
using System.ComponentModel;

public class Health : MonoBehaviour
{
    [SerializeField] public float healthPoints = 100f;
    [SerializeField] public float maxHealthPoints;
    // Start is called before the first frame update
    bool isDead = false;

    Armor_Logic armorLogic;

    [SerializeField] List<GameObject> dropList;
 
    public void Start()
    {
        //healthPoints = maxHealthPoints;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            healthPoints = healthPoints - 5f;
        }
    }

    [SerializeField] SpriteRenderer spriteRenderer;

    public bool IsDead()
    {
        return isDead;
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("Hited");
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        healthPoints = Mathf.Max((float)(healthPoints - damage * 1), 0);
        if (healthPoints == 0)
        {
            Die();
        }
        spriteRenderer.color = Color.red;
        Invoke("ReturnDefaultColor", 0.3f);
    }

    private SpriteRenderer ReturnDefaultColor()
    {
        spriteRenderer.color = Color.white;
        return spriteRenderer;
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        gameObject.SetActive(false);
        DropList(dropList);
    }

    private void DropList(List<GameObject> list)
    {
        int r = Random.RandomRange(0, 99);
        Debug.Log(r);

        list = dropList;
        foreach (GameObject item in list)
        {           
            Instantiate(item, new Vector3(transform.position.x, transform.position.y, transform.position.z),
                new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }   
    }
}
