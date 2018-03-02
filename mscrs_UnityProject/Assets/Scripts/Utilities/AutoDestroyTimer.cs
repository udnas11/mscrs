// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class AutoDestroyTimer : MonoBehaviour
{
    [SerializeField]
    float _time;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_time);
        Destroy(gameObject);
    }
}