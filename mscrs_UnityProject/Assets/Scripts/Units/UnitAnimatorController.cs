// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class UnitAnimatorController : MonoBehaviour
{
    #region public serialised vars
    #endregion


    #region private protected vars
    Animator _animator;
    #endregion


    #region pub methods
    public void SetRunning(bool newState)
    {
        _animator.SetBool("isRunning", newState);
    }

    public void Attack()
    {
        _animator.SetTrigger("doAttack");
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    #endregion
}