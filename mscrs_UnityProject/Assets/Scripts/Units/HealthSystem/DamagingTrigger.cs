// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public enum EDamageReceiverType
{
    Player = 1,
    Enemy = 2,
    Destructable = 4
}

public class DamagingTrigger : MonoBehaviour
{
    public enum EShakeTriggerOptions
    {
        None,
        OnEnable,
        OnHitOnly
    }

    [Serializable]
    public struct DamageTriggerProperties
    {
        public int DeathAnimation;
        public bool TriggerHitAnimation;
        public string CustomHitSound;
    }

    #region public serialised vars
    [HideInInspector]
    public EDamageReceiverType MaskDamageType;
    
    [SerializeField]
    int _damage;
    [SerializeField]
    bool _canCrit;
    [SerializeField]
    DamageTriggerProperties _properties;

    [SerializeField, Header("Camera Shake")]
    CameraShaker _cameraShaker;
    [SerializeField]
    EShakeTriggerOptions _shakerTriggerType;

    [SerializeField, Header("Freeze")]
    bool _shouldFreeze;
    [SerializeField]
    float _freezeDuration;

    [SerializeField, Header("Sparks")]
    Vector2 _sparksOffset;
    [SerializeField]
    Transform[] _onHitEffects;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    public DamageTriggerProperties Properties { get { return _properties; } }
    #endregion


    #region private protected methods
    virtual protected void OnProcessDamage(DamageReceiver receiver)
    {
        if (receiver.IsDead)
            return;

        if (_shouldFreeze)
            SlowMoController.Instance.ApplyFreeze(_freezeDuration);

        if (_shakerTriggerType == EShakeTriggerOptions.OnHitOnly && _cameraShaker != null)
            _cameraShaker.Apply();

        int dmg = _damage;
        bool isCrit = false;
        if (_canCrit)
        {
            GenericSettingsSO genericSettings = AssetDatabaseSO.Instance.GenericSettings;
            if (UnityRandom.Range(0f, 1f) <= genericSettings.CritChance)
            {
                isCrit = true;
                dmg = Mathf.FloorToInt(dmg * genericSettings.CritMultiplier);
                SlowMoController.Instance.ApplyCritSlowmo();
                Vector3 sparkPos = transform.position;
                sparkPos.x += _sparksOffset.x * transform.lossyScale.x;
                sparkPos.y += _sparksOffset.y;
                Instantiate(AssetDatabaseSO.Instance.SparksPrefab, sparkPos, Quaternion.identity);
            }
        }

        if (_onHitEffects.Length > 0)
        {
            Vector3 spawnPos = transform.position;
            spawnPos.x += _sparksOffset.x * transform.lossyScale.x;
            spawnPos.y += _sparksOffset.y;
            var newInst = Instantiate(_onHitEffects[UnityRandom.Range(0, _onHitEffects.Length)], spawnPos, Quaternion.identity) as Transform;
            newInst.localScale = this.transform.lossyScale;
        }

        receiver.TakeDamage(dmg, isCrit, this);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    protected virtual void OnEnable()
    {
        if (_shakerTriggerType == EShakeTriggerOptions.OnEnable && _cameraShaker != null)
            _cameraShaker.Apply();
    }

    private void OnTriggerEnter2D(Collider2D receiverTrigger)
    {
        var receiverTarget = receiverTrigger.GetComponent<DamageReceiver>();
        if (receiverTarget == null)
            return; // collided with another damage dealer        

        int maskThis = (int)MaskDamageType;
        int maskTarget = (int)receiverTarget.MaskDamageType;
        if ((maskThis & maskTarget) != 0)
        {
            OnProcessDamage(receiverTarget);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position + (Vector3)_sparksOffset, 0.01f);
    }
    #endregion
}