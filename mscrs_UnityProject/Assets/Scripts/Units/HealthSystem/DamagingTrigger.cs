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
    Enemy = 2
}

public class DamagingTrigger : MonoBehaviour
{
    public enum EShakeTriggerOptions
    {
        None,
        OnEnable,
        OnHitOnly
    }

    #region public serialised vars
    [HideInInspector]
    public EDamageReceiverType MaskDamageType;
    
    [SerializeField]
    int _damage;
    [SerializeField]
    int _deathAnim;
    [SerializeField]
    bool _triggerHitAnimation;

    [SerializeField, Header("Camera Shake")]
    CameraShaker _cameraShaker;
    [SerializeField]
    EShakeTriggerOptions _shakerTriggerType;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    virtual protected void OnProcessDamage(DamageReceiver receiver)
    {
        if (_shakerTriggerType == EShakeTriggerOptions.OnHitOnly && _cameraShaker != null)
            _cameraShaker.Apply();

        receiver.TakeDamage(_damage, _deathAnim, _triggerHitAnimation);
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
    #endregion
}