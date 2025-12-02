using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [Header("Buttons")]
        public GameObject startGameButton;
        public GameObject continueGameButton;
        public GameObject settingsButton;
        public GameObject exitGameButton;

        [Header("UI Panels")]
        public GameObject newGameConfirmationPanel;        
        public GameObject exitConfirmationPanel;
        public GameObject settingsPanel;

        public string firstScene;

        public static event System.Action NewGameStarted;

        private void Awake()
        {
            if (Data.SaveLoadManager.CanLoad())
            {
                continueGameButton.SetActive(true);
            }
            else
            {
                continueGameButton.SetActive(false);
            }
        }

        public void OnStartGameButtonClick()
        {
            if (Data.SaveLoadManager.CanLoad())
            {
                newGameConfirmationPanel.SetActive(true);
            }
            else
            {
                StartGame();
            }
        }

        public void OnContinueGameButtonClick()
        {
            Data.SaveLoadManager.LoadGame();
        }

        public void OnSettingsButtonClick()
        {
            settingsPanel.SetActive(true);
        }

        public void OnExitButtonClick()
        {
            exitConfirmationPanel.SetActive(true);
        }

        public void StartGame()
        {
            Data.SaveLoadManager.Clear();

            NewGameStarted?.Invoke();

            LoadingScreenManager.Instance.LoadScene(firstScene);
        }
    }
}
