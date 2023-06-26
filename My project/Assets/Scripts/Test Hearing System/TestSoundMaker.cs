using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples
{
    public class TestSoundMaker : MonoBehaviour
    {
        [SerializeField] private AudioSource source = null;

        [SerializeField] private float soundRange = 25f;

        [SerializeField] GameObject phisicalBodyForSound;

        [SerializeField] private Sound.SoundType soundType = Sound.SoundType.Dangerous;

        private void OnMouseDown()
        {
            //if (source.isPlaying) //If already playing a sound, don't allow overlapping sounds 
            //    return;

            //source.Play();

            //var sound = new Sound(new Vector2(), soundRange, soundType);
            
            Instantiate(phisicalBodyForSound, gameObject.transform);

            //Sounds.MakeSound(sound);
        }
    }
}
