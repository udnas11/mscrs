// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{

    public event Action<float, float> OnStaminaChanged;

    #region public serialised vars
    [SerializeField]
    float _staminaMaximum;
    [SerializeField]
    float _staminaRegenPerSecond;
    [SerializeField]
    Collider2D _groundTrigger;
    [SerializeField]
    float _jumpImpulse;
    [SerializeField]
    float _groundRaycastDistance;
    [SerializeField]
    Transform _enemyRaycastTarget;
    [SerializeField]
    Transform[] _flippables;

    // driven by Animation custom property
    [HideInInspector, SerializeField]
    float _animHorizontalSpeed;
    [HideInInspector, SerializeField]
    float _animHorizontalForcedSpeed;
    #endregion


    #region private protected vars
    PlayerAnimatorController _playerAnimator;
    Rigidbody2D _rigidBody2d;
    SpriteRenderer _spriteRenderer;
    HealthEntity _healthEntity;
    float _horizontalInput;
    bool _facingRight = true;
    bool _queueJump = false;
    bool _queueAirdrop = false;
    int _groundTriggersActive = 0;
    bool _dead;
    float _physicsPenaltyOverTimestamp;
    float _stamina;
    float _startRunTime;

    Coroutine _physicsInhibitorCoroutine;
    #endregion


    #region pub methods
    public bool InAir { get { return _groundTriggersActive == 0; } }
    public Transform EnemyRaycastTarget { get { return _enemyRaycastTarget; } }
    public bool IsDead { get { return _dead; } }
    public HealthEntity HealthEntity { get { return _healthEntity; } }
    public Vector2 VelocityRigidbody { get { return _rigidBody2d.velocity; } }
    public float Stamina { get { return _stamina; } }
    public float StaminaMax { get { return _staminaMaximum; } }
    public bool IsFlippedX { get { return !_facingRight; } }

    public void DecreaseStamina(float cost)
    {
        SetStamina(_stamina - cost);
    }
    #endregion


    #region private protected methods
    void SetStamina(float newVal)
    {
        float newStamina = Mathf.Clamp(newVal, 0f, _staminaMaximum);
        if (newStamina == _stamina)
            return;

        _stamina = newStamina;
        _playerAnimator.SetStamina(_stamina);
        if (OnStaminaChanged != null)
            OnStaminaChanged(_stamina, _staminaMaximum);
    }

    bool RaycastIsGroundBeneath()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, _groundRaycastDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, Vector2.down * _groundRaycastDistance, hit.collider == null ? Color.red : Color.green, 1f);
        return hit.collider != null;
    }
    
    bool GetIsFlipAvailable()
    {
        bool result = _playerAnimator.GetPhaseState(BaseAnimatorController.EAnimationPhase.Roll) || _playerAnimator.GetPhaseState(BaseAnimatorController.EAnimationPhase.Attacking);
        return !result;
    }

    IEnumerator IVelocityInhibitor(float duration)
    {
        float timeStart = Time.time;
        _rigidBody2d.gravityScale = 0.2f;
        while (Time.time < timeStart + duration) // fail-safe. Effect interrupted by _queueAirdrop
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
        if (horizontal != 0f)
            _startRunTime = Time.time;
        else
            _playerAnimator.SetShouldStopRun(Time.time - _startRunTime > 0.25f);

        _playerAnimator.SetRunning(horizontal != 0f);
        _horizontalInput = horizontal;
    }

    private void OnPushForceReceived(Vector2 force, float physicsDuration)
    {
        _rigidBody2d.AddForce(force, ForceMode2D.Impulse);
        _physicsPenaltyOverTimestamp = Time.time + physicsDuration;
    }

    private void OnJumpInput()
    {
        if (InAir == false)
        {
            _playerAnimator.Jump();
        }
        else
        {
            if (RaycastIsGroundBeneath())
                _playerAnimator.Jump();
        }
    }

    private void OnAttackInput(bool immediate)
    {
        if (InAir == immediate)
            _playerAnimator.Attack();
    }

    private void OnAttack2Input(bool immediate)
    {
        if (InAir == immediate)
            _playerAnimator.Attack2();
    }

    private void OnAttack1ChargedInput()
    {
        _playerAnimator.Attack1Charged();
    }

    private void OnAttack2ChargedInput()
    {
        _playerAnimator.Attack2Charged();
    }

    private void OnRollInput()
    {
        if (InAir == false)
        {
            _playerAnimator.Roll();
        }
        else
        {
            if (RaycastIsGroundBeneath())
                _playerAnimator.Roll();
        }
    }

    private void OnAnimationCallbackJumpForce()
    {
        _queueJump = true;
    }

    private void OnPlayGetHitAnimation()
    {
        _playerAnimator.GetHit();
    }

    private void OnAnimationCallbackInhibitPhysics(float duration)
    {
        _physicsInhibitorCoroutine = StartCoroutine(IVelocityInhibitor(duration));
    }

    private void OnAnimationCallbackAirdrop()
    {
        _queueAirdrop = true;
    }

    private void OnDying(int deathAnimationIndex)
    {
        _dead = true;
        _playerAnimator.SetDead(true, deathAnimationIndex);
    }
    #endregion


    #region mono events
    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimatorController>();
        _rigidBody2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthEntity = GetComponent<HealthEntity>();        
    }

    private void Start()
    {
        SetStamina(_staminaMaximum);

        PlayerInputHandler.Instance.OnHorizontalChange += OnHorizontalInputChange;
        PlayerInputHandler.Instance.OnJump += OnJumpInput;
        PlayerInputHandler.Instance.OnAttack1 += OnAttackInput;
        PlayerInputHandler.Instance.OnAttack2 += OnAttack2Input;
        PlayerInputHandler.Instance.OnAttack1Charged += OnAttack1ChargedInput;
        PlayerInputHandler.Instance.OnAttack2Charged += OnAttack2ChargedInput;
        PlayerInputHandler.Instance.OnRoll += OnRollInput;

        _playerAnimator.OnAnimEventJumpApplyForceAction += OnAnimationCallbackJumpForce;
        _playerAnimator.OnAnimEventInhibitPhysicsAction += OnAnimationCallbackInhibitPhysics;
        _playerAnimator.OnAnimEventAirdropAction += OnAnimationCallbackAirdrop;

        _healthEntity.OnDeath += OnDying;
        _healthEntity.OnPushForceReceived += OnPushForceReceived;
        _healthEntity.OnPlayGetHitAnimation += OnPlayGetHitAnimation;
    }

    private void OnDestroy()
    {
        PlayerInputHandler.Instance.OnHorizontalChange -= OnHorizontalInputChange;
        PlayerInputHandler.Instance.OnJump -= OnJumpInput;
        PlayerInputHandler.Instance.OnAttack1 -= OnAttackInput;
        PlayerInputHandler.Instance.OnAttack2 -= OnAttack2Input;
        PlayerInputHandler.Instance.OnAttack1Charged -= OnAttack1ChargedInput;
        PlayerInputHandler.Instance.OnAttack2Charged -= OnAttack2ChargedInput;
        PlayerInputHandler.Instance.OnRoll -= OnRollInput;

        _playerAnimator.OnAnimEventJumpApplyForceAction -= OnAnimationCallbackJumpForce;
        _playerAnimator.OnAnimEventInhibitPhysicsAction -= OnAnimationCallbackInhibitPhysics;
        _playerAnimator.OnAnimEventAirdropAction -= OnAnimationCallbackAirdrop;

        _healthEntity.OnDeath -= OnDying;
        _healthEntity.OnPushForceReceived -= OnPushForceReceived;
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _groundTriggersActive++;
            _playerAnimator.SetInAir(InAir);
        }
    }

    private void OnTriggerExit2D(Collider2D trigger)
    {
        if (trigger.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            _groundTriggersActive--;
            _playerAnimator.SetInAir(InAir);
        }
    }

    private void Update()
    {
        SetStamina(_stamina + _staminaRegenPerSecond * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (_dead)
            return;

        if (_horizontalInput != 0f &&
            (_horizontalInput > 0) != _facingRight &&
            GetIsFlipAvailable())
        {
            _facingRight = _horizontalInput > 0;
            _spriteRenderer.flipX = !_facingRight;
            for (int i = 0; i < _flippables.Length; i++)
                _flippables[i].transform.localScale = new Vector3(_facingRight ? 1f : -1f, 1f, 1f);
        }

        float resultHorizontalVelocity = _animHorizontalSpeed * _horizontalInput + _animHorizontalForcedSpeed * (_facingRight?1f:-1f);
        
        if (InAir == false && Time.time > _physicsPenaltyOverTimestamp)
        {
            _rigidBody2d.velocity = new Vector2(resultHorizontalVelocity, _rigidBody2d.velocity.y);
        }
        else
        {
            _rigidBody2d.AddForce(new Vector2(resultHorizontalVelocity - _rigidBody2d.velocity.x, 0f), ForceMode2D.Impulse);
        }

        if (_queueJump)
        {
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
        /*
        GUILayout.Label("Player velocity: " + _rigidBody2d.velocity);
        GUILayout.Label("Ground collisions: " + _groundTriggersActive);
        GUILayout.Label("Horizontal Input: " + _horizontalInput);
        GUILayout.Label("Anim Horizontal Speed: " + _animHorizontalSpeed);
        GUILayout.Label("Anim Horizontal Forced: " + _animHorizontalForcedSpeed);
        */
        /*
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y = Screen.height - pos.y - 15;
        GUI.Label(new Rect(pos, new Vector2(50, 20)), _stamina.ToString("0.") + "/" + _staminaMaximum.ToString("0."));
        */
    }
    #endregion
}
