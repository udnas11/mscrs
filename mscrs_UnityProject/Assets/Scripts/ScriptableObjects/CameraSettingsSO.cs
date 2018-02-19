// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "Camera Settings Container")]
public class CameraSettingsSO : ScriptableObject
{
    public float AddHeight;
    public float LerpSpeed;

    public float PlayerVelocityMultiplier;
    public float PlayerVelocityLerpSpeed;
}