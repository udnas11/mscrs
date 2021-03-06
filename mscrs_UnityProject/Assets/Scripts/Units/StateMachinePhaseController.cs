﻿// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class StateMachinePhaseController : StateMachineBehaviour
{

    [SerializeField]
    BaseAnimatorController.EAnimationPhase _onStateEnterActivate;
    [SerializeField]
    BaseAnimatorController.EAnimationPhase _onStateExitDeactivate;

    [Space]
    [SerializeField]
    BaseAnimatorController.EAnimationPhase _onMachineEnterActivate;
    [SerializeField]
    BaseAnimatorController.EAnimationPhase _onMachineExitDeactivate;

    private void ChangeState(Animator animator, BaseAnimatorController.EAnimationPhase phase, bool newValue)
    {
        var unitAnimController = animator.GetComponent<BaseAnimatorController>();
        if (unitAnimController != null)
        {
            unitAnimController.OnStatePhaseChange(phase, newValue);
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (_onStateEnterActivate != BaseAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onStateEnterActivate, true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (_onStateExitDeactivate != BaseAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onStateExitDeactivate, false);
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);

        if (_onMachineEnterActivate != BaseAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onMachineEnterActivate, true);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);

        if (_onMachineExitDeactivate != BaseAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onMachineExitDeactivate, false);
    }
}
