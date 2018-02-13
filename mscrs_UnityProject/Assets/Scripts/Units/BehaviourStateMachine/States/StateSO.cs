// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "New State", menuName = "Behaviour State Machine/State")]
public class StateSO : ScriptableObject
{
    [SerializeField]
    ActionBaseSO[] _actions;
    [SerializeField]
    Transition[] _transitions;

    public void StateUpdate(UnitController unitController)
    {
        // do actions
        for (int i = 0; i < _actions.Length; i++)
            _actions[i].Act(unitController);

        // check transitions
        for (int i = 0; i < _transitions.Length; i++)
        {
            StateSO newState = _transitions[i].QueryTransition(unitController);
            if (newState != null)
            {
                unitController.SetBehaviourState(newState);
                return;
            }
        }
    }
}