using System;
using System.Collections.Generic;
using UnityEngine;


namespace Pawn
{
    public class PawnView : MonoBehaviour
    {
        [SerializeField] private Color _defaultColor;
        [SerializeField] private Color _delitedColor;
        [SerializeField] private List<MeshRenderer> _meshRenderers;

        private Bounds _localBounds;

        private static readonly int BaseMap = Shader.PropertyToID("_BaseColor");
        private MaterialPropertyBlock _materialPropertyBlock;

        public Bounds Bounds
        {
            get
            {
                var bounds = _localBounds;
                bounds.center = transform.TransformPoint(_localBounds.center);
                return bounds;
            }
        }

        private void Awake()
        {
            var localBounds = new Bounds(_meshRenderers[0].transform.localPosition, Vector3.zero);

            foreach (var meshRenderer in _meshRenderers)
            {
                localBounds.Encapsulate(meshRenderer.bounds);
            }

            _localBounds = localBounds;
            _materialPropertyBlock = new();
        }


        public void SetPlacementState(bool inZone)
        {
            var color = inZone ? _defaultColor : _delitedColor;
            _materialPropertyBlock.SetColor(BaseMap, color);

            foreach (var meshRenderer in _meshRenderers)
                meshRenderer.SetPropertyBlock(_materialPropertyBlock);
        }
    }
}