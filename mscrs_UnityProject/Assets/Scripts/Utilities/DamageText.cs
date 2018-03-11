// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class DamageText : MonoBehaviour
{
    #region public serialised vars
    [SerializeField]
    TextMesh _text;
    [SerializeField]
    float _duration;
    [SerializeField]
    AnimationCurve _heightAnim;
    [SerializeField]
    AnimationCurve _alphaAnim;
    #endregion


    #region private protected vars
    Vector2 _offset;
    #endregion


    #region pub methods
    public void Init(int damage, Vector2 offset, Color color, Transform sourceTransform = null)
    {
        if (sourceTransform)
            transform.position = sourceTransform.position;

        transform.Translate(offset, Space.World);

        _text.text = damage.ToString();

        StartCoroutine(IAnimation(damage, color));
    }
    #endregion


    #region private protected methods
    IEnumerator IAnimation(int damage, Color color)
    {
        float startTime = Time.time;
        float endTime = startTime + _duration;
        float t = 0f;
        while (Time.time < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.time);
            _text.color = new Color(color.r, color.g, color.b, _alphaAnim.Evaluate(t));
            _text.transform.localPosition = new Vector2(0f, _heightAnim.Evaluate(t));
            yield return null;
        }
        Destroy(gameObject);
    }
    #endregion


    #region events
    #endregion


    #region mono events
    #endregion
}