// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class SlowMoController : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    KeyCode _slowMoButton;
    #endregion


    #region private protected vars
    bool _slowMo = false;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Update()
    {
        if (Input.GetKeyDown(_slowMoButton))
        {
            _slowMo = !_slowMo;
            Time.timeScale = _slowMo ? 0.2f : 1f;
        }
    }
    #endregion
}