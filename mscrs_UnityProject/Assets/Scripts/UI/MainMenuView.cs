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
        Button _urlButton;
        [SerializeField]
        Button _quitButton;


        void OnStartClick()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Disclaimer", UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        void OnURLClick()
        {
            Application.OpenURL("https://docs.google.com/presentation/d/1OfhmMNdyiu8P-Qrqz2zKHJmw-DlX8cwGqtNHj7O-7vw/edit#slide=id.p");
        }

        void OnQuitclick()
        {
            Application.Quit();
        }

        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _urlButton.onClick.AddListener(OnURLClick);
            _quitButton.onClick.AddListener(OnQuitclick);
        }
    }
}