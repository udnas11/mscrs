// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class SpritesheetController : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    Sprite[] _spriteArray;
    [SerializeField]
    float _interval = 0.09f;
    #endregion


    #region private protected vars
    SpriteRenderer _spriteRenderer;
    int _frame = 0;
    Coroutine _playCoroutine;
    #endregion


    #region pub methods
    #endregion


    #region private protected methods
    IEnumerator CPlayAnimation()
    {
        for (;;)
        {
            _spriteRenderer.sprite = _spriteArray[_frame++];
            yield return new WaitForSeconds(_interval);
            if (_frame >= _spriteArray.Length)
                _frame = 0;
        }
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _playCoroutine = StartCoroutine(CPlayAnimation());
    }

    private void OnDisable()
    {
        StopCoroutine(_playCoroutine);
        _playCoroutine = null;
    }
    #endregion
}
