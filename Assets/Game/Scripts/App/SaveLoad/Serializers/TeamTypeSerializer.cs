using Modules.Entities;
using Newtonsoft.Json;
using SampleGame.Gameplay;
using System.Collections.Generic;

public sealed class TeamTypeSerializer : GameSerializer<EntityWorld, TeamTypeData>
{
    protected override void Deserialize(EntityWorld service, TeamTypeData data)
    {
        var components = data.Components;
        foreach (var component in components)
        {
            TeamTypeData teamTypeData = JsonConvert.DeserializeObject<TeamTypeData>(component.Value);

            if (service.TryGet(component.Key, out Entity entity) && entity.TryGetComponent(out Team team))
            {
                team.Type = teamTypeData.Team;
            }
        }
    }

    protected override TeamTypeData Serialize(EntityWorld service)
    {
        var entities = service.GetAll();
        var components = new Dictionary<int, string>();
        TeamTypeData teamTypeData = new();
        foreach (var entity in entities)
        {
            if (entity.TryGetComponent(out Team team))
            {
                teamTypeData.Team = team.Type;
                string json = JsonConvert.SerializeObject(teamTypeData);
                components[entity.Id] = json;
            }
        }
        teamTypeData.Components = components;
        return teamTypeData;
    }
}
