// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

abstract public class UnitController : MonoBehaviour
{
    [SerializeField]
    protected StateSO _startState;
    
    protected PlayerController _player;

    protected virtual void Start()
    {
        _player = SceneController.Instance.PlayerController;
    }
}