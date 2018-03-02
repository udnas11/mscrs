// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class SwordSparks : MonoBehaviour
{

    [SerializeField]
    GameObject _toEnable;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        _toEnable.SetActive(true);
    }

}