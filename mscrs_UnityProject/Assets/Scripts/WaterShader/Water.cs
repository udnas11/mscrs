// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class Water : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Transform _reflectPoint;
    [SerializeField, Range(0f, 0.5f)]
    float _bumpMultiplier;
    [SerializeField]
    Vector2 _bumpSpeed;
    #endregion


    #region private protected vars
    Material _material;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    private void Init()
    {
        _material.SetFloat("_BumpMult", _bumpMultiplier);
        _material.SetVector("_BumpSpeed", _bumpSpeed);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
        Init();
    }    

    void LateUpdate()
    {
        _material.SetVector("_ReflectPoint", Camera.main.WorldToViewportPoint(_reflectPoint.position));
        Init(); // to be removed later
    }
    #endregion
}