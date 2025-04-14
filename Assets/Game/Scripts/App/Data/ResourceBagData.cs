using SampleGame.Common;
using System;
using System.Collections.Generic;

[Serializable]
public struct ResourceBagData
{
    public Dictionary<int, string> Components { get; set; }
    public int CurrentResourceBag { get; set; }
    public ResourceType Type { get; set; }
}
