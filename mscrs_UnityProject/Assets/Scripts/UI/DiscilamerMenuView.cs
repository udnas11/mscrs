// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class DiscilamerMenuView : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    UnityEngine.UI.Button _onPlayButton;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    void OnPlayButtonPressed()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel", UnityEngine.SceneManagement.LoadSceneMode.Single);
    }
    #endregion


    #region mono events
    private void Awake()
    {
        _onPlayButton.onClick.AddListener(OnPlayButtonPressed);
    }
    #endregion
}