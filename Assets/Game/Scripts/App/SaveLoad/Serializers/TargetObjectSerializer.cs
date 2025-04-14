using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Common;
using SampleGame.Gameplay;
using System.Collections.Generic;
using UnityEngine;

public sealed class TargetObjectSerializer : GameSerializer<EntityWorld, TargetObjectData>
{
    protected override void Deserialize(EntityWorld service, TargetObjectData data)
    {        
        var components = data.Components;        
        foreach (var component in components)
        {
            TargetObjectData targetObjectData = JsonConvert.DeserializeObject<TargetObjectData>(component.Value);
            
            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out TargetObject targetObject))
            {
                if (targetObjectData.TargetId == -1)
                {
                    targetObject.Value = null;
                    continue;
                }     
                
                if (targetObject.Value == null && !service.Has(targetObjectData.TargetId))
                {
                    Entity newEntity = service.Spawn(targetObjectData.Name, targetObjectData.Position, targetObjectData.Rotation, targetObjectData.TargetId);
                    targetObject.Value = newEntity;
                }
                else
                {
                    service.TryGet(targetObjectData.TargetId, out Entity newEntity);
                    targetObject.Value = newEntity;
                }

                if (targetObject.Value.Id != targetObjectData.TargetId)
                {
                    service.TryGet(targetObjectData.TargetId, out Entity targetEntity);
                    targetObject.Value = targetEntity;
                }
            }            
        }                        
    }

    protected override TargetObjectData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        TargetObjectData targetObjectData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out TargetObject targetObject))
            {
                if (targetObject.Value != null)
                {
                    targetObjectData.Name = targetObject.Value.Name;
                    targetObjectData.TargetId = targetObject.Value.Id;
                    targetObject.Value.TryGetComponent(out Transform transform);
                    targetObjectData.Position = new SerializedVector3(transform.position);
                    targetObjectData.Rotation = new SerializedVector3((SerializedVector3)transform.rotation);                    
                }
                else
                {
                    targetObjectData.TargetId = -1;
                }                    
            }            

            string json = JsonConvert.SerializeObject(targetObjectData);
            components[entity.Id] = json;
        }
        targetObjectData.Components = components;
        return targetObjectData;        
    }
}
