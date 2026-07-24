using CrazyPawn;
using Factorys;
using Pawn;
using Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EntryPoint : LifetimeScope
{
    [SerializeField] private GameZone _gameZone;
    [SerializeField] private Camera _camera;
    [SerializeField] private CrazyPawnSettings _crazyPawnSettings;
    [SerializeField] private PawnView _prefabView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_crazyPawnSettings);
        builder.RegisterInstance(_prefabView);
        builder.RegisterInstance(_camera);
        builder.RegisterInstance(_gameZone).AsImplementedInterfaces().AsSelf();
        builder.Register<CrazyPawnsInputs>(Lifetime.Singleton);
        builder.Register<PawnFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<KeyBoardInputHandler>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<CameraMovementController>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        builder.Register<DragAndDropService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }

    private void Start()
    {
        var factory = Container.Resolve<PawnFactory>();
        var dragAndDropService = Container.Resolve<DragAndDropService>();
    }
}