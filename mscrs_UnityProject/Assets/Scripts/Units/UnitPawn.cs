// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class UnitPawn : MonoBehaviour
{
    #region public serialised vars
    [SerializeField, HideInInspector]
    float _animHorizontalSpeed;
    [SerializeField]
    GameObject _damageDealers;
    #endregion


    #region private protected vars
    private UnitAnimatorController _unitAnimatorController;
    private Rigidbody2D _rigidBody2d;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _targetMovePos;
    private bool _flipLeft;
    private int _groundTriggersActive;
    private float _physicsPenaltyOverTimestamp;
    #endregion


    #region pub methods
    public bool InAir { get { return _groundTriggersActive == 0; } }
    public bool IsFlipX { get { return _flipLeft; } }

    public void SetTargetMove(Vector2 pos)
    {
        _targetMovePos = pos;
    }

    public void SetFlipLeft(bool newState)
    {
        if (_flipLeft != newState)
        {
            _flipLeft = newState;
            _spriteRenderer.flipX = newState;
            _damageDealers.transform.localScale = new Vector3(newState?-1f:1f, 1f, 1f);
        }
    }

    public void Attack(Vector2 targetPos)
    {
        if (_unitAnimatorController.GetPhaseState(BaseAnimatorController.EAnimationPhase.Attacking) == false)
            SetFlipLeft(targetPos.x < transform.position.x);
        _unitAnimatorController.Attack();
    }

    public void Die(int deathAnimationIndex = 0)
    {
        _unitAnimatorController.SetDead(true, deathAnimationIndex);
    }

    public void ApplyPushForce(Vector2 force, float physicsDuration)
    {
        _rigidBody2d.AddForce(force, ForceMode2D.Impulse);
        _physicsPenaltyOverTimestamp = Time.time + physicsDuration;
    }

    public void PlayGotHitAnimation()
    {
        _unitAnimatorController.GetHit();
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _unitAnimatorController = GetComponent<UnitAnimatorController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        _groundTriggersActive++;
        _unitAnimatorController.SetInAir(InAir);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        _groundTriggersActive--;
        _unitAnimatorController.SetInAir(InAir);
    }

    private void FixedUpdate()
    {
        if (_targetMovePos != Vector2.zero)
        {
            SetFlipLeft(transform.position.x > _targetMovePos.x);
        }

        _unitAnimatorController.SetRunning(_targetMovePos != Vector2.zero);

        float resultHorizontalVelocity = _animHorizontalSpeed * (_flipLeft ? -1f : 1f);
        
        if (InAir == false && Time.time > _physicsPenaltyOverTimestamp)
        {
            _rigidBody2d.velocity = new Vector2(resultHorizontalVelocity, _rigidBody2d.velocity.y);
        }
        else
        {
            _rigidBody2d.AddForce(new Vector2(resultHorizontalVelocity - _rigidBody2d.velocity.x, 0f), ForceMode2D.Impulse);
        }
    }
    #endregion
}