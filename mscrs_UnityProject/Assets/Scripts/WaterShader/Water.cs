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
    //[SerializeField, Range(0f, 0.3f)]
    //float _bumpMultiplier;
    [SerializeField]
    Vector2 _bumpMultiplier;
    [SerializeField]
    Vector2 _bumpSpeed;

    [SerializeField, Header("Bump maps Distress")]
    Texture2D _bumpTextureDistress;
    [SerializeField]
    Vector2 _bumpTextureScaleDistress;
    [SerializeField]
    Vector2 _bumpMultiplierDistress;
    [SerializeField]
    AnimationCurve _distressCurve;
    [SerializeField]
    float _distressDuration;

    [SerializeField, Header("Others"), Range(0.25f, 2f)]
    float _scale = 1f;
    [SerializeField, Range(0.25f, 3f)]
    float _exposure = 1f;
    [SerializeField, Range(0f, 2f)]
    float _saturation = 1f;
    #endregion


    #region private protected vars
    Material _material;
    //bool[] _distressAvailable = new bool[4];
    Vector4[] _distressInfo = new Vector4[4];
    Coroutine[] _distressCoroutines = new Coroutine[4];
    int _lastDistress;
    #endregion


    #region pub methods
    public void ApplyDistress(Vector3 worldPos, float range)
    {
        if (++_lastDistress == 4)
            _lastDistress = 0;

        if (_distressCoroutines[_lastDistress] != null)
            StopCoroutine(_distressCoroutines[_lastDistress]);
        _distressCoroutines[_lastDistress] = StartCoroutine(CApplyDistress(_lastDistress, worldPos, range));
    }

    IEnumerator CApplyDistress(int slot, Vector3 worldPos, float range)
    {
        float timeStart = Time.time;
        float timeEnd = timeStart + _distressDuration;
        float t;
        Vector4 info = _distressInfo[slot]; //posx, posy, mult, range
        //Vector3 viewPos = Camera.main.WorldToViewportPoint(worldPos);
        
        while (Time.time < timeEnd)
        {
            t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            info.x = worldPos.x;
            info.y = worldPos.y;
            info.w = range;
            info.z = _distressCurve.Evaluate(t);
            _distressInfo[slot] = info;
            yield return null;
        }
        info.z = 0f;
        _distressInfo[slot] = info;
    }

    void ApplyDistressInfo()
    {
        _material.SetVectorArray("_DistressInfo", _distressInfo);
    }
    #endregion


    #region private protected methods
    private void Init()
    {
        //_material.SetFloat("_BumpMult", _bumpMultiplier);
        _material.SetVector("_BumpMult", _bumpMultiplier);
        _material.SetVector("_BumpSpeed", _bumpSpeed);
        _material.SetFloat("_ReflectScale", _scale);
        _material.SetTexture("_BumpTex", _bumpTexture);
        _material.SetTextureScale("_BumpTex", _bumpTextureScale);
        _material.SetTextureOffset("_BumpTex", _bumpTextureOffset);
        _material.SetTexture("_BumpMask", _bumpMask);
        _material.SetFloat("_Exposure", _exposure);
        _material.SetFloat("_Saturation", _saturation);

        _material.SetTexture("_BumpTexDistress", _bumpTextureDistress);
        _material.SetTextureScale("_BumpTexDistress", _bumpTextureScaleDistress);
        _material.SetVector("_BumpMultDistress", _bumpMultiplierDistress);
        //_material.SetFloat("_BumpMultDistress", _bumpMultiplierDistress);
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

    private void Update()
    {
        ApplyDistressInfo();
    }

    void LateUpdate()
    {
        _material.SetVector("_ReflectPoint", Camera.main.WorldToViewportPoint(_reflectPoint.position));
        Init(); // to be removed later
    }
    #endregion
}