// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[System.Serializable]
public class UnitBehaviourSettings
{
    public float DetectRange;
}

abstract public class UnitController : MonoBehaviour
{
    public UnitBehaviourSettings BehaviourSettings;

    [SerializeField]
    protected StateSO _startState;

    protected StateSO _currentState;
    protected PlayerController _player;

    public virtual void SetBehaviourState(StateSO newState)
    {
        Debug.Log("New state: " + newState.name);
        _currentState = newState;
    }

    protected virtual void Awake()
    {
        SetBehaviourState(_startState);
    }

    protected virtual void Start()
    {
        _player = SceneController.Instance.PlayerController;
    }

    protected virtual void Update()
    {
        _currentState.StateUpdate(this);
    }
}