// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
public class BaseAnimatorController : MonoBehaviour
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
    #endregion


    #region private protected vars
    protected Animator _animator;
    protected List<string> _triggerQueue = new List<string>();
    protected Dictionary<EAnimationPhase, bool> _phaseStates = new Dictionary<EAnimationPhase, bool>();
    #endregion


    #region pub methods
    public virtual void SetInAir(bool newInAir)
    {
        _animator.SetBool("isInAir", newInAir);
    }

    public virtual void SetRunning(bool newState)
    {
        _animator.SetBool("isRunning", newState);
    }

    public virtual void SetDead(bool newState)
    {
        _animator.SetBool("isDead", newState);
    }

    public virtual void Attack()
    {
        TriggerOnce("doAttack");
    }

    public bool GetPhaseState(EAnimationPhase state)
    {
        bool value;
        bool valueExists = _phaseStates.TryGetValue(state, out value);
        return valueExists && value;
    }
    #endregion


    #region private protected methods
    protected void TriggerOnce(string trigger)
    {
        _animator.SetTrigger(trigger);
        _triggerQueue.Add(trigger);
    }
    #endregion


    #region events
    public void OnStatePhaseChange(EAnimationPhase state, bool newStatus) // callbacks from animator state machine script
    {
        var oldState = GetPhaseState(state);
        if (oldState != newStatus)
        {
            _phaseStates[state] = newStatus;
        }
    }
    #endregion


    #region mono events
    virtual protected void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    virtual protected void Update()
    {        
    }

    private void LateUpdate()
    {
        for (int i = 0; i < _triggerQueue.Count; i++)
        {
            _animator.ResetTrigger(_triggerQueue[i]);
        }
        _triggerQueue.Clear();
    }
    #endregion
}