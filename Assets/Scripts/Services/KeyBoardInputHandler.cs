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
        public Vector2 Direction { get; private set; }

        [Inject]
        public KeyBoardInputHandler(CrazyPawnsInputs inputActionAsset)
        {
            _inputActionAsset = inputActionAsset;
        }

        public void Initialize()
        {
            _moveAction = _inputActionAsset.Player.Move;
            _moveAction.Enable();
        }

        public void Tick()
        {
            Direction = _moveAction.ReadValue<Vector2>();
        }
    }
}