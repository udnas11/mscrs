// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class DamageReceiver : MonoBehaviour
{
    #region public serialised vars
    [HideInInspector]
    public EDamageReceiverType MaskDamageType;

    [SerializeField]
    HealthEntity _healthEntity;

    [SerializeField]
    string _soundID;
    #endregion


    #region private protected vars
    UnitPawn _unitPawn;
    #endregion


    #region pub methods
    public bool IsDead { get { return _healthEntity.IsDead; } }

    //public void TakeDamage(int damageCount, int deathAnimationIndex, bool triggerHitAnimation)
    public void TakeDamage(int damageCount, DamagingTrigger.DamageTriggerProperties properties)
    {
        _healthEntity.TakeDamage(damageCount, properties.DeathAnimation, properties.TriggerHitAnimation);
        if (string.IsNullOrEmpty(properties.CustomHitSound) == false)
            AudioController.Play(properties.CustomHitSound, transform.position);
        else if (string.IsNullOrEmpty(_soundID) == false)
            AudioController.Play(_soundID, transform.position);
    }

    public void TakePushForce(Vector2 pushForce, float physicsDuration)
    {
        _healthEntity.TakePushForce(pushForce, physicsDuration);
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _unitPawn = _healthEntity.GetComponent<UnitPawn>();
    }
    #endregion
}