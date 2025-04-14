using Cysharp.Threading.Tasks;
using System.Collections.Generic;

public sealed class GameSaveLoader
{
    private readonly IGameRepository _repository;
    private readonly IEnumerable<IGameSerializer> _serializers;

    public GameSaveLoader(IGameRepository repository, IEnumerable<IGameSerializer> serializers)
    {
        _repository = repository;
        _serializers = serializers;
    }

    public async UniTask<bool> Save(int version)
    {
        var gameState = new Dictionary<string, string>();
        foreach (IGameSerializer serializer in _serializers)
            serializer.Serialize(gameState);        
        return await _repository.SetState(gameState, version);        
    }

    public async UniTask<bool> Load(int version)
    {
        var (success, gameState) = await _repository.GetState(version);

        foreach (IGameSerializer serializer in _serializers)
            serializer.Deserialize(gameState);

        if (success)
        {
            return true;
        }
        return false;
    }
}
