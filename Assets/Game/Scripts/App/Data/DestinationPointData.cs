using SampleGame.Common;
using System;
using System.Collections.Generic;

[Serializable]
public struct DestinationPointData
{
    public Dictionary<int, string> Components { get; set; }
    public SerializedVector3 DestinationPoint {  get; set; }
}
