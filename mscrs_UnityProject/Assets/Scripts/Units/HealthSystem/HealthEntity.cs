// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class HealthEntity : MonoBehaviour
{

    public event Action<Vector2, float> OnPushForceReceived;
    public event Action<int> OnDeath;
    public event Action OnPlayGetHitAnimation;
    public event Action<int, int> OnHealthChanged;

    #region public serialised vars
    [SerializeField]
    int _maxHP;
    [SerializeField]
    DamageReceiver[] _receivers;
    [SerializeField]
    string _deathSoundID;
    #endregion


    #region private protected vars
    int _currentHP;
    bool _dead;
    #endregion


    #region pub methods
    public bool IsDead { get { return _dead; } }

    public void TakeDamage(int damage, int deathAnimationIndex, bool triggerHitAnimation)
    {
        if (_dead)
            return;
        
        _currentHP -= damage;
        if (OnHealthChanged != null)
            OnHealthChanged(_currentHP, _maxHP);

        if (_currentHP <= 0)
            Die(deathAnimationIndex);
        else if (triggerHitAnimation && OnPlayGetHitAnimation != null)
            OnPlayGetHitAnimation();
    }
    
    public void TakePushForce(Vector2 force, float physicsDuration)
    {
        if (OnPushForceReceived != null)
            OnPushForceReceived(force, physicsDuration);
    }

    public void Die(int deathAnimationIndex)
    {
        if (_dead)
            return;
        
        _dead = true;
        if (string.IsNullOrEmpty(_deathSoundID) == false)
            AudioController.Play(_deathSoundID, transform.position);
        if (OnDeath != null)
            OnDeath(deathAnimationIndex);
    }
    #endregion


    #region private protected methods
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _currentHP = _maxHP;
    }

    private void OnGUI()
    {
        Vector2 pos = Camera.main.WorldToScreenPoint(transform.position);
        pos.y = Screen.height - pos.y;
        GUI.Label(new Rect(pos, new Vector2(50, 20)), _currentHP.ToString() + "/" + _maxHP.ToString());
    }
    #endregion
}