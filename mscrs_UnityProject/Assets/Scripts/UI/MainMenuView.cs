// Made by Alexandru Romanciuc <sanromanciuc@gmail.com>
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityRandom = UnityEngine.Random;
using UnityEngine.UI;

namespace MoonscarsUI
{
    public class MainMenuView : MonoBehaviour
    {

        [SerializeField]
        Button _startButton;
        [SerializeField]
        Button _quitButton;


        void OnStartClick()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        void OnQuitclick()
        {
            Application.Quit();
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _quitButton.onClick.AddListener(OnQuitclick);
        }

    }
}