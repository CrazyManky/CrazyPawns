using System;
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

        private PawnView _dragObject;
        private Vector3 _offset;
        private Plane _dragPlane;

        private bool _isDragging = false;

        [Inject]
        public DragAndDropService(IInputHandler inputHandler, Camera camera)
        {
            _inputHandler = inputHandler;
            _camera = camera;
        }

        public void Initialize()
        {
            _inputHandler.DragButton.started += context => OnDragStarted(context);
            _inputHandler.DragButton.performed += context => ButtonHold(context);
            _inputHandler.DragButton.canceled += context => OnResetDrag(context);
        }

        private void ButtonHold(InputAction.CallbackContext context)
        {
            if (context.performed || _dragObject != null)
            {
                Debug.Log("Подготовли к перетаскиванию");
                _isDragging = true;
                return;
            }

            Debug.Log("Объекта нет");
            _isDragging = false;
        }


        private void OnDragStarted(InputAction.CallbackContext context)
        {
            Ray ray = _camera.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );

            if (!Physics.Raycast(ray, out var hit))
                return;
            
            PawnView dragObject =
                hit.collider.GetComponentInParent<PawnView>();
            
            if (dragObject == null)
                return;
            
            _dragObject = dragObject;

            _dragPlane = new Plane(Vector3.up, Vector3.zero);
            
            if (_dragPlane.Raycast(ray, out float distance))
            {
                Vector3 hitPoint = ray.GetPoint(distance);

                _offset =
                    _dragObject.transform.position - hitPoint;
            }


            _isDragging = true;
        }

        public void LateTick()
        {
            if (!_isDragging)
                return;

            OnDragging();
        }

        private void OnDragging()
        {
            Ray ray = _camera.ScreenPointToRay(
                Mouse.current.position.ReadValue()
            );


            if (!_dragPlane.Raycast(ray, out float distance))
                return;


            Vector3 mouseWorldPosition =
                ray.GetPoint(distance);


            _dragObject.transform.position =
                mouseWorldPosition + _offset;
        }

        private void OnResetDrag(InputAction.CallbackContext context)
        {
            if (context.canceled)
            {
                _dragObject = null;
                _isDragging = false;
            }
        }

        public void Dispose()
        {
            _inputHandler.DragButton.started -= OnDragStarted;
            _inputHandler.DragButton.performed -= OnDragStarted;
            _inputHandler.DragButton.canceled -= OnDragStarted;
        }
    }
}