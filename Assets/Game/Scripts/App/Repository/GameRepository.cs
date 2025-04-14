using Cysharp.Threading.Tasks;
using Modules.Ecryption;
using Newtonsoft.Json;
using System.Collections.Generic;

public sealed class GameRepository : IGameRepository
{    
    private readonly GameClient _client;      
    private readonly string _aesPassword;
    private readonly byte[] _aesSalt;

    public GameRepository(GameClient client, string aesPassword, byte[] aesSalt)
    {
        _client = client;        
        _aesPassword = aesPassword;
        _aesSalt = aesSalt;
    }

    public async UniTask<bool> SetState(Dictionary<string, string> gameState, int version)
    {        
        string json = JsonConvert.SerializeObject(gameState);        
        string newJson = AesEncryptor.Encrypt(json, _aesPassword, _aesSalt);   
        
        return await _client.Save(newJson, version);  
    }

    public async UniTask<(bool, Dictionary<string, string>)> GetState(int version)
    {        
        Dictionary<string, string> remoteState;

        var (success, remoteJson) = await _client.Load(version);
        if (success)
        {
            string json = AesEncryptor.Decrypt(remoteJson, _aesPassword, _aesSalt);            
            remoteState = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            remoteState ??= new Dictionary<string, string>();
            return (success, remoteState);
        }
        else
        {
            remoteState = new Dictionary<string, string>();
            return (success, remoteState);
        }                
    }
}
