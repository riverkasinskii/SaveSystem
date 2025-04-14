using System.Collections.Generic;

public interface IGameSerializer
{
    void Serialize(IDictionary<string, string> saveState);
    void Deserialize(IDictionary<string, string> loadState);
}
