// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

abstract public class QueryBaseSO : ScriptableObject
{
    abstract public int DoQuery(UnitController unitController);
}