// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class StateMachinePhaseClearAll : StateMachineBehaviour
{
    [SerializeField]
    bool _onEnterState;
    [SerializeField]
    bool _onExitState;

    void ClearStates(Animator animator)
    {
        var unitAnimController = animator.GetComponent<BaseAnimatorController>();
        if (unitAnimController != null)
        {
            unitAnimController.ClearStatePhases();
        }
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (_onEnterState)
            ClearStates(animator);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);

        if (_onExitState)
            ClearStates(animator);
    }
}