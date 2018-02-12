// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Collider2D _groundTrigger;
    [SerializeField]
    float _jumpImpulse;
    [SerializeField]
    float _groundRaycastDistance;


    // driven by Animation custom property
    [HideInInspector, SerializeField]
    float _animHorizontalSpeed;
    [HideInInspector, SerializeField]
    float _animHorizontalForcedSpeed;
    #endregion


    #region private protected vars
    UnitAnimatorController _unitAnimator;
    Rigidbody2D _rigidBody2d;
    float _horizontalInput;
    bool _facingRight = true;
    bool _queueJump = false;
    bool _queueAirdrop = false;
    int _groundTriggersActive = 0;

    Coroutine _physicsInhibitorCoroutine;
    #endregion


    #region pub methods
    public bool InAir { get { return _groundTriggersActive == 0; } }
    #endregion


    #region private protected methods
    bool RaycastIsGroundBeneath()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, _groundRaycastDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * _groundRaycastDistance, hit.collider == null ? Color.red : Color.green, 1f);
        return hit.collider != null;
    }
    
    bool GetIsFlipAvailable()
    {
        bool result = _unitAnimator.GetPhaseState(UnitAnimatorController.EAnimationPhase.Roll) || _unitAnimator.GetPhaseState(UnitAnimatorController.EAnimationPhase.Attacking);
        return !result;
    }

    IEnumerator IVelocityInhibitor()
    {
        float timeStart = Time.time;
        _rigidBody2d.gravityScale = 0.2f;
        while (Time.time < timeStart + 3f) // fail-safe. Effect interrupted by _queueAirdrop
        {
            yield return new WaitForFixedUpdate();
            _rigidBody2d.velocity = _rigidBody2d.velocity * 0.5f;
        }
        _rigidBody2d.gravityScale = 1f;
    }
    #endregion


    #region events
    private void OnHorizontalInputChange(float horizontal)
    {
        _unitAnimator.SetRunning(horizontal != 0f);
        /*
        if (horizontal != 0f && GetIsFlipAvailable())
        {
            _facingRight = horizontal > 0;
            _unitAnimator.SetFlipX(!_facingRight);
        }
        */
        _horizontalInput = horizontal;
    }

    private void OnJumpInput()
    {
        if (InAir == false)
        {
            _unitAnimator.Jump();
        }
        else
        {
            if (RaycastIsGroundBeneath())
                _unitAnimator.Jump();
        }
    }

    private void OnAttackInput()
    {
        //if (InAir == false)
        {
            _unitAnimator.Attack();
        }
    }

    private void OnRollInput()
    {
        if (InAir == false)
        {
            _unitAnimator.Roll();
        }
        else
        {
            if (RaycastIsGroundBeneath())
                _unitAnimator.Roll();
        }
    }

    private void OnAnimationCallbackJumpForce()
    {
        _queueJump = true;
    }

    private void OnAnimationCallbackInhibitPhysics()
    {
        _physicsInhibitorCoroutine = StartCoroutine(IVelocityInhibitor());
    }

    private void OnAnimationCallbackAirdrop()
    {
        _queueAirdrop = true;
    }
    #endregion


    #region mono events
    private void Awake()
    {
        _unitAnimator = GetComponent<UnitAnimatorController>();
        _rigidBody2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        PlayerInputHandler.Instance.OnHorizontalChange += OnHorizontalInputChange;
        PlayerInputHandler.Instance.OnJump += OnJumpInput;
        PlayerInputHandler.Instance.OnAttack += OnAttackInput;
        PlayerInputHandler.Instance.OnRoll += OnRollInput;

        _unitAnimator.OnAnimEventJumpApplyForceAction += OnAnimationCallbackJumpForce;
        _unitAnimator.OnAnimEventAirdropInhibitPhysicsAction += OnAnimationCallbackInhibitPhysics;
        _unitAnimator.OnAnimEventAirdropAction += OnAnimationCallbackAirdrop;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        _groundTriggersActive++;
        _unitAnimator.SetInAir(InAir);
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        _groundTriggersActive--;
        _unitAnimator.SetInAir(InAir);
    }

    private void FixedUpdate()
    {
        if (_horizontalInput != 0f &&
            (_horizontalInput > 0) != _facingRight &&
            GetIsFlipAvailable())
        {
            _facingRight = _horizontalInput > 0;
            _unitAnimator.SetFlipX(!_facingRight);
        }

        float resultHorizontalVelocity = _animHorizontalSpeed * _horizontalInput + _animHorizontalForcedSpeed * (_facingRight?1f:-1f);

        if (InAir == false)// && _unitAnimator.GetPhaseState(UnitAnimatorController.EAnimationPhase.Jump) == false)
        {
            _rigidBody2d.velocity = new Vector2(resultHorizontalVelocity, _rigidBody2d.velocity.y);
        }
        else
        {
            _rigidBody2d.AddForce(new Vector2(resultHorizontalVelocity - _rigidBody2d.velocity.x, 0f), ForceMode2D.Impulse);
        }

        if (_queueJump)
        {
            //_rigidBody2d.AddForce(new Vector2(0f, _jumpImpulse), ForceMode2D.Impulse);
            _rigidBody2d.velocity = new Vector2(_rigidBody2d.velocity.x, _jumpImpulse);
            _queueJump = false;
        }
        if (_queueAirdrop)
        {
            StopCoroutine(_physicsInhibitorCoroutine);
            _physicsInhibitorCoroutine = null;
            _rigidBody2d.gravityScale = 1f;
            _rigidBody2d.velocity = new Vector2(_rigidBody2d.velocity.x, -6f);
            _queueAirdrop = false;
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Player velocity: " + _rigidBody2d.velocity);
        GUILayout.Label("Ground collisions: " + _groundTriggersActive);
        GUILayout.Label("Horizontal Input: " + _horizontalInput);
        GUILayout.Label("Anim Horizontal Speed: " + _animHorizontalSpeed);
        GUILayout.Label("Anim Horizontal Forced: " + _animHorizontalForcedSpeed);
    }
    #endregion
}
