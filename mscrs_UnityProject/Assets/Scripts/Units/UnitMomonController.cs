// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class UnitMomonController : UnitController
{
    #region public serialised vars
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public override void Attack(Vector2 targetPos)
    {
        float dist = Vector2.Distance(targetPos, transform.position);
        _unitPawn.Attack(targetPos, dist > 0.6f ? 0 : 1);
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    #endregion
}