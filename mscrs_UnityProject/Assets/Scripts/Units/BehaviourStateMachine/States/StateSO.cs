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
}