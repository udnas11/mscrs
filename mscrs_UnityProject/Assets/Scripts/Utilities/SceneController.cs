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

    public Action<PlayerController> OnPlayerSpawned;

    #region public serialised vars
    [SerializeField]
    string _unitSceneName;
    [SerializeField]
    Transform _playerSpawnPos;

    [SerializeField, Header("DEBUG TO BE REMOVED")]
    bool _loadUnits = true;
    [SerializeField]
    bool _loadPlayer = true;

    [HideInInspector]
    public PlayerController PlayerControllerInstance;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    [ContextMenu("Respawn Player")]
    public void RespawnPlayer()
    {
        if (_loadPlayer == false)
            return;

        if (PlayerControllerInstance != null)
        {
            PlayerControllerInstance.HealthEntity.OnDeath -= OnPlayerDeath;
            Destroy(PlayerControllerInstance.gameObject);
        }

        PlayerControllerInstance = Instantiate(AssetDatabaseSO.Instance.PlayerPrefab, _playerSpawnPos.position, Quaternion.identity) as PlayerController;
        PlayerControllerInstance.HealthEntity.OnDeath += OnPlayerDeath;

        if (OnPlayerSpawned != null)
            OnPlayerSpawned(PlayerControllerInstance);
    }

    [ContextMenu("Respawn enemies")]
    public void RestartUnitsScene()
    {
        if (_loadUnits == false)
            return;

        if (string.IsNullOrEmpty(_unitSceneName) == false)
        {
            bool _loaded = false;
            for (int i = 0; i < SceneManager.sceneCount && !_loaded; i++)
                if (SceneManager.GetSceneAt(i).name == _unitSceneName)
                {
                    _loaded = true;
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
    private void OnPlayerDeath(int deathAnim)
    {
        Invoke("RespawnPlayer", 3f);
    }
    #endregion


    #region mono events
    private void Awake()
    {
        RegisterSingleton(this);

        RespawnPlayer();

        if (_loadUnits == false)
            return;

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

    private void OnGUI()
    {
        if (GUI.Button(new Rect(Screen.width - 150, 0, 150, 25), "Respawn Player"))
            RespawnPlayer();
        if (GUI.Button(new Rect(Screen.width - 150, 25, 150, 25), "Reload Units Scene"))
            RestartUnitsScene();
    }
    #endregion
}
