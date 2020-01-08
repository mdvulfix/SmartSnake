using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SmartSnake{

    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {

            SceneManager.LoadScene(1);

        }

        public void QuitGame()
        {

            Application.Quit();
            Debug.Log("Game is finished");

        }
    }
}
