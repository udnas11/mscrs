// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class CameraEffectsController : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Shader _cameraShader;
    [SerializeField]
    AnimationCurve _heartbeatCurve;
    #endregion


    #region private protected vars
    Material _camMaterial;
    PlayerController _player;

    float _playerHealth = 1f;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    void SetSaturation(float saturation)
    {
        _camMaterial.SetFloat("_Saturation", saturation);
    }
    #endregion


    #region events
    private void OnPlayerHealthChange(int current, int max)
    {
        _playerHealth = (float)current / max;
    }

    private void OnPlayerSpawned(PlayerController inst)
    {
        _player.HealthEntity.OnHealthChanged -= OnPlayerHealthChange;
        _playerHealth = 1f;

        _player = inst;
        _player.HealthEntity.OnHealthChanged += OnPlayerHealthChange;
    }
    #endregion


    #region mono events
    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, _camMaterial);
    }

    private void Awake()
    {
        _camMaterial = new Material(_cameraShader);
        SetSaturation(1f);
    }

    private void Start()
    {
        _player = SceneController.Instance.PlayerControllerInstance;
        _player.HealthEntity.OnHealthChanged += OnPlayerHealthChange;

        SceneController.Instance.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void Update()
    {
        float t = _heartbeatCurve.Evaluate(Time.time);
        SetSaturation(Mathf.Lerp(_playerHealth, 1f, t));
    }
    #endregion
}