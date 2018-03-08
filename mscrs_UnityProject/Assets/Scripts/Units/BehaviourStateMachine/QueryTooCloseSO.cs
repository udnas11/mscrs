// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "QueryTooClose", menuName = "Behaviour State Machine/Query - Too Close")]
public class QueryTooCloseSO : QueryBaseSO
{
    public override int DoQuery(UnitController unitController)
    {
        float playerPosX = SceneController.Instance.PlayerControllerInstance.transform.position.x;
        float distanceX = Mathf.Abs(playerPosX - unitController.transform.position.x);
        return (distanceX <= unitController.BehaviourSettings.AttackMinimumRange) ? 1 : 0;
    }
}