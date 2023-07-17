using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{
    public static Action<Vector3> OnDockingStationFreeing;

    [SerializeField] private DroneSpawner droneSpawner;
    [SerializeField] private DockingStationsManager dockingStationsManager;
    [SerializeField] private Vector3 queueStartPosition;

    private int numberOfDronesInQueue = 0;

    private void OnEnable()
    {
        OrderManager.OnOrderCreation += OnOrderCreation;
        Drone.OnDroneGetOrder += OnDroneGetOrder;
    }

    private void OnDisable()
    {
        OrderManager.OnOrderCreation -= OnOrderCreation;
        Drone.OnDroneGetOrder -= OnDroneGetOrder;
    }

    private void OnOrderCreation(OrderSO orderSO)
    {
        if (!dockingStationsManager.IsFreeStationAvailable())
        {
            numberOfDronesInQueue++;
            StartCoroutine(droneSpawner.SpawnDrone(orderSO, numberOfDronesInQueue));
        }
        else
        {
            Vector3 dockingStationPosition = dockingStationsManager.GetFreeDockingStation();
            StartCoroutine(droneSpawner.SpawnDrone(orderSO, dockingStationPosition));
        }
    }

    private void OnDroneGetOrder()
    {
        numberOfDronesInQueue--;
        OnDockingStationFreeing?.Invoke(dockingStationsManager.GetFreeDockingStation());
    }
}
