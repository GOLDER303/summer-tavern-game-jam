using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static Action<OrderSO> OnOrderCreation;

    [SerializeField] private OrderSO[] orderSOs;
    [SerializeField] private OrderSpawner orderSpawner;

    public void CreateRandomOrder()
    {
        OrderSO orderSO = orderSOs[UnityEngine.Random.Range(0, orderSOs.Length)];
        orderSpawner.PrepareOrder(orderSO);

        OnOrderCreation?.Invoke(orderSO);
    }
}
