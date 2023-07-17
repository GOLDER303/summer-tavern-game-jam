using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingStationsManager : MonoBehaviour
{
    [SerializeField] private Vector3[] dockingStationsPositions;

    private HashSet<Vector3> occupiedDockingStations = new HashSet<Vector3>();

    public Vector3 GetFreeDockingStation()
    {
        if (occupiedDockingStations.Count < dockingStationsPositions.Length)
        {
            foreach (Vector3 dockingStationPosition in dockingStationsPositions)
            {
                if (!occupiedDockingStations.Contains(dockingStationPosition))
                {
                    occupiedDockingStations.Add(dockingStationPosition);
                    return dockingStationPosition;
                }
            }
        }

        throw new System.Exception("No free docking station");
    }

    public bool IsFreeStationAvailable()
    {
        return occupiedDockingStations.Count < dockingStationsPositions.Length;
    }

    public void FreeDockingStation(Drone drone)
    {
        occupiedDockingStations.Remove(drone.dockingStationPosition);
    }
}
