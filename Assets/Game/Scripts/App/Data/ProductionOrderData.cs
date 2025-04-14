using Modules.Entities;
using System;
using System.Collections.Generic;

[Serializable]
public struct ProductionOrderData
{
    public Dictionary<int, string> Components { get; set; }
    public IReadOnlyList<EntityConfig> Queue { get; set; }
}
