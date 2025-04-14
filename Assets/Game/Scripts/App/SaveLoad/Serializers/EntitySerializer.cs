using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Common;
using System.Collections.Generic;
using UnityEngine;

public sealed class EntitySerializer : GameSerializer<EntityWorld, EntityData>
{
    protected override void Deserialize(EntityWorld service, EntityData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            EntityData entityData = JsonConvert.DeserializeObject<EntityData>(component.Value);

            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out Transform transform))
            {
                transform.SetPositionAndRotation(entityData.Position, entityData.Rotation);
            }
            else
            {
                service.Spawn(entityData.Name, entityData.Position, entityData.Rotation, entityData.Id);
            }
        }
    }

    protected override EntityData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        EntityData entityData = new();
        foreach (var entity in entities)
        {
            entityData.Id = entity.Id;
            entityData.Name = entity.Name;            

            if (entity.TryGetComponent(out Transform transform))
            {
                entityData.Position = new SerializedVector3(transform.position);
                entityData.Rotation = new SerializedVector3((SerializedVector3)transform.rotation);                
            }

            string json = JsonConvert.SerializeObject(entityData);
            components[entity.Id] = json;
        }
        entityData.Components = components;
        return entityData;
    }
}
