using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System;

public class DataManager{

    private List<LevelData> levelDatas;
    private GameConfig _config;

    public Action LoadingError;

    private List<Level> levels;
    public List<Level> Levels => levels;

    public Action<Level> OpenLevelCallback;

    public DataManager(GameConfig config){
        _config = config;
    }

    public async UniTask Init(){
        await LoadLevels();
        InitLevels();
    }

    private void InitLevels(){
        levels = new List<Level>();
        for (int i = 0; i < levelDatas.Count; i++){
            Level level = new Level(levelDatas[i], new SavedLevelProgress(), LoadTexture, OpenLevelCallback);
            levels.Add(level);
        }
    }

    private async UniTask LoadLevels(){
        if (!IsInternetAvailable())
        {
            Debug.Log("There is no internet connection");
            LoadingError?.Invoke();
            return;
        }

        UnityWebRequest request = UnityWebRequest.Get(_config.ContentPath);
        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            LevelsResult result = JsonUtility.FromJson<LevelsResult>(request.downloadHandler.text);
            LevelData[] levelDatas = result.levels;

            this.levelDatas = new List<LevelData>();
            for (int i = 0; i < levelDatas.Length; i++)
            {
                LevelData levelData = levelDatas[i];
                this.levelDatas.Add(levelData);
            }
        }
        else
        {
            Debug.Log(request.error);
            LoadingError?.Invoke();
        }
    }

    private async UniTask CheckImagesLoading(){
        List<Texture2D> textures = new List<Texture2D>();
        foreach (LevelData data in levelDatas){
            Texture2D texture = await LoadTexture(data.imageUrl);
            textures.Add(texture);
        }
    }

    private bool IsInternetAvailable()
    {
        return Application.internetReachability != NetworkReachability.NotReachable;
    }

    public async UniTask<Texture2D> LoadTexture(string url){
        if (!IsInternetAvailable())
        {
            Debug.Log("There is no internet connection");
            return null;
        }

        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        await request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Texture2D texture =  DownloadHandlerTexture.GetContent(request);
            return texture;
        }
        else{
            Debug.Log(request.error);
            return null;
        }
    }
}