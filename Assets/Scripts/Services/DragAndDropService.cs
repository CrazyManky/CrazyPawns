using System;
using Factorys;
using Interfaces;
using Pawn;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Services
{
    public class DragAndDropService : IInitializable, ILateTickable, IDisposable
    {
        private IInputHandler _inputHandler;
        private Camera _camera;
        private IValidatePlacement _gameZone;
        private IPawnRemover _pawnRemover;

        private PawnView _dragObject;
        private Vector3 _offset;
        private Plane _dragPlane = new(Vector3.up, Vector3.zero);

        private bool _isDragging = false;

        [Inject]
        public DragAndDropService(IInputHandler inputHandler, Camera camera, IValidatePlacement gameZone, IPawnRemover pawnRemover)
        {
            _inputHandler = inputHandler;
            _camera = camera;
            _gameZone = gameZone;
            _pawnRemover = pawnRemover;
        }

        public void Initialize()
        {
            _inputHandler.DragButton.started += context => OnDragStarted(context);
            _inputHandler.DragButton.canceled += context => OnResetDrag(context);
        }

        private void OnDragStarted(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out var hit))
                return;

            _dragObject = hit.collider.GetComponent<PawnView>();

            if (_dragObject == null)
                return;

            if (_dragPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);
                _offset = _dragObject.transform.position - hitPoint;
            }


            _isDragging = true;
        }

        public void LateTick()
        {
            if (!_isDragging)
                return;

            Ray ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!_dragPlane.Raycast(ray, out float distance))
                return;

            Vector3 mouseWorldPosition = ray.GetPoint(distance);
            _dragObject.transform.position = mouseWorldPosition + _offset;
            _dragObject.SetPlacementState(_gameZone.Contains(_dragObject.Bounds));
        }


        private void OnResetDrag(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                if (!_gameZone.Contains(_dragObject.Bounds))
                {
                    _pawnRemover.Remove(_dragObject);
                }

                _dragObject = null;
                _isDragging = false;
            }
        }

        public void Dispose()
        {
            _inputHandler.DragButton.started -= OnDragStarted;
            _inputHandler.DragButton.canceled -= OnDragStarted;
        }
    }
}