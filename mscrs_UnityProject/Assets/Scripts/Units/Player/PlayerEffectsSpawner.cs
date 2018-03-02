// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class PlayerEffectsSpawner : MonoBehaviour
{
    #region public serialised vars
    #endregion


    #region private protected vars
    PlayerController _playerController;
    #endregion


    #region pub methods
    public void SpawnEffect(Transform prefab)
    {
        var newInst = Instantiate(prefab, this.transform.position, Quaternion.identity) as Transform;
        if (_playerController.IsFlippedX)
        {
            Vector3 newScale = newInst.localScale;
            newScale.x *= -1f;
            newInst.localScale = newScale;
        }
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    #endregion
}