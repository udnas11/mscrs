// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class CameraController : Singleton<CameraController>
{
    #region public serialised vars
    #endregion


    #region private protected vars
    PlayerController _player;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    CameraSettingsSO _settings;
    float _horizontalInput;
    Vector2 _playerVelocitySmooth;
    #endregion


    #region events
    void OnPlayerSpawned(PlayerController player)
    {
        _player = player;
        _player.HealthEntity.OnDeath += OnPlayerDeath;
    }

    void OnPlayerDeath(int deathAnim)
    {
        
    }

    private void OnInputHorizontal(float horizontalAxis)
    {
        _horizontalInput = horizontalAxis;
    }
    #endregion


    #region mono events
    private void Awake()
    {
        RegisterSingleton(this);
        _settings = AssetDatabaseSO.Instance.CameraSettings;
    }

    private void Start()
    {
        SceneController.Instance.OnPlayerSpawned += OnPlayerSpawned;
        _player = SceneController.Instance.PlayerControllerInstance;

        PlayerInputHandler.Instance.OnHorizontalChange += OnInputHorizontal;
    }

    private void Update()
    {
        if (_player == null)
            return;

        Vector2 targetPos = _player.transform.position;
        targetPos.y += _settings.AddHeight;
        //targetPos.x += _horizontalInput * _settings.AheadDistance;

        _playerVelocitySmooth = Vector2.Lerp(_playerVelocitySmooth, _player.VelocityRigidbody * _settings.PlayerVelocityMultiplier, Time.deltaTime * _settings.PlayerVelocityLerpSpeed);

        targetPos.x += _playerVelocitySmooth.x;

        transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * _settings.LerpSpeed);
    }
    #endregion
}