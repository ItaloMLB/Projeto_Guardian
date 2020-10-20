using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeeperEvent : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip attackSound;
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void KeeperAttackSound()
    {

        audioSource.PlayOneShot(attackSound);
    
    
    }
}
