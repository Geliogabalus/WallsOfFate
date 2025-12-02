using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private GameObject newGamePanel;
        [SerializeField] private string firstScene;

        public static event System.Action NewGameStarted;

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

        public void ShowNewGamePanel()
        {
            newGamePanel.SetActive(true);
        }

        public void HideNewGamePanel()
        {
            newGamePanel.SetActive(false);
        }

    }

}
