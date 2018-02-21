// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class CameraShaker : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    AnimationCurve _animX = AnimationCurve.Linear(0f, 0f, 1f, 0f);
    [SerializeField]
    AnimationCurve _animY = AnimationCurve.Linear(0f, 0f, 1f, 0f);
    [SerializeField]
    float _duration;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public void Apply()
    {
        CameraController.Instance.ApplyCameraShake(_animX, _animY, _duration);
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    #endregion
}