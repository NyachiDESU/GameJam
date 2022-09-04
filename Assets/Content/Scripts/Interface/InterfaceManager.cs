using UnityEngine;

namespace GameJam
{
    public class InterfaceManager : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [SerializeField] private GameObject _menuWindow;
        [SerializeField] private GameObject _optionsWindow;

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
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
