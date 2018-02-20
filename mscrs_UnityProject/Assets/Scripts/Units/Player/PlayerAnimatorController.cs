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
    public event Action OnAnimEventAirdropInhibitPhysicsAction;
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

    public void Roll()
    {
        TriggerOnce("doRoll");
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

    public void OnAnimEventAirdropInhibitPhsyics()
    {
        if (OnAnimEventAirdropInhibitPhysicsAction != null)
            OnAnimEventAirdropInhibitPhysicsAction();
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
    private void OnGUI()
    {
        string statesOn = "Active phases: ";
        foreach (var pair in _phaseStates)
            if (pair.Value)
                statesOn += pair.Key.ToString() + " ";
        GUI.Label(new Rect(0, Screen.height-20, 300, 20), statesOn);
    }
    #endregion
}