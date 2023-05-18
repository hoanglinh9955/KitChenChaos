using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateSpawn;
    public event EventHandler OnPlateGrab;
    [SerializeField] private KitchenObjectSO plateSO;

    private float spawnPlateTimeMax = 4f;
    private float spawnPlateTime;
    private int plateSpawnAmountMax = 4;
    private int plateSpawnAmount;

    private void Update()
    {
        spawnPlateTime += Time.deltaTime;
        if(spawnPlateTime > spawnPlateTimeMax)
        {
            spawnPlateTime = 0f;

            if(plateSpawnAmount < plateSpawnAmountMax)
            {
                plateSpawnAmount++;

                OnPlateSpawn?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(PlayerController player)
    {
        //if player didnt has kitchen object
        if (!player.HasKitchenObject())
        {
            if(plateSpawnAmount > 0)
            {
                plateSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateSO, player);
                OnPlateGrab?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
