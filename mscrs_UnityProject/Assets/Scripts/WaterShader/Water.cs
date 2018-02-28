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
    #endregion


    #region private protected vars
    Material _material;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _material = GetComponent<SpriteRenderer>().material;
    }    

    void LateUpdate()
    {
        _material.SetVector("_ReflectPoint", Camera.main.WorldToViewportPoint(_reflectPoint.position));
        //_material.SetVector("_ReflectPoint", _reflectPoint.position - transform.position);
    }
    #endregion
}