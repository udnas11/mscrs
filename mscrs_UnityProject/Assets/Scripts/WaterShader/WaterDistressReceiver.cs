// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class WaterDistressReceiver : MonoBehaviour
{
    #region static
    static private List<WaterDistressReceiver> _receiverList = new List<WaterDistressReceiver>();
    static public void WaterDistressApply(WaterDistressEvent transmitter)
    {
        foreach (WaterDistressReceiver receiver in _receiverList)
            receiver.ApplyDistress(transmitter);
    }
    #endregion


    #region public serialised vars
    #endregion


    #region private protected vars
    Water _water;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    void ApplyDistress(WaterDistressEvent receiver)
    {
        _water.ApplyDistress(receiver.transform.position, receiver.Range);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _water = GetComponent<Water>();
    }

    private void OnEnable()
    {
        _receiverList.Add(this);
    }

    private void OnDisable()
    {
        _receiverList.Remove(this);
    }
    #endregion
}