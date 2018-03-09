// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class AudioManager : MonoBehaviour
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
    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 150, 100, 150, 25), "Toggle Audio"))
            AudioController.MuteSound(!AudioController.IsSoundMuted());
    }
    #endregion
}