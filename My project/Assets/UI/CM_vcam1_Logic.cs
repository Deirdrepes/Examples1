using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CM_vcam1_Logic : MonoBehaviour
{
    public GameObject playerAvaible;
    CinemachineVirtualCamera cinemachineVirtualCamera;
    // Start is called before the first frame update
    void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        playerAvaible = GameObject.FindGameObjectWithTag("Player"); 
        cinemachineVirtualCamera.LookAt = playerAvaible.transform;
        cinemachineVirtualCamera.Follow = playerAvaible.transform;
       
    }
}
