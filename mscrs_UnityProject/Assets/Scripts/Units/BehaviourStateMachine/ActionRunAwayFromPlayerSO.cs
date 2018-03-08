// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "ActionRunAwayFromPlayer", menuName = "Behaviour State Machine/Action - Run away from player")]
public class ActionRunAwayFromPlayerSO : ActionBaseSO
{
    public override void Act(UnitController unitController)
    {
        Vector2 playerPos = SceneController.Instance.PlayerControllerInstance.transform.position;
        Vector2 unitPos = unitController.transform.position;
        float deltaPosX = unitPos.x - playerPos.x;
        Vector2 targetPos = unitPos + new Vector2(deltaPosX, 0f);
        unitController.SetMoveTarget(targetPos);
    }
}