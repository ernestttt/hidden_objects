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
        LevelsResult result = JsonUtility.FromJson<LevelsResult>(request.downloadHandler.text);
        LevelData[] levelDatas = result.levels;

        levels = new Dictionary<int, LevelData>();
        for (int i = 0; i < levelDatas.Length; i++)
        {
            LevelData levelData = levelDatas[i];
            levels.Add(levelData.id, levelData);
        }
    }

    private async UniTask CheckImagesLoading(){
        List<Texture2D> textures = new List<Texture2D>();
        foreach (LevelData data in levels.Values){
            Texture2D texture = await LoadTexture(data.imageUrl);
            textures.Add(texture);
        }
    }

    public async UniTask<Texture2D> LoadTexture(string url){
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        await request.SendWebRequest();
        Texture2D texture =  DownloadHandlerTexture.GetContent(request);
        return texture;
    }
}