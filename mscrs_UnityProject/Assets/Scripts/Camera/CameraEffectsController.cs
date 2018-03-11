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
    AnimationCurve _hurtAnim;
    [SerializeField]
    float _hurtAnimDuration = 1f;
    #endregion


    #region private protected vars
    Material _camMaterial;
    PlayerController _player;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    void SetRedMultiplier(float saturation)
    {
        _camMaterial.SetFloat("_RedMultiplier", saturation);
    }

    IEnumerator IAnimateHurt()
    {
        float timeStart = Time.time;
        float timeEnd = timeStart + _hurtAnimDuration;
        while (Time.time < timeEnd)
        {
            float t = Mathf.InverseLerp(timeStart, timeEnd, Time.time);
            SetRedMultiplier(_hurtAnim.Evaluate(t));
            yield return null;
        }
        SetRedMultiplier(1f);
    }
    #endregion


    #region events
    private void OnPlayerHealthChange(int current, int max)
    {
        StartCoroutine(IAnimateHurt());
    }

    private void OnPlayerSpawned(PlayerController inst)
    {
        _player.HealthEntity.OnHealthChanged -= OnPlayerHealthChange;

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
        SetRedMultiplier(1f);
    }

    private void Start()
    {
        _player = SceneController.Instance.PlayerControllerInstance;
        _player.HealthEntity.OnHealthChanged += OnPlayerHealthChange;

        SceneController.Instance.OnPlayerSpawned += OnPlayerSpawned;
    }
    #endregion
}