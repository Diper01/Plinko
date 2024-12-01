using UnityEngine;
using Zenject;
public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject ballPoolPrefab;
    public override void InstallBindings()
    {
        Container.Bind<BallPool>().FromComponentInHierarchy().AsSingle().NonLazy();
        Container.Bind<TweenManager>().FromComponentInHierarchy().AsSingle().NonLazy();
    }
}
