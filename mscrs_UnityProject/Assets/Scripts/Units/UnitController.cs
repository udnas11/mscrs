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
    protected Transform _behaviourRaycastTarget;

    [SerializeField, Header("State machine")]
    protected StateSO _startState;
    [SerializeField]
    protected StateSO _deadState;
    #endregion


    #region private protected variables
    protected StateSO _currentState;
    protected PlayerController _player;
    protected UnitPawn _unitPawn;
    protected HealthEntity _healthEntity;

    protected Vector2 _moveTarget;
    protected bool _isMoveTargetSet;
    protected bool _dead;

    public Transform BehaviourRaycastTarget { get { return _behaviourRaycastTarget; } }
    public bool IsDead { get { return _dead; } }
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
        if (_currentState != null)
            _currentState.OnExit(this);
        _currentState = newState;
    }
    #endregion


    #region events
    private void OnDeath()
    {
        _dead = true;
        SetBehaviourState(_deadState);
        _unitPawn.Die();
    }
    #endregion


    #region mono events
    protected virtual void Awake()
    {
        _unitPawn = GetComponent<UnitPawn>();
        _healthEntity = GetComponent<HealthEntity>();

        SetBehaviourState(_startState);
    }

    protected virtual void Start()
    {
        _player = SceneController.Instance.PlayerController;
        _healthEntity.OnDeath += OnDeath;
    }

    protected virtual void Update()
    {
        _currentState.StateUpdate(this);
    }
    #endregion
}