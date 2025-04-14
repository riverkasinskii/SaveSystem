using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public interface IGameRepository
{
    UniTask<bool> SetState(Dictionary<string, string> gameState, int version);
    UniTask<(bool, Dictionary<string, string>)> GetState(int version);
}
