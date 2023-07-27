using Examples;
using Examples.Combat;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions.Must;

public interface Weapon
{
    public bool IsDestroyed { get; set; }
    public string Path { get; set; }
    public float Damage { get; set; }
    public float MeleeRange { get; set; }

    public GameObject bulletOfRangeWeapon { get; set; }

    [SerializeField] public AnimationClip animationClip { get; set; }
}
