using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager : MonoBehaviour
{

    [SerializeField] private DroneSpawner droneSpawner;
    [SerializeField] private DockingStationsManager dockingStationsManager;
    [SerializeField] private Vector3 queueStartPosition;

    private Queue<Drone> droneQueue = new Queue<Drone>();

    private void OnEnable()
    {
        OrderManager.OnOrderCreation += HandleOrderCreation;
        Drone.OnDroneFreeingDockingStation += HandleDroneFreeingDockingStation;
        DroneSpawner.OnDroneSpawned += HandleDroneSpawned;
    }

    private void OnDisable()
    {
        OrderManager.OnOrderCreation -= HandleOrderCreation;
        Drone.OnDroneFreeingDockingStation -= HandleDroneFreeingDockingStation;
        DroneSpawner.OnDroneSpawned -= HandleDroneSpawned;
    }

    private void HandleOrderCreation(OrderSO orderSO)
    {
        if (!dockingStationsManager.IsFreeStationAvailable())
        {
            droneSpawner.SpawnDrone(orderSO, droneQueue.Count + 1);
        }
        else
        {
            Vector3 dockingStationPosition = dockingStationsManager.GetFreeDockingStation();
            droneSpawner.SpawnDrone(orderSO, dockingStationPosition);
        }
    }

    private void HandleDroneFreeingDockingStation(Drone drone)
    {
        dockingStationsManager.FreeDockingStation(drone);

        if (droneQueue.Count > 0)
        {
            Drone queuedDrone = droneQueue.Dequeue();
            queuedDrone.dockingStationPosition = dockingStationsManager.GetFreeDockingStation();
        }
    }

    private void HandleDroneSpawned(Drone drone)
    {
        if (drone.placeInQueue > 0)
        {
            droneQueue.Enqueue(drone);
        }
    }
}
