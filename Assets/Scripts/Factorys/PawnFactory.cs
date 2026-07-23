using System;
using CrazyPawn;
using Pawn;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class PawnFactory
{
    [SerializeField] private CrazyPawnSettings _crazyPawnSettings;
    [SerializeField] private PawnView _prefabView;

    public void CreatePawn()
    {
        for (int i = 0; i <= _crazyPawnSettings.InitialPawnCount; i++)
        {
            var instance = Object.Instantiate(_prefabView);
            Vector2 point = Random.insideUnitCircle * _crazyPawnSettings.InitialZoneRadius;
            instance.transform.position = new Vector3(point.x, 0, point.y);
        }
    }
}