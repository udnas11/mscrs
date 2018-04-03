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
    [SerializeField, Header("Player Aura")]
    Shader _cameraShaderPlayerAura;
    [SerializeField]
    float _playerAuraRange;

    [SerializeField, Header("Hurt")]
    Shader _cameraShaderHurt;
    [SerializeField]
    AnimationCurve _hurtAnim;
    [SerializeField]
    float _hurtAnimDuration = 1f;
    #endregion


    #region private protected vars
    Material _camMaterialHurt;
    Material _camMaterialPlayerAura;
    //RenderTexture _texturePass;
    PlayerController _player;
    Camera _cam;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    void SetRedMultiplier(float saturation)
    {
        _camMaterialHurt.SetFloat("_RedMultiplier", saturation);
    }

    void SetPlayerPos(Vector2 viewPos)
    {
        _camMaterialPlayerAura.SetFloat("_Range", _playerAuraRange);
        _camMaterialPlayerAura.SetVector("_Origin", viewPos);
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
        /*
        if (_texturePass == null)
            _texturePass = new RenderTexture(source);
            */
        Graphics.Blit(source, source, _camMaterialPlayerAura);
        Graphics.Blit(source, destination, _camMaterialHurt);
    }

    private void Awake()
    {
        _cam = GetComponent<Camera>();
        _camMaterialPlayerAura = new Material(_cameraShaderPlayerAura);
        _camMaterialHurt = new Material(_cameraShaderHurt);
        SetRedMultiplier(1f);
    }

    private void Start()
    {
        _player = SceneController.Instance.PlayerControllerInstance;
        _player.HealthEntity.OnHealthChanged += OnPlayerHealthChange;

        SceneController.Instance.OnPlayerSpawned += OnPlayerSpawned;
    }

    private void Update()
    {
        Vector3 worldSpace = _player.transform.position;
        Vector2 viewSpace = _cam.WorldToViewportPoint(worldSpace);
        SetPlayerPos(viewSpace);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
            SetRedMultiplier(1f);
    }
    #endregion
}