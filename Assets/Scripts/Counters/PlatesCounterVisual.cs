using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform CounterTopPoint;
    [SerializeField] private Transform plateCounterPrefabs;

    private List<GameObject> plateListVisual;
    private void Awake()
    {
        plateListVisual = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateGrab += PlatesCounter_OnPlateGrab;
    }

    private void PlatesCounter_OnPlateGrab(object sender, System.EventArgs e)
    {
        GameObject choosePlate = plateListVisual[plateListVisual.Count - 1];
        plateListVisual.Remove(choosePlate);
        Destroy(choosePlate);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        Transform plateTransform = Instantiate(plateCounterPrefabs, CounterTopPoint);
        float plateOffsetY = .1f;
        plateTransform.localPosition = new Vector3(0, plateOffsetY * plateListVisual.Count, 0);

        plateListVisual.Add(plateTransform.gameObject);
    }
}
