using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderSpawner : MonoBehaviour
{
    [SerializeField] GameObject orderPrefab;

    private void OnEnable()
    {
        OrderManager.OnOrderCreation += OnOrderCreation;
    }

    private void OnDisable()
    {
        OrderManager.OnOrderCreation -= OnOrderCreation;
    }

    private void OnOrderCreation(OrderSO orderSO)
    {
        StartCoroutine(PrepareOrder(orderSO));
    }

    private IEnumerator PrepareOrder(OrderSO orderSO)
    {
        yield return new WaitForSeconds(orderSO.timeToPrepare);

        GameObject spawnedOrder = Instantiate(orderPrefab, transform.position, Quaternion.identity);
        spawnedOrder.GetComponent<Order>().orderSO = orderSO;
    }
}
