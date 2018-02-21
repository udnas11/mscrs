// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class DamagingTriggerPushback : DamagingTrigger
{
    #region public serialised vars
    [SerializeField, Header("Pushback")]
    Vector2 _pushForce;
    [SerializeField]
    float _physicsDuration;
    #endregion


    #region private protected vars
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    protected override void OnProcessDamage(DamageReceiver receiver)
    {
        // applying push before damage so units that will die get pushed as well

        Vector2 pushForceFlippable = _pushForce;
        pushForceFlippable.x *= transform.lossyScale.x < 0 ? -1f : 1f;
        receiver.TakePushForce(pushForceFlippable, _physicsDuration);

        base.OnProcessDamage(receiver);
    }
    #endregion


    #region events
    #endregion


    #region mono events

    #endregion
}