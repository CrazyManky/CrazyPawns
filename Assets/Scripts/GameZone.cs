using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GameZone : MonoBehaviour
{
    public MeshRenderer MeshRenderer;

    private void Awake()
    {
        MeshRenderer = GetComponent<MeshRenderer>();
    }
}