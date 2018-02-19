using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    public event Action<float> OnHorizontalChange;
    public event Action OnJump;
    public event Action OnAttack;
    public event Action OnAttack2;
    public event Action OnRoll;

    #region public serialised vars
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    float _horizontal = 0f;
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        RegisterSingleton(this);
    }

    private void Update()
    {
        float newHorizontal = Input.GetAxisRaw("Horizontal");
        if (newHorizontal != _horizontal)
        {
            _horizontal = newHorizontal;
            if (OnHorizontalChange != null)// && _horizontal != 0f)
                OnHorizontalChange(_horizontal);
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            if (OnJump != null)
                OnJump();
        }
        
        if (Input.GetButtonDown("Attack1"))
        {
            if (OnAttack != null)
                OnAttack();
        }

        if (Input.GetButtonDown("Attack2"))
        {
            if (OnAttack2 != null)
                OnAttack2();
        }

        if (Input.GetButtonDown("Roll"))
        {
            if (OnRoll != null)
                OnRoll();
        }
    }
    #endregion
}
