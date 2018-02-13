// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{

    public enum EAnimationPhase
    {
        None,
        Roll,
        Jump,
        Attacking,
        ComboZone
    }

    #region public serialised vars
    public event Action OnAnimEventJumpApplyForceAction;
    public event Action OnAnimEventAirdropInhibitPhysicsAction;
    public event Action OnAnimEventAirdropAction;
    public event Action<bool> OnJumpPhaseActive;
    #endregion


    #region private protected vars
    Animator _animator;
    SpriteRenderer _spriteRenderer;
    List<string> _triggerQueue = new List<string>();

    Dictionary<EAnimationPhase, bool> _phaseStates = new Dictionary<EAnimationPhase, bool>();
    #endregion


    #region pub set methods
    public void SetRunning(bool newState)
    {
        _animator.SetBool("isRunning", newState);
    }

    public void SetFlipX(bool newFlip)
    {
        _spriteRenderer.flipX = newFlip;
    }

    public void Jump()
    {
        TriggerOnce("doJump");
    }

    public void SetInAir(bool newInAir)
    {
        _animator.SetBool("isInAir", newInAir);
    }

    public void Attack()
    {
        TriggerOnce("doAttack");
        if (GetPhaseState(EAnimationPhase.ComboZone))
            _animator.SetTrigger("doCombo");
    }

    public void Roll()
    {
        TriggerOnce("doRoll");
    }

    public bool GetPhaseState(EAnimationPhase state)
    {
        bool value;
        bool valueExists = _phaseStates.TryGetValue(state, out value);
        return valueExists && value;
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

    public void OnStatePhaseChange(EAnimationPhase state, bool newStatus) // callbacks from animator state machine script
    {
        var oldState = GetPhaseState(state);
        if (oldState != newStatus)
        {
            _phaseStates[state] = newStatus;
        }
    }
    #endregion


    #region private protected methods
    private void TriggerOnce(string trigger)
    {
        _animator.SetTrigger(trigger);
        _triggerQueue.Add(trigger);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        for (int i = 0; i < _triggerQueue.Count; i++)
            _animator.ResetTrigger(_triggerQueue[i]);
        _triggerQueue.Clear();
    }

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