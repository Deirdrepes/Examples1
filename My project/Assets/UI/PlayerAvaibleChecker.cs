using Examples.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvaibleChecker : MonoBehaviour
{
    public GameObject playerAvaible;
        
    public HeartUI heartUI;
    public HeartUI heartUI1;
    public HeartUI heartUI2;
    public HeartUI heartUI3;
    public HeartUI heartUI4;

    public PlayerMove playerMove;
    public Health health;
    public Fighter fighter;
    
    [SerializeField] private GameObject player;
    [SerializeField] private Vector2 spawnPoint;
    // Start is called before the first frame update
    void Start()
    {
        playerAvaible = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerIsAlive())
        {
            //Invoke("Respawn", 3f);
            Respawn();
        }
    }

    public bool PlayerIsAlive()
    {
        playerAvaible = GameObject.FindGameObjectWithTag("Player");
        return playerAvaible != null;
    }

    public void Respawn()
    {
        Instantiate(player,spawnPoint, new Quaternion());

        heartUI.heart1.gameObject.SetActive(true);
        heartUI.heart2.gameObject.SetActive(false);
        heartUI.heart3.gameObject.SetActive(false);

        heartUI1.heart1.gameObject.SetActive(true);
        heartUI1.heart2.gameObject.SetActive(false);
        heartUI1.heart3.gameObject.SetActive(false);

        heartUI2.heart1.gameObject.SetActive(true);
        heartUI2.heart2.gameObject.SetActive(false);
        heartUI2.heart3.gameObject.SetActive(false);

        heartUI3.heart1.gameObject.SetActive(true);
        heartUI3.heart2.gameObject.SetActive(false);
        heartUI3.heart3.gameObject.SetActive(false);

        heartUI4.heart1.gameObject.SetActive(true);
        heartUI4.heart2.gameObject.SetActive(false);
        heartUI4.heart3.gameObject.SetActive(false);
    }
}
