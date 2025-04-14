using System;
using System.Collections.Generic;

[Serializable]
public struct HealthData
{
    public Dictionary<int, string> Components { get; set; }
    public int Health { get; set; }
}
