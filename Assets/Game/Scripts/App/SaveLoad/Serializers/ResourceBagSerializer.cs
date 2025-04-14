using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using System.Collections.Generic;

public sealed class ResourceBagSerializer : GameSerializer<EntityWorld, ResourceBagData>
{
    protected override void Deserialize(EntityWorld service, ResourceBagData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            ResourceBagData resourceBagData = JsonConvert.DeserializeObject<ResourceBagData>(component.Value);
                        
            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out ResourceBag resourceBag))
            {
                resourceBag.Current = resourceBagData.CurrentResourceBag;
                resourceBag.Type = resourceBagData.Type;
            }
        }
    }

    protected override ResourceBagData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        ResourceBagData resourceBagData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out ResourceBag resourceBag))
            {
                resourceBagData.CurrentResourceBag = resourceBag.Current;
                resourceBagData.Type = resourceBag.Type;
                string json = JsonConvert.SerializeObject(resourceBagData);
                components[entity.Id] = json;
            }
        }
        resourceBagData.Components = components;
        return resourceBagData;
    }
}
