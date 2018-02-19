// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class EnemyPlayerMirrorController : UnitController
{
    #region public serialised vars
    [SerializeField, Header("Player Mirror")]
    GibsController _gibsPrefab;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public void SpawnGibs()
    {
        var gibs = Instantiate(_gibsPrefab, _behaviourRaycastTarget.position, Quaternion.identity) as GibsController;
        gibs.SetFlipX(!_unitPawn.IsFlipX);
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    #endregion
}
