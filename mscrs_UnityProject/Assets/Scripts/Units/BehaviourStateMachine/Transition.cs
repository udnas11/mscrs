// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[Serializable]
public class Transition
{
    public QueryBaseSO Query;
    public StateSO[] ResultStates;

    public StateSO QueryTransition(UnitController unitController)
    {
        int result = Query.DoQuery(unitController);
        return ResultStates[result];
    }
}