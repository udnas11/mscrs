// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "AssetDatabase", menuName = "Global Asset Database")]
public class AssetDatabaseSO : ScriptableObject
{

    public PlayerController PlayerPrefab;
    public CameraSettingsSO CameraSettings;


    #region pseudo singleton
    static private AssetDatabaseSO _instance;
    static public AssetDatabaseSO Instance
    {
        get
        {
            if (_instance == null)
                _instance = Resources.Load<AssetDatabaseSO>("AssetDatabase");
            Assert.IsNotNull(_instance, "AssetDatabase could not be loaded!");
            return _instance;
        }
    }
    #endregion

}