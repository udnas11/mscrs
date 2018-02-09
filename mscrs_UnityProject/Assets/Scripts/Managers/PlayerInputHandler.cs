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
    public event Action OnRoll;

    #region public serialised vars
    [SerializeField]
    KeyCode _jumpKey;
    [SerializeField]
    KeyCode _attackKey;
    [SerializeField]
    KeyCode _rollKey;
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

        //if (Input.GetKeyDown(_jumpKey))
        if (Input.GetButtonDown("Jump"))
        {
            if (OnJump != null)
                OnJump();
        }

        //if (Input.GetKeyDown(_attackKey))
        if (Input.GetButtonDown("Attack1"))
        {
            if (OnAttack != null)
                OnAttack();
        }

        //if (Input.GetKeyDown(_rollKey))
        if (Input.GetButtonDown("Roll"))
        {
            if (OnRoll != null)
                OnRoll();
        }
    }
    #endregion
}
