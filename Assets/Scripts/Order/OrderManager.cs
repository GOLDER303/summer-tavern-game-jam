using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static Action<OrderSO> OnOrderCreation;

    [SerializeField] public OrderSO[] orderSOs;

    public void CreateRandomOrder()
    {
        OrderSO orderSO = orderSOs[UnityEngine.Random.Range(0, orderSOs.Length)];
        OnOrderCreation?.Invoke(orderSO);
    }
}
