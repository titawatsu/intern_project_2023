using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using fps_16bit;

namespace fps_16bit
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;
        public PauseController pauseController;
        public GameObject GameOverUi;

        #region START_UPDATE
        private void Start()
        {
            SetGameResume();
        }

        private void Awake()
        {
            if (instance != null && instance != this) Destroy(this);

            else instance = this;

        }
        #endregion
        
        private void SetGameResume()
        {
            pauseController.ResumeGame(); //For making Time.timeScale = 1f;
            GameOverUi.SetActive(false);
        }

        public void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

        public void LoadNextLevel()
        {
            int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneBuildIndex == SceneManager.sceneCountInBuildSettings)
            {
                LoadLevel(0);

            }
            else
            {
                SceneManager.LoadScene(nextSceneBuildIndex);
            }
        }

        private int GetCurrentSceneIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }

        public void RestartLevel()
        {
            int currentSceneBuildIndex = GetCurrentSceneIndex();

            SceneManager.LoadScene(currentSceneBuildIndex);
            
        }

        public void ProcessPlayerDeath()
        {
            GameOver();
        }
        
        private void GameOver()
        {

            Time.timeScale = 0f;
            PauseController.paused = true; // if use pauseController.paused = true;, it cannot be accessed with an instance reference; qualify it with a type name instead (PauseController)

            Cursor.lockState = CursorLockMode.Confined;

            pauseController.bgSound.Pause();

            Destroy(this.gameObject);
            GameOverUi.SetActive(true);

        }
        
    }
}

