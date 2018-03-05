// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class SoundHelper : MonoBehaviour
{
    public void PlaySound(string soundID)
    {
        AudioController.Play(soundID);
    }

    public void PlaySoundThisPosition(string soundID)
    {
        AudioController.Play(soundID, transform.position);
    }

    public void PlaySoundThisTransform(string soundID)
    {
        AudioController.Play(soundID, transform);
    }
}