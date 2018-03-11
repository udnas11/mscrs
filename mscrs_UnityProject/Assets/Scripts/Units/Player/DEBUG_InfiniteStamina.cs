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

    private void OnGUI()
    {
        Color gui = GUI.color;
        GUI.color = _active ? Color.green : Color.red;
        if (GUI.Button(new Rect(Screen.width - 150, 50, 150, 50), "Toggle use stamina"))
            _active = !_active;
        GUI.color = gui;
    }

}