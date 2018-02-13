// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
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
    PlayerAnimatorController.EAnimationPhase _onStateEnterActivate;
    [SerializeField]
    PlayerAnimatorController.EAnimationPhase _onStateExitDeactivate;

    [Space]
    [SerializeField]
    PlayerAnimatorController.EAnimationPhase _onMachineEnterActivate;
    [SerializeField]
    PlayerAnimatorController.EAnimationPhase _onMachineExitDeactivate;

    private void ChangeState(Animator animator, PlayerAnimatorController.EAnimationPhase phase, bool newValue)
    {
        var unitAnimController = animator.GetComponent<PlayerAnimatorController>();
        if (unitAnimController != null)
        {
            unitAnimController.OnStatePhaseChange(phase, newValue);
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (_onStateEnterActivate != PlayerAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onStateEnterActivate, true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (_onStateExitDeactivate != PlayerAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onStateExitDeactivate, false);
    }

    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);

        if (_onMachineEnterActivate != PlayerAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onMachineEnterActivate, true);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);

        if (_onMachineExitDeactivate != PlayerAnimatorController.EAnimationPhase.None)
            ChangeState(animator, _onMachineExitDeactivate, false);
    }
}
