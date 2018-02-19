// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "QueryWithinAttackReach", menuName = "Behaviour State Machine/Query - Within Attack Range")]
public class QueryWithinAttackReachSO : QueryBaseSO
{
    public override int DoQuery(UnitController unitController)
    {
        float playerPosX = SceneController.Instance.PlayerControllerInstance.transform.position.x;
        float distanceX = Mathf.Abs(playerPosX - unitController.transform.position.x);
        return (distanceX <= unitController.BehaviourSettings.AttackRange) ? 1 : 0;
    }
}