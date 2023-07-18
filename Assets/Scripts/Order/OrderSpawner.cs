using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour
{
    [SerializeField] private GameObject orderPrefab;
    [SerializeField] private float spawnRadius = 2f;

    public void PrepareOrder(OrderSO orderSO)
    {

        Vector3 spawnLocation = Random.insideUnitCircle.normalized * spawnRadius + new Vector2(transform.position.x, transform.position.y);

        GameObject spawnedOrder = Instantiate(orderPrefab, spawnLocation, Quaternion.identity);
        spawnedOrder.GetComponent<Order>().orderSO = orderSO;
    }
}
