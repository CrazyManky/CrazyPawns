using CrazyPawn;
using Factorys;
using Pawn;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class EntryPoint : LifetimeScope
{
    [SerializeField] private CrazyPawnSettings _crazyPawnSettings;
    [SerializeField] private PawnView _prefabView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_crazyPawnSettings);
        builder.RegisterInstance(_prefabView);
        builder.Register<PawnFactory>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
    }

    private void Start()
    {
        var factory = Container.Resolve<PawnFactory>();
    }
}