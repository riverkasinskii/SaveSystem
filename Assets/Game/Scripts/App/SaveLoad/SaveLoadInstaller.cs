using UnityEngine;
using Zenject;

[CreateAssetMenu(
        fileName = "SaveLoadInstaller",
        menuName = "Zenject/App/New SaveLoadInstaller"
    )]
public sealed class SaveLoadInstaller : ScriptableObjectInstaller
{    
    public override void InstallBindings()
    {
        this.Container.Bind<GameSaveLoader>().AsSingle();
        this.Container.BindInterfacesTo<EntitySerializer>().AsSingle();
        this.Container.BindInterfacesTo<CountdownSerializer>().AsSingle();
        this.Container.BindInterfacesTo<DestinationPointSerializer>().AsSingle();
        this.Container.BindInterfacesTo<HealthSerializer>().AsSingle();
        this.Container.BindInterfacesTo<ResourceBagSerializer>().AsSingle();
        this.Container.BindInterfacesTo<ProductionOrderSerializer>().AsSingle();    
        this.Container.BindInterfacesTo<TargetObjectSerializer>().AsSingle();        
        this.Container.BindInterfacesTo<TeamTypeSerializer>().AsSingle();
    }
}
