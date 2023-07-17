using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dronePrefab;

    public IEnumerator SpawnDrone(OrderSO orderSO, Vector3 dockingStationPosition)
    {
        yield return new WaitForSeconds(GetRandomDelay());
        GameObject droneGameObject = Instantiate(dronePrefab, transform.position, Quaternion.identity);

        Drone drone = droneGameObject.GetComponent<Drone>();

        drone.orderSO = orderSO;
        drone.dockingStationPosition = dockingStationPosition;
    }

    public IEnumerator SpawnDrone(OrderSO orderSO, int placeInQueue)
    {
        yield return new WaitForSeconds(GetRandomDelay());
        GameObject droneGameObject = Instantiate(dronePrefab, transform.position, Quaternion.identity);

        Drone drone = droneGameObject.GetComponent<Drone>();

        drone.orderSO = orderSO;
        drone.placeInQueue = placeInQueue;
    }

    private int GetRandomDelay()
    {
        return Random.Range(1, 4);
    }
}
