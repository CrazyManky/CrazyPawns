using Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Services
{
    public class CameraMovementController : IInitializable, ILateTickable
    {
        private IInputHandler _inputHandler;
        private Camera _camera;
        private GameZone _gameZone;

        private float _moveSpeed = 5f;
        private float _offset = 3.5f;

        private Bounds _bounds;

        [Inject]
        public CameraMovementController(IInputHandler inputHandler, Camera camera, GameZone gameZone)
        {
            _inputHandler = inputHandler;
            _camera = camera;
            _gameZone = gameZone;
        }

        public void Initialize()
        {
            _bounds = _gameZone.MeshRenderer.bounds;
        }

        public void LateTick() => _camera.transform.position = GetClampedPosition(_inputHandler.Direction);

        private Vector3 GetClampedPosition(Vector2 direction)
        {
            Vector3 position = _camera.transform.position += new Vector3(direction.x, 0, direction.y) * _moveSpeed * Time.deltaTime;
            var targetPosition = new Vector3
            (
                Mathf.Clamp(position.x, _bounds.min.x - _offset, _bounds.max.x + _offset),
                position.y,
                Mathf.Clamp(position.z, _bounds.min.z - _offset, _bounds.max.z + _offset)
            );


            return targetPosition;
        }
    }
}