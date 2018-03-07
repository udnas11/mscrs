// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class UnitAnimatorController : BaseAnimatorController
{
    #region public serialised vars
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public override void Attack()
    {
        if (GetPhaseState(EAnimationPhase.Attacking) == false)
        {
            _animator.SetFloat("indexAttackAnim", UnityRandom.Range(0, 2));
            base.Attack();
        }
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    #endregion
}