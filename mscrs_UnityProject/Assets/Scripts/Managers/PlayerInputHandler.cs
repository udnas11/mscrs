using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    public event Action<float> OnHorizontalChange;
    public event Action OnJump;
    public event Action OnAttack1;
    public event Action OnAttack2;
    public event Action OnAttack1Charged;
    public event Action OnAttack2Charged;
    public event Action OnRoll;

    #region public serialised vars
    public const float cHoldDuration = 0.3f;
    #endregion


    #region private protected vars
    float _horizontal = 0f;
    Dictionary<string, float> _buttonHoldTimes;
    List<string> _buttonHoldActiveList;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        RegisterSingleton(this);
        _buttonHoldTimes = new Dictionary<string, float>();
        _buttonHoldActiveList = new List<string>();
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
        /*
        if (Input.GetButtonDown("Attack1"))
        {
            if (OnAttack1 != null)
                OnAttack1();
        }*/

        ////

        if (Input.GetButtonDown("Attack1"))
        {
            _buttonHoldTimes["Attack1"] = Time.time;
        }
        if (Input.GetButton("Attack1"))
        {
            float time = Time.time - _buttonHoldTimes["Attack1"];
            if (time > cHoldDuration && _buttonHoldActiveList.Contains("Attack1") == false)
            {
                _buttonHoldActiveList.Add("Attack1");
                if (OnAttack1Charged != null)
                    OnAttack1Charged();
            }
        }
        if (Input.GetButtonUp("Attack1"))
        {
            _buttonHoldActiveList.Remove("Attack1");

            float time = Time.time - _buttonHoldTimes["Attack1"];
            if (time < cHoldDuration)
            {
                if (OnAttack1 != null)
                    OnAttack1();
            }
        }

        /////

        if (Input.GetButtonDown("Attack2"))
        {
            _buttonHoldTimes["Attack2"] = Time.time;
        }
        if (Input.GetButton("Attack2"))
        {
            float time = Time.time - _buttonHoldTimes["Attack2"];
            if (time > cHoldDuration && _buttonHoldActiveList.Contains("Attack2") == false)
            {
                _buttonHoldActiveList.Add("Attack2");
                if (OnAttack2Charged != null)
                    OnAttack2Charged();
            }
        }
        if (Input.GetButtonUp("Attack2"))
        {
            _buttonHoldActiveList.Remove("Attack2");

            float time = Time.time - _buttonHoldTimes["Attack2"];
            if (time < cHoldDuration)
            {
                if (OnAttack2 != null)
                    OnAttack2();
            }
        }

        /////

        if (Input.GetButtonDown("Roll"))
        {
            if (OnRoll != null)
                OnRoll();
        }
    }
    #endregion
}
