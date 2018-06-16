// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class WaterDistressEvent : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    float _range = 1f;

    [SerializeField]
    bool _onEnable;

    public float Range { get { return _range; } }
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    void TriggerThis()
    {
        WaterDistressReceiver.WaterDistressApply(this);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void OnEnable()
    {
        if (_onEnable)
            TriggerThis();
    }
    #endregion
}