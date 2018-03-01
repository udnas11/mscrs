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

    [SerializeField, Header("Bump maps")]
    Texture2D _bumpTexture;
    [SerializeField]
    Vector2 _bumpTextureScale;
    [SerializeField]
    Vector2 _bumpTextureOffset;
    [SerializeField]
    Texture2D _bumpMask;
    [SerializeField, Range(0f, 0.5f)]
    float _bumpMultiplier;
    [SerializeField]
    Vector2 _bumpSpeed;

    [SerializeField, Header("Others"), Range(0.25f, 2f)]
    float _scale = 1f;
    [SerializeField, Range(0.25f, 3f)]
    float _exposure = 1f;
    [SerializeField, Range(0f, 2f)]
    float _saturation = 1f;
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
        _material.SetFloat("_ReflectScale", _scale);
        _material.SetTexture("_BumpTex", _bumpTexture);
        _material.SetTextureScale("_BumpTex", _bumpTextureScale);
        _material.SetTextureOffset("_BumpTex", _bumpTextureOffset);
        _material.SetTexture("_BumpMask", _bumpMask);
        _material.SetFloat("_Exposure", _exposure);
        _material.SetFloat("_Saturation", _saturation);
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