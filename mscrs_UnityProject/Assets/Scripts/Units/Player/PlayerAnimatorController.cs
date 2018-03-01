// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : BaseAnimatorController
{

    #region public serialised vars
    public event Action OnAnimEventJumpApplyForceAction;
    public event Action<float> OnAnimEventInhibitPhysicsAction;
    public event Action OnAnimEventAirdropAction;
    public event Action<bool> OnJumpPhaseActive;
    #endregion


    #region private protected vars
    #endregion


    #region pub set methods
    public void Jump()
    {
        TriggerOnce("doJump");
    }    

    public override void Attack()
    {
        base.Attack();
        if (GetPhaseState(EAnimationPhase.ComboZone))
            _animator.SetTrigger("doCombo");
    }

    public override void Attack2()
    {
        base.Attack2();
        if (GetPhaseState(EAnimationPhase.ComboZone))
            _animator.SetTrigger("doCombo2");
    }

    public virtual void Attack2Charged()
    {
        TriggerOnce("doChargedHeavy");
    }

    public virtual void Attack1Charged()
    {
        TriggerOnce("doChargedLight");
    }

    public void Roll()
    {
        TriggerOnce("doRoll");
    }

    public void SetStamina(float newValue)
    {
        _animator.SetFloat("PlayerStamina", newValue);
    }

    public void SetShouldStopRun(bool newValue)
    {
        _animator.SetBool("shouldStopRun", newValue);
    }
    #endregion


    #region pub callback methods
    public void OnAnimEventJumpApplyforce() // unity even from jump start animation when to apply force
    {
        if (OnAnimEventJumpApplyForceAction != null)
            OnAnimEventJumpApplyForceAction();
    }

    public void OnAnimEventComboAvailable() // unity event from attack animations when input for combo chain is available
    {
        OnStatePhaseChange(EAnimationPhase.ComboZone, true);
    }

    public void OnAnimEventComboUnavailable()
    {
        OnStatePhaseChange(EAnimationPhase.ComboZone, false);
    }

    public void OnAnimEventInhibitPhsyics(float duration)
    {
        if (OnAnimEventInhibitPhysicsAction != null)
            OnAnimEventInhibitPhysicsAction(duration);
    }

    public void OnAnimEventAirDrop()
    {
        if (OnAnimEventAirdropAction != null)
            OnAnimEventAirdropAction();
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    #endregion
}