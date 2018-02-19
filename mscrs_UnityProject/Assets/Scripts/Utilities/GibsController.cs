// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class GibsController : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Rigidbody2D[] _gibParts;
    [SerializeField]
    float _autodestroyTime;

    [SerializeField, Space]
    float _fromCenterImpulse;
    [SerializeField]
    Vector2 _lateralImpulse;
    [SerializeField]
    Vector2 _torqueRange;
    #endregion


    #region private protected vars
    bool _flipX;
    #endregion


    #region pub methods
    public void SetFlipX(bool newValue)
    {
        _flipX = newValue;
    }
    #endregion


    #region private protected methods
    void InitPhysics()
    {
        int gibCount = _gibParts.Length;
        float a;
        float aRandomRange = (Mathf.PI * 2f) / (float)gibCount;
        float aRandom;
        Vector2 force = Vector2.zero;
        for (int i = 0; i < gibCount; i++)
        {
            a = (Mathf.PI * 2f) * (float)i / gibCount;
            aRandom = UnityRandom.Range(0f, aRandomRange);
            force.x = Mathf.Sin(a + aRandom) * _fromCenterImpulse;
            force.y = Mathf.Cos(a + aRandom) * _fromCenterImpulse;

            force.x += _lateralImpulse.x;
            force.y += _lateralImpulse.y;

            force.x *= _flipX ? -1f : 1f;

            _gibParts[i].AddForce(force, ForceMode2D.Impulse);
            _gibParts[i].AddTorque(UnityRandom.Range(_torqueRange.x, _torqueRange.y), ForceMode2D.Impulse);
        }
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        Destroy(this.gameObject, _autodestroyTime);
    }

    private void Start()
    {
        InitPhysics();
    }
    #endregion
}