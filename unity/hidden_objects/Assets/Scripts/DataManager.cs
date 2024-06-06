using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;

public class DataManager{

    private Dictionary<int, LevelData> levels;
    private GameConfig _config;

    public DataManager(GameConfig config){
        _config = config;
    }

    public async UniTask Init(){
        await LoadLevels();
    }

    private async UniTask LoadLevels(){
        UnityWebRequest request = UnityWebRequest.Get(_config.ContentPath);
        await request.SendWebRequest();
        Debug.Log(request.downloadHandler.text);
    }
}