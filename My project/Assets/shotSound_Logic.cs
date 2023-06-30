using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotSound_Logic : MonoBehaviour
{
    AudioSource m_AudioSource;
    public float clipLength { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        m_AudioSource = GetComponentInParent<AudioSource>();
        m_AudioSource.Play();
        clipLength = m_AudioSource.clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        
        print(clipLength);

        Invoke("InteractionInvoke", 0.15f);

        //must be "clipLength"
        Invoke("InteractionDestroyInvoke", clipLength);
    }
    public void InteractionDestroyInvoke()
    {
        Destroy(transform.parent.gameObject);
    }
    public void InteractionInvoke()
    {
        gameObject.SetActive(false);
    }
}
