using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupsSpawner : MonoBehaviour
{
    
    public Collider[] totalBoosts;
    public Collider[] boostInRange;
    public Collider[] boostOutRange;

    public GameObject playerVehicle;
    public float rangeOfSphere;

    public LayerMask boostPickupsLayerMask;

    private void Start()
    {
        playerVehicle = LevelManager.Instance.playerVisual;
        
    }

    private void FixedUpdate()
    {
        
        boostInRange = Physics.OverlapSphere(playerVehicle.transform.position, rangeOfSphere,boostPickupsLayerMask);
    
        foreach (var boost in boostInRange)
        {
            boost.gameObject.SetActive(true);
        }

        boostOutRange = totalBoosts.Except(boostInRange).ToArray();
        
        foreach (var boost in boostOutRange)
        {
            boost.gameObject.SetActive(false);
        }
    }
}
