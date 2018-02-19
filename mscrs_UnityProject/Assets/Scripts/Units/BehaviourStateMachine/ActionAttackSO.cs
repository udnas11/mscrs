// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "ActionAttack", menuName = "Behaviour State Machine/Action - Attack")]
public class ActionAttackSO : ActionBaseSO
{
    public override void Act(UnitController unitController)
    {
        unitController.Attack(SceneController.Instance.PlayerControllerInstance.transform.position);
    }
}