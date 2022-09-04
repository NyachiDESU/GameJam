using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameJam
{
    public class InterfaceManager : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;

        [SerializeField] private Animator _cabinetAnimator;
        [SerializeField] private Animator _doorAnimator;

        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _optionsWindow;

        [SerializeField] private GameObject _firstOptions;
        [SerializeField] private GameObject _secondOptions;
        [SerializeField] private GameObject _gameOver;

        [SerializeField] private Button _gameOverButton;

        private bool _mainOptionsIsSecond = false;

        private static InterfaceManager _instance;

        private void Start()
        {
            _instance = this;
            GameOverTrigger.OnGameEnded = OpenGameOverWindow;
        }

        public void SwitchMenuState(bool state)
        {
            _canvas.SetActive(state);

            if (!state)
                OpenMainMenu();
        }

        public void OpenMainMenu()
        {
            _optionsWindow.SetActive(false);
            _menuWindow.SetActive(true);
        }

        public void OpenOptions()
        {
            _optionsWindow.SetActive(true);
            _menuWindow.SetActive(false);

            if (_mainOptionsIsSecond)
                OpenSecondOptions();
            else
                OpenFirstOptions();
        }

        private void OpenFirstOptions()
        {
            _firstOptions.SetActive(true);
            _secondOptions.SetActive(false);
        }

        private void OpenSecondOptions()
        {
            _firstOptions.SetActive(false);
            _secondOptions.SetActive(true);
        }

        private void OpenGameOverWindow()
        {
            InputHandler.DisablePlayerInput();

            _canvas.SetActive(true);

            _firstOptions.SetActive(false);
            _secondOptions.SetActive(false);
            _gameOver.SetActive(true);

            _gameOverButton.onClick.AddListener(() =>
            {
                Debug.Log("Player quit");
                QuitGame();
            });
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public void OpenDoorAndCloseMenu()
        {
            StartCoroutine(WaitForAnimation());
            InputHandler.EnablePlayerInput();
            SwitchMenuState(false);
        }

        private IEnumerator WaitForAnimation()
        {
            AudioManager.PlayClip(ClipType.MoveShelf, _cabinetAnimator.transform.position, 0.9f);
            _cabinetAnimator.SetTrigger("MoveBookcase");
            yield return new WaitForSeconds(3);
            AudioManager.PlayClip(ClipType.DoorOpening, _doorAnimator.transform.position, 0.9f);
            _doorAnimator.SetTrigger("OpenDoor");
        }

        public static void SetSecondOptions()
        {
            _instance._mainOptionsIsSecond = true;
        }
    }
}
