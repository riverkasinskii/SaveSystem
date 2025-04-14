using SampleGame.Common;
using System;
using System.Collections.Generic;

[Serializable]
public struct EntityData
{
    public Dictionary<int, string> Components { get; set; }
    public string Name { get; set; }    
    public int Id { get; set; }
    public SerializedVector3 Position { get; set; }
    public SerializedVector3 Rotation { get; set; }
}
