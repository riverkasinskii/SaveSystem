using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

public sealed class GameClient
{
    private readonly string _uri;

    public GameClient(string uri)
    {
        _uri = uri;
    }

    public async UniTask<bool> Save(string json, int version)
    {
        UnityWebRequest request = UnityWebRequest.Put($"{_uri}/save?version={version}", json);

        try
        {
            await request.SendWebRequest();
        }
        catch (UnityWebRequestException)
        {
            return false;
        }
        return request.result == UnityWebRequest.Result.Success;
    }

    public async UniTask<(bool, string)> Load(int version)
    {
        UnityWebRequest request = UnityWebRequest.Get($"{_uri}/load?version={version}");

        try
        {
            await request.SendWebRequest();
        }
        catch (UnityWebRequestException)
        {
            return (false, null);
        }
        
        string json = request.downloadHandler.text;        
        return json == null || json == string.Empty ? (false, null) : (true, json);
    }
}
