using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public interface IItem
{
    string Path { get; set; }
    bool IsDestroyed { get; set; }
    void RemoverCloneNaming();
    void ItemKeeper(Collider2D collider);
    void InteractionInvoke();
}
