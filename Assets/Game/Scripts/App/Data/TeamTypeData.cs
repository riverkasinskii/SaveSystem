using SampleGame.Common;
using System;
using System.Collections.Generic;

[Serializable]
public struct TeamTypeData
{
    public Dictionary<int, string> Components { get; set; }
    public TeamType Team {  get; set; }
}
