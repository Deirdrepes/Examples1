using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Examples
{
    public class Sound
    {
        public enum SoundType { Default = -1, Interesting, Dangerous };

        public Sound(Vector2 _pos, float _range, SoundType _type = SoundType.Default)
        {
            soundType = _type;

            pos = _pos;

            range = _range;
        }

        public readonly SoundType soundType;

        public readonly Vector2 pos;

        /// <summary>
        /// This the intensity of the sound.
        /// </summary>
        public readonly float range;
    }
}