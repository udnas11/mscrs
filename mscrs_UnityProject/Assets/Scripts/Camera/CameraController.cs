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
    CameraSettingsSO _settings;
    float _horizontalInput;
    Vector2 _playerVelocitySmooth;
    Vector2 _shake = Vector2.zero;
    CameraZone _activeCameraZone;
    #endregion


    #region pub methods
    public void CameraZoneEnter(CameraZone zone)
    {
        _activeCameraZone = zone;
    }

    public void CameraZoneExit(CameraZone zone)
    {
        if (zone == _activeCameraZone)
            _activeCameraZone = null;
    }

    public void ApplyCameraShake(AnimationCurve x, AnimationCurve y, float duration)
    {
        StartCoroutine(ICameraShake(x, y, duration));
    }
    #endregion


    #region private protected methods
    IEnumerator ICameraShake(AnimationCurve x, AnimationCurve y, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;
        float t;
        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            _shake.x += x.Evaluate(t);
            _shake.y += y.Evaluate(t);
            yield return null;
        }
    }
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

        _playerVelocitySmooth = Vector2.Lerp(_playerVelocitySmooth, _player.VelocityRigidbody * _settings.PlayerVelocityMultiplier, Time.deltaTime * _settings.PlayerVelocityLerpSpeed);

        targetPos.x += _playerVelocitySmooth.x;

        if (_activeCameraZone != null)
            targetPos = _activeCameraZone.TargetPos;

        targetPos += _shake;
        _shake = Vector2.zero;
        transform.position = Vector2.Lerp(transform.position, targetPos/* + _shake*/, Time.deltaTime * _settings.LerpSpeed);
    }
    #endregion
}