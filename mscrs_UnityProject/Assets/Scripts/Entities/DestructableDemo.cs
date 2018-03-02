// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class DestructableDemo : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Transform _spawnOnDestroy;
    #endregion


    #region private protected vars
    HealthEntity _healthEntity;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    private void OnDeath(int obj)
    {
        var newInst = Instantiate(_spawnOnDestroy, this.transform.position, Quaternion.identity) as Transform;
        Destroy(gameObject);
    }
    #endregion


    #region mono events
    private void Awake()
    {
        _healthEntity = GetComponent<HealthEntity>();
        _healthEntity.OnDeath += OnDeath;
    }
    #endregion
}