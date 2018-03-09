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
    public float AttackMinimumRange;
}

public class UnitController : MonoBehaviour
{
    #region public serialisable
    public UnitBehaviourSettings BehaviourSettings;

    [SerializeField]
    protected Transform _behaviourRaycastTarget;
    [SerializeField]
    GibsController _gibsPrefab;

    [SerializeField, Header("State machine")]
    protected StateSO _startState;
    [SerializeField]
    protected StateSO _deadState;
    #endregion


    #region private protected variables
    protected StateSO _currentState;
    protected UnitPawn _unitPawn;
    protected HealthEntity _healthEntity;

    protected Vector2 _moveTarget;
    protected bool _isMoveTargetSet;
    protected bool _dead;

    public Transform BehaviourRaycastTarget { get { return _behaviourRaycastTarget; } }
    public bool IsDead { get { return _dead; } }
    public bool IsFlipX { get { return _unitPawn.IsFlipX; } }
    #endregion


    #region public methods
    public virtual void Attack(Vector2 targetPos)
    {
        float dist = Vector2.Distance(targetPos, transform.position);
        _unitPawn.Attack(targetPos, dist > 0.6f ? 0 : 1);
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

    public virtual void SpawnGibs()
    {
        var gibs = Instantiate(_gibsPrefab, _behaviourRaycastTarget.position, Quaternion.identity) as GibsController;
        gibs.SetFlipX(!_unitPawn.IsFlipX);
    }
    #endregion


    #region events
    private void OnDeath(int deathAnimationIndex)
    {
        _dead = true;
        SetBehaviourState(_deadState);
        _unitPawn.Die(deathAnimationIndex);
    }

    private void OnPushForceReceived(Vector2 vector, float physicsDuration)
    {
        if (!_dead)
            _unitPawn.ApplyPushForce(vector, physicsDuration);
    }

    private void OnPlayGetHitAnimation()
    {
        _unitPawn.PlayGotHitAnimation();
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
        _healthEntity.OnDeath += OnDeath;
        _healthEntity.OnPushForceReceived += OnPushForceReceived;
        _healthEntity.OnPlayGetHitAnimation += OnPlayGetHitAnimation;
    }

    protected virtual void Update()
    {
        _currentState.StateUpdate(this);
    }
    #endregion
}