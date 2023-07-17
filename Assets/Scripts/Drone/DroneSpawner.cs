using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public static Action<Drone> OnDroneSpawned;

    [SerializeField] private GameObject dronePrefab;

    public void SpawnDrone(OrderSO orderSO, Vector3 dockingStationPosition)
    {
        GameObject droneGameObject = Instantiate(dronePrefab, transform.position, Quaternion.identity);

        Drone drone = droneGameObject.GetComponent<Drone>();

        drone.orderSO = orderSO;
        drone.dockingStationPosition = dockingStationPosition;

        OnDroneSpawned?.Invoke(drone);
    }

    public void SpawnDrone(OrderSO orderSO, int placeInQueue)
    {
        GameObject droneGameObject = Instantiate(dronePrefab, transform.position, Quaternion.identity);

        Drone drone = droneGameObject.GetComponent<Drone>();

        drone.orderSO = orderSO;
        drone.placeInQueue = placeInQueue;

        OnDroneSpawned?.Invoke(drone);
    }
}
