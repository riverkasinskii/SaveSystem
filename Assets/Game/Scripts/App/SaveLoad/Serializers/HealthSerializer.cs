using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using System.Collections.Generic;

public sealed class HealthSerializer : GameSerializer<EntityWorld, HealthData>
{
    protected override void Deserialize(EntityWorld service, HealthData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            HealthData healthData = JsonConvert.DeserializeObject<HealthData>(component.Value);
                        
            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out Health health))
            {
                health.Current = healthData.Health;
            }
        }
    }

    protected override HealthData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        HealthData healthData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out Health health))
            {
                healthData.Health = health.Current;
                string json = JsonConvert.SerializeObject(healthData);
                components[entity.Id] = json;
            }
        }
        healthData.Components = components;
        return healthData;
    }        
}
