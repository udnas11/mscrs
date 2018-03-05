// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;
using UnityEngine.UI;

namespace MoonscarsUI
{
    public class PlayerStatus : Singleton<PlayerStatus>
    {
        #region public serialised vars
        [SerializeField]
        Slider _healthSlider;
        [SerializeField]
        Slider _healthSliderDelayed;
        [SerializeField]
        float _healthDelaySpeed;

        [SerializeField]
        Slider _staminaSlider;
        [SerializeField]
        Slider _staminaSliderDelayed;
        [SerializeField]
        float _staminaDelaySpeed;
        #endregion


        #region private protected vars
        PlayerController _playerController;
        HealthEntity _playerHealthEntity;
        #endregion


        #region pub methods
        #endregion


        #region private protected methods
        void Init(PlayerController playerController)
        {
            _playerController = playerController;
            _playerHealthEntity = _playerController.HealthEntity;
            _playerHealthEntity.OnHealthChanged += OnPlayerHealthUpdated;
            _playerController.OnStaminaChanged += OnPlayerStaminaUpdated;
        }

        private void OnPlayerHealthUpdated(int current, int max)
        {
            float newValue = (float)current / max;
            _healthSlider.value = newValue;
        }

        private void OnPlayerStaminaUpdated(float current, float max)
        {
            float newValue = (float)current / max;
            _staminaSlider.value = newValue;
        }
        #endregion


        #region events
        #endregion


        #region mono events
        private void Awake()
        {
            RegisterSingleton(this);
            Init(SceneController.Instance.PlayerControllerInstance);
        }

        private void Update()
        {
            _healthSliderDelayed.value = Mathf.MoveTowards(_healthSliderDelayed.value, _healthSlider.value, _healthDelaySpeed * Time.unscaledDeltaTime);
            _staminaSliderDelayed.value = Mathf.MoveTowards(_staminaSliderDelayed.value, _staminaSlider.value, _staminaDelaySpeed * Time.unscaledDeltaTime);
        }

        private void OnDestroy()
        {
            ResetInstance();
        }
        #endregion
    }
}