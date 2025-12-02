using UnityEngine;
using UnityEngine.SceneManagement;
using Quest;

namespace Game
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private GameObject newGamePanel;
        [SerializeField] private string firstScene;

        public static event System.Action NewGameStarted;

        private SaveLoadManager _saveLoadManager;
        private void Awake()
        {
            _saveLoadManager = GetComponent<SaveLoadManager>();
        }
        void Start()
        {
            newGamePanel.SetActive(false);
        }
        void Update()
        {
            if (newGamePanel != null && newGamePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
            {
                HideNewGamePanel();
            }
        }

        public void StartGameButton()
        {
            if (_saveLoadManager != null && _saveLoadManager.CanLoad())
            {
                ShowNewGamePanel();
            }
            else
            {
                StartGame();
            }
        }
        public void ShowNewGamePanel()
        {
            newGamePanel.SetActive(true);
        }
        public void StartGame()
        {
            if (_saveLoadManager != null)
                _saveLoadManager.ClearSavs();
            QuestCollection.ClearQuests();

            // �������� ��� ��������� �������� �� ���������
            Resources.Gold = 50;
            Resources.Food = 50;
            Resources.PeopleSatisfaction = 6;
            Resources.CastleStrength = 200;

            NewGameStarted?.Invoke();
            if (firstScene == "StartDay") LoadingScreenManager.Instance.OnConfirmEndOfDay();
            else LoadingScreenManager.Instance.LoadScene(firstScene);
        }

        public void BackToMenuGame()
        {
            Game.Resources.Gold = 50;
            Game.Resources.Food = 50;
            Game.Resources.PeopleSatisfaction = 6;
            Game.Resources.CastleStrength = 200;

            LoadingScreenManager.Instance.LoadScene("MainMenu");
        }

        public void HideNewGamePanel()
        {
            newGamePanel.SetActive(false);
        }

    }

}
