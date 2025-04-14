using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using System.Collections.Generic;

public sealed class CountdownSerializer : GameSerializer<EntityWorld, CountdownData>
{
    protected override void Deserialize(EntityWorld service, CountdownData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            CountdownData componentData = JsonConvert.DeserializeObject<CountdownData>(component.Value);                                    
            
            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out Countdown countDown))
            {
                countDown.Current = componentData.Current;
            }                      
        }
    }

    protected override CountdownData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        CountdownData countdownData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out Countdown countDown))
            {
                countdownData.Current = countDown.Current;
                string json = JsonConvert.SerializeObject(countdownData);
                components[entity.Id] = json;
            }    
        }
        countdownData.Components = components;
        return countdownData;
    }        
}
