// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;

public class DEBUG_InfiniteStamina : MonoBehaviour
{

    bool _active;
    PlayerController _player;

    private void Awake()
    {
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            _active = !_active;

        if (_active)
            _player.DecreaseStamina(-100f);
    }

}