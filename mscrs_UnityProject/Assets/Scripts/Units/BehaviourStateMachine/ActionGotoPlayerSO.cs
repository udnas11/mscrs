// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "ActionGotoPlayer", menuName = "Behaviour State Machine/Action - Go to player")]
public class ActionGotoPlayerSO : ActionBaseSO
{
    public override void Act(UnitController unitController)
    {
        Vector2 targetPos = SceneController.Instance.PlayerControllerInstance.transform.position;
        unitController.SetMoveTarget(targetPos);
    }
}