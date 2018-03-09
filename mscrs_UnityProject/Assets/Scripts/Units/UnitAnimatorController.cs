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
    [SerializeField]
    int _attackAnimCount = 1;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public virtual void AttackCustomAnim(int customAnimation = -1)
    {
        if (GetPhaseState(EAnimationPhase.Attacking) == false)
        {
            if (customAnimation < 0)
                _animator.SetFloat("indexAttackAnim", UnityRandom.Range(0, _attackAnimCount));
            else
                _animator.SetFloat("indexAttackAnim", customAnimation);
            Attack();
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