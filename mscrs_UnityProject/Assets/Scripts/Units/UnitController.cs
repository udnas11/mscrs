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
    public float AttackRange;
}

abstract public class UnitController : MonoBehaviour
{
    #region public serialisable
    public UnitBehaviourSettings BehaviourSettings;

    [SerializeField]
    protected StateSO _startState;
    [SerializeField]
    protected Transform _behaviourRaycastTarget;
    #endregion


    #region private protected variables
    protected StateSO _currentState;
    protected PlayerController _player;
    protected UnitPawn _unitPawn;

    protected Vector2 _moveTarget;
    protected bool _isMoveTargetSet;

    public Transform BehaviourRaycastTarget { get { return _behaviourRaycastTarget; } }
    #endregion


    #region public methods
    public virtual void Attack(Vector2 targetPos)
    {
        _unitPawn.Attack(targetPos);
    }

    public virtual void SetMoveTarget(Vector3 target)
    {
        _moveTarget = target;
        _isMoveTargetSet = target != Vector3.zero;
        _unitPawn.SetTargetMove(_moveTarget);
    }

    public virtual void SetBehaviourState(StateSO newState)
    {
        Debug.Log("New state: " + newState.name);
        if (_currentState != null)
            _currentState.OnExit(this);
        _currentState = newState;
    }
    #endregion


    #region mono events
    protected virtual void Awake()
    {
        _unitPawn = GetComponent<UnitPawn>();

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
    #endregion
}