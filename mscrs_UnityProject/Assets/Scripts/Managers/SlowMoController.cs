// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class SlowMoController : Singleton<SlowMoController>
{
    #region public serialised vars
    [SerializeField]
    KeyCode _slowMoButton;
    #endregion


    #region private protected vars
    bool _slowMoDebug = false;
    float _slowMoAccumulated = 1f;
    #endregion


    #region pub methods
    public void ApplyCritSlowmo()
    {
        GenericSettingsSO genericSettings = AssetDatabaseSO.Instance.GenericSettings;
        ApplySlowmo(genericSettings.CritSlowmotionCurve, genericSettings.CritSlowmotionDuration);
    }

    public void ApplySlowmo(AnimationCurve curve, float duration)
    {
        StartCoroutine(IApplySlowmo(curve, duration));
    }

    public void ApplyFreeze(float duration)
    {
        StartCoroutine(IApplyFreeze(duration));
    }
    #endregion


    #region private protected methods
    IEnumerator IApplySlowmo(AnimationCurve curve, float duration)
    {
        float startTime = Time.unscaledTime;
        float endTime = startTime + duration;
        float t;
        while (Time.unscaledTime < endTime)
        {
            t = Mathf.InverseLerp(startTime, endTime, Time.unscaledTime);
            _slowMoAccumulated *= curve.Evaluate(t);
            yield return null;
        }
    }

    IEnumerator IApplyFreeze(float duration)
    {
        float startTime = Time.unscaledTime;
        float endTime = startTime + duration;
        while (Time.unscaledTime < endTime)
        {
            _slowMoAccumulated = 0f;
            yield return null;
        }
    }
    #endregion


    #region events
    #endregion


    #region mono events
    private void Awake()
    {
        RegisterSingleton(this);
    }

    private void Update()
    {
        float lastTimeScale = Time.timeScale;
        float newTimeScale = _slowMoDebug ? 0.2f : _slowMoAccumulated;
        Time.timeScale = newTimeScale;
        _slowMoAccumulated = 1f;
        /*
        //if (lastTimeScale != newTimeScale)
        {
            List<AudioObject> activeSounds = AudioController.GetPlayingAudioObjectsInCategory("SFX");
            foreach (var sound in activeSounds)
                sound.pitch = Mathf.Max(newTimeScale, 0.2f);
        }
        */

        if (Input.GetKeyDown(_slowMoButton))
            _slowMoDebug = !_slowMoDebug;
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width - 150, Screen.height - 25, 150, 25), "Time scale: " + Time.timeScale);
        /*if (GUI.Button(new Rect(Screen.width - 150, 50, 150, 25), "Toggle slowmo"))
            _slowMoDebug = !_slowMoDebug;*/
    }
    #endregion
}