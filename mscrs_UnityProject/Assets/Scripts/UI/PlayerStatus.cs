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
        Slider _staminaSlider;
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
            _healthSlider.value = (float)current / max;
        }

        private void OnPlayerStaminaUpdated(float current, float max)
        {
            _staminaSlider.value = current / max;
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

        private void OnDestroy()
        {
            ResetInstance();
        }
        #endregion
    }
}