using System;
using CrazyPawn;
using Pawn;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Factorys
{
    [Serializable]
    public class PawnFactory : IInitializable
    {
         private CrazyPawnSettings _crazyPawnSettings;
         private PawnView _prefabView;

        [Inject]
        public PawnFactory(CrazyPawnSettings crazyPawnSettings, PawnView prefabView)
        {
            _crazyPawnSettings = crazyPawnSettings;
            _prefabView = prefabView;
        }

        public void Initialize()
        {
            for (int i = 0; i <= _crazyPawnSettings.InitialPawnCount; i++)
            {
                var instance = Object.Instantiate(_prefabView);
                Vector2 point = Random.insideUnitCircle * _crazyPawnSettings.InitialZoneRadius;
                instance.transform.position = new Vector3(point.x, 0, point.y);
            }
        }
    }
}