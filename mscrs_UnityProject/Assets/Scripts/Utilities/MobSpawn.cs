// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class MobSpawn : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    HealthEntity _mobPrefab;
    #endregion


    #region private protected vars
    HealthEntity _activeMob;
    bool _activeMobAlive = true;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    private void OnActiveMobDeath(int deathAnim)
    {
        _activeMobAlive = false;
    }
    #endregion


    #region mono events
    private IEnumerator Start()
    {
        for (;;)
        {
            yield return new WaitForSeconds(2f);
            _activeMob = Instantiate(_mobPrefab, transform.position, Quaternion.identity, this.transform) as HealthEntity;
            _activeMob.transform.SetParent(null);
            _activeMob.OnDeath += OnActiveMobDeath;
            _activeMobAlive = true;

            yield return new WaitWhile(() => { return _activeMobAlive; });
        }
    }    
    #endregion
}