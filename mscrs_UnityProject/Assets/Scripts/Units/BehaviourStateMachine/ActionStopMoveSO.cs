// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[CreateAssetMenu(fileName = "ActionStopMove", menuName = "Behaviour State Machine/Action - Stop Movement")]
public class ActionStopMoveSO : ActionBaseSO
{
    public override void Act(UnitController unitController)
    {
        unitController.SetMoveTarget(Vector2.zero);
    }
}