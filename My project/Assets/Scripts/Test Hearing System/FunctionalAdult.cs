using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Examples
{
    public class FunctionalAdult : MonoBehaviour, IHear
    {
        

        private void Start()
        {
           
            
        }

        private void Update()
        { 
            
        }
        public void RespondToSound(Sound sound)
        {
            print("Hey" + sound.pos.x);
        }

        
    }
}