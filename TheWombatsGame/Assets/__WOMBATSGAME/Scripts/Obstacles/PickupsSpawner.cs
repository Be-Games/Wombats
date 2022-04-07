using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupsSpawner : MonoBehaviour
{
    public GameObject boostPrefab;
    public Transform[] boostPositions;

    public Collider[] boostInRange;

    public GameObject playerVehicle;
    public float rangeOfSphere;

    public LayerMask boostPickupsLayerMask;

    private void Start()
    {
        playerVehicle = LevelManager.Instance.currentPlayerCarModel;

        foreach (var x in boostPositions)
        {
            Instantiate(boostPrefab, x.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        
        boostInRange = Physics.OverlapSphere(playerVehicle.transform.position, rangeOfSphere,boostPickupsLayerMask);

        foreach (var boost in boostInRange)
        {
            boost.gameObject.SetActive(false);
        }
    }
}
