using SampleGame.Common;
using System.Collections.Generic;

public struct TargetObjectData
{
    public Dictionary<int, string> Components { get; set; }
    public string Name { get; set; }    
    public int TargetId { get; set; }
    public SerializedVector3 Position { get; set; }
    public SerializedVector3 Rotation { get; set; }
}
