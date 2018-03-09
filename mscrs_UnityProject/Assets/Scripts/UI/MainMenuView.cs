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
        Button _readmeButton;
        [SerializeField]
        Button _quitButton;

        [SerializeField]
        GameObject _readmePanel;


        void OnStartClick()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TestLevel", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        void OnReadmeClick()
        {
            _readmePanel.SetActive(!_readmePanel.activeSelf);
        }

        void OnQuitclick()
        {
            Application.Quit();
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _readmeButton.onClick.AddListener(OnReadmeClick);
            _quitButton.onClick.AddListener(OnQuitclick);
        }
    }
}