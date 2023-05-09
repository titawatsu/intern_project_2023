using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using fps_16bit;
using UnityEngine.InputSystem;

namespace fps_16bit
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private GameObject pauseUi;
        [SerializeField] private GameManager gameManager;
        public AudioSource bgSound;

        public static bool paused = false;

        public InputActionReference pausedButton;


        private void OnEnable()
        {
            pausedButton.action.Enable();
            pausedButton.action.performed += PauseHandler;
        }

        private void OnDisable()
        {
            pausedButton.action.Disable();
            pausedButton.action.performed -= PauseHandler;
        }

        private void PauseHandler(InputAction.CallbackContext context)
        {
            PauseGame();
        }

        private void Start()
        {
            ResumeGame();
        }

        private void DeterminePause() // funtions about game states
        {
            if (paused)
                ResumeGame();
            else
                PauseGame();
        }

        public void PauseGame()
        {
            Time.timeScale = 0f;
            bgSound.Pause();
            paused = true;
            pauseUi.SetActive(true);

            //AudioListener.pause = true; for pause all sound which make button sound mute
        }

        public void ResumeGame()
        {
            Time.timeScale = 1f;
            bgSound.UnPause();
            paused = false;
            pauseUi.SetActive(false);

            //AudioListener.pause = false;
        }

        public void GotoMainmenuButton() => gameManager.LoadLevel(0); // load main menu level 

    }
}
