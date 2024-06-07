using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataManager{

    private List<LevelData> levelDatas;
    private GameConfig _config;

    public Action LoadingError;

    private List<Level> levels;
    public List<Level> Levels => levels;

    public Action<Level> OpenLevelCallback;
    private BinaryFormatter binaryFormatter = new BinaryFormatter();

    public DataManager(GameConfig config){
        _config = config;

        if (!Directory.Exists(_config.SavePath))
        {
            Directory.CreateDirectory(_config.SavePath);
        }
    }

    public async UniTask Init(){
        await LoadLevels();
        InitLevels();
    }

    private void InitLevels(){
        levels = new List<Level>();
        for (int i = 0; i < levelDatas.Count; i++){
            if(!TryToLoadSaveData(levelDatas[i].id, out SavedLevelProgress progress)){
                progress = new SavedLevelProgress(levelDatas[i].id);
            }
            Level level = new Level(levelDatas[i], progress, LoadTexture, OpenLevelCallback, Save);
            levels.Add(level);
        }
    }

    private bool TryToLoadSaveData(int id, out SavedLevelProgress progress){
        string path = $"{_config.SavePath}/{id}.dat";
        if (File.Exists(path)){
            using (FileStream stream = new FileStream(path, FileMode.Open)){
                progress = binaryFormatter.Deserialize(stream) as SavedLevelProgress;
                return true;
            }
        }

        progress = null;
        return false;
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

    private void Save(SavedLevelProgress progress){
        using (FileStream stream = new FileStream($"{_config.SavePath}/{progress.Id}.dat", FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, progress);
        }
    }
}