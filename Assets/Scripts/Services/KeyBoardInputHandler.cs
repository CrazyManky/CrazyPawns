using Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Services
{
    public class KeyBoardInputHandler : IInputHandler, IInitializable, ITickable
    {
        private CrazyPawnsInputs _inputActionAsset;
        private InputAction _moveAction;
        private InputAction _buttonClickAction;
        public Vector2 Direction { get; private set; }
        public InputAction DragButton => _buttonClickAction;


        [Inject]
        public KeyBoardInputHandler(CrazyPawnsInputs inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
        }

        public void Initialize()
        {
            _moveAction = _inputActionAsset.Player.Move;
            _buttonClickAction = _inputActionAsset.Player.ClickLeftButtton;
            _moveAction.Enable();
            _buttonClickAction.Enable();
        }

        public void Tick()
        {
            Direction = _moveAction.ReadValue<Vector2>();
        }
    }
}