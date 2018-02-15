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
    #region public serialised vars
    [HideInInspector]
    public EDamageReceiverType MaskDamageType;
    
    [SerializeField]
    int _damage;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void OnTriggerEnter2D(Collider2D receiverTrigger)
    {
        var receiverTarget = receiverTrigger.GetComponent<DamageReceiver>();
        if (receiverTarget == null)
            return; // collided with another damage dealer

        int maskThis = (int)MaskDamageType;
        int maskTarget = (int)receiverTarget.MaskDamageType;
        if ((maskThis & maskTarget) != 0)
        {
            receiverTarget.TakeDamage(_damage);
        }
    }
    #endregion
}