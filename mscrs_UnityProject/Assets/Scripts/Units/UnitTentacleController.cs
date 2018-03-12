// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class UnitTentacleController : UnitController
{
    #region public serialised vars
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    protected override void Update()
    {
        base.Update();

        Vector2 deltaPos = SceneController.Instance.PlayerControllerInstance.transform.position - transform.position;
        if (Mathf.Abs(deltaPos.x) < 1f && Mathf.Abs(deltaPos.y) < 0.25f)
        {
            _unitPawn.Jump();
        }
    }
    #endregion
}