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
    #endregion


    #region private protected vars
    UnitPawn _unitPawn;
    #endregion


    #region pub methods
    public void TakeDamage(int damageCount, int deathAnimationIndex, bool triggerHitAnimation)
    {
        _healthEntity.TakeDamage(damageCount, deathAnimationIndex, triggerHitAnimation);
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