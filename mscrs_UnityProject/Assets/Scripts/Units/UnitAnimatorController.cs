// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class UnitAnimatorController : MonoBehaviour
{

    public enum EAnimationPhase
    {
        None,
        Roll,
        Jump
    }

    #region public serialised vars
    public event Action OnAnimCallbackApplyForce;
    public event Action<bool> OnJumpPhaseActive;
    #endregion


    #region private protected vars
    private bool _isJumpPhase;
    public bool IsJumpPhase { get { return _isJumpPhase; } }

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
        _animator.SetTrigger("doAttack");
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
    public void OnAnimCallbackJumpApplyforce() // unity even from jump start animation when to apply force
    {
        if (OnAnimCallbackApplyForce != null)
            OnAnimCallbackApplyForce();
    }

    public void OnStatePhaseChange(EAnimationPhase state, bool newStatus)
    {
        var oldState = GetPhaseState(state);
        if (oldState != newStatus)
        {
            _phaseStates[state] = newStatus;
        }
    }

    public void SetJumpPhaseActive(bool newVal) // state machine callback from anmator when jump anim started
    {
        if (newVal != _isJumpPhase)
            if (OnJumpPhaseActive != null)
                OnJumpPhaseActive(newVal);
        _isJumpPhase = newVal;
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
    #endregion
}