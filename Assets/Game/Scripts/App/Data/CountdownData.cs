using System;
using System.Collections.Generic;

[Serializable]
public struct CountdownData
{
    public Dictionary<int, string> Components { get; set; }
    public float Current { get; set; }    
}
