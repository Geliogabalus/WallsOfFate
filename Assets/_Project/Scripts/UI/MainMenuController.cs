using UnityEngine;
using UnityEngine.SceneManagement;
using Quest;

namespace Game.UI
{
    [RequireComponent(typeof(SaveLoadManager))]
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

        private SaveLoadManager _saveLoadManager;

        SaveLoadManager SaveLoadManager => _saveLoadManager;

        private void Awake()
        {
            _saveLoadManager = GetComponent<SaveLoadManager>();

            if (SaveLoadManager.CanLoad())
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
            if (SaveLoadManager.CanLoad())
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
            SaveLoadManager.LoadGame();
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
            SaveLoadManager.ClearSavs();
            QuestCollection.ClearQuests();

            // Initialize starting resources            
            Resources.Gold = 50;
            Resources.Food = 50;
            Resources.PeopleSatisfaction = 6;
            Resources.CastleStrength = 200;

            NewGameStarted?.Invoke();

            LoadingScreenManager.Instance.LoadScene(firstScene);
        }
    }
}
