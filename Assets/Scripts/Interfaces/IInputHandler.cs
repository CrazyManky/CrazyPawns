using UnityEngine;
using UnityEngine.InputSystem;

namespace Interfaces
{
    public interface IInputHandler
    {
        public Vector2 Direction { get; }
        public InputAction DragButton { get; }
    }
}