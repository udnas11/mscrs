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
    Transform[] _onHitEffects;
    [SerializeField]
    string _soundID;
    #endregion


    #region private protected vars
    UnitPawn _unitPawn;
    #endregion


    #region pub methods
    public bool IsDead { get { return _healthEntity.IsDead; } }

    //public void TakeDamage(int damageCount, int deathAnimationIndex, bool triggerHitAnimation)
    //public void TakeDamage(int damageCount, DamagingTrigger.DamageTriggerProperties properties)
    public void TakeDamage(int damageCount, DamagingTrigger damageTrigger)
    {
        DamagingTrigger.DamageTriggerProperties properties = damageTrigger.Properties;
        _healthEntity.TakeDamage(damageCount, properties.DeathAnimation, properties.TriggerHitAnimation);

        if (_onHitEffects.Length > 0)
        {
            var newInst = Instantiate(_onHitEffects[UnityRandom.Range(0, _onHitEffects.Length)], transform.position, Quaternion.identity);
            newInst.localScale = new Vector3(Mathf.Sign(this.transform.position.x - damageTrigger.transform.position.x), 1f, 1f);
        }

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