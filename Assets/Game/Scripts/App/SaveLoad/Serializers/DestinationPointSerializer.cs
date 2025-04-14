using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using System.Collections.Generic;

public sealed class DestinationPointSerializer : GameSerializer<EntityWorld, DestinationPointData>
{
    protected override void Deserialize(EntityWorld service, DestinationPointData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            DestinationPointData destinationPointData = JsonConvert.DeserializeObject<DestinationPointData>(component.Value);

            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out DestinationPoint destinationPoint))
            {
                destinationPoint.Value = destinationPointData.DestinationPoint;
            }           
        }
    }

    protected override DestinationPointData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        DestinationPointData destinationpointData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out DestinationPoint destinationPoint))
            {
                destinationpointData.DestinationPoint = destinationPoint.Value;
                string json = JsonConvert.SerializeObject(destinationpointData);
                components[entity.Id] = json;
            }
        }
        destinationpointData.Components = components;
        return destinationpointData;
    }
}
