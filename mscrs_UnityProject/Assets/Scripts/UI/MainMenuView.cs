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
            Application.OpenURL("https://docs.google.com/presentation/d/1iFLv4k--R-AFMvZN_9haiPR4uiIVRZ4XsHGe1I2g28s/edit?usp=sharing");
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

            UnityEngine.Analytics.Analytics.CustomEvent("build_2");
        }
    }
}