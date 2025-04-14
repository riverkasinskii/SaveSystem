using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using System.Collections.Generic;

public sealed class ProductionOrderSerializer : GameSerializer<EntityWorld, ProductionOrderData>
{
    protected override void Deserialize(EntityWorld service, ProductionOrderData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            ProductionOrderData productionOrderData = JsonConvert.DeserializeObject<ProductionOrderData>(component.Value);
                        
            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out ProductionOrder productionOrder))
            {
                productionOrder.Queue = productionOrderData.Queue;
            }
        }
    }

    protected override ProductionOrderData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        ProductionOrderData productionOrderData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out ProductionOrder productionOrder))
            {
                productionOrderData.Queue = productionOrder.Queue;
                string json = JsonConvert.SerializeObject(productionOrderData);
                components[entity.Id] = json;
            }
        }
        productionOrderData.Components = components;
        return productionOrderData;
    }

}
