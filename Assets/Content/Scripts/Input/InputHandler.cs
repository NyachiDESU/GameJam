using UnityEngine;
using UnityEngine.InputSystem;

namespace GameJam
{
    public class InputHandler : MonoBehaviour
    {
        private static InputHandler _instance;

        private InputActions _inputActions;

        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private InterfaceManager _interfaceManager;

        private bool _isMenuOpened = false;

        private void Awake()
        {
            _instance = this;
            _inputActions = new InputActions();
        }

        private void OnEnable()
        {
            _inputActions.Enable();

            _inputActions.Menu.OpenMenu.started += SwitchMenu;
        }

        private void OnDisable()
        {
            _inputActions.Disable();
        }
        
        private void SwitchMenu(InputAction.CallbackContext callback)
        {
            _isMenuOpened = !_isMenuOpened;
            _playerInput.enabled = !_isMenuOpened;

            Cursor.lockState = _isMenuOpened == true ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = _isMenuOpened;

            _interfaceManager.SwitchMenuState(_isMenuOpened);
        }

        public static void EnablePlayerInput()
        {
            _instance._playerInput.enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
