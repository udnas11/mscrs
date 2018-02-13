// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class SceneController : Singleton<SceneController>
{
    #region public serialised vars
    [SerializeField]
    string _unitSceneName;

    [HideInInspector]
    public PlayerController PlayerController;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    [ContextMenu("asdf")]
    public void RestartSubScenes()
    {
        if (string.IsNullOrEmpty(_unitSceneName) == false)
        {
            bool _loaded = false;
            for (int i = 0; i < SceneManager.sceneCount && !_loaded; i++)
                if (SceneManager.GetSceneAt(i).name == _unitSceneName)
                {
                    _loaded = true;
                    Debug.Log("found");
                }

            if (_loaded)
                SceneManager.UnloadSceneAsync(_unitSceneName);

            SceneManager.LoadScene(_unitSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        RegisterSingleton(this);

        if (string.IsNullOrEmpty(_unitSceneName) == false)
        {
            bool _loaded = false;
            for (int i = 0; i < SceneManager.sceneCount && !_loaded; i++)
                if (SceneManager.GetSceneAt(i).name == _unitSceneName)
                {
                    _loaded = true;
                }

            if (!_loaded)
                SceneManager.LoadScene(_unitSceneName, UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
    }
    #endregion
}
