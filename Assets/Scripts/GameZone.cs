using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GameZone : MonoBehaviour, IValidatePlacement
{
    public MeshRenderer MeshRenderer;

    private Bounds _bounds;

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
        _bounds = MeshRenderer.bounds;
    }

    public bool Contains(Bounds bounds)
    {
        return _bounds.Intersects(bounds);
    }
}

public interface IValidatePlacement
{
    bool Contains(Bounds bounds);
}