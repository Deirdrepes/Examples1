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
    void Update()
    {
        playerAvaible = GameObject.FindGameObjectWithTag("Player");
        cinemachineVirtualCamera.Follow = playerAvaible.transform;
        cinemachineVirtualCamera.LookAt = playerAvaible.transform;
    }
}
