using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OntrashGrab;

    new public static void ResetStaticData()
    {
        OntrashGrab = null;
    }
    public override void Interact(PlayerController player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf(); 
            OntrashGrab?.Invoke(this, EventArgs.Empty);
        }
    }
}
