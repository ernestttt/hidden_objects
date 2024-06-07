using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level
{
    private LevelData _levelData;
    private SavedLevelProgress _savedData;

    private Func<string, UniTask<Texture2D>> loadTexture;
    private Action<Level> openLevel;

    public event Action OnProgressUpdated;
    public event Action OnCompleted;
    public event Action<SavedLevelProgress> saveLevel;

    public Level(LevelData levelData, SavedLevelProgress progress, 
                Func<string, UniTask<Texture2D>> loadTexture, Action<Level> openLevel, Action<SavedLevelProgress> save){
        this._levelData = levelData;
        this._savedData = progress;
        this.loadTexture = loadTexture;
        this.openLevel = openLevel;
        this.saveLevel = save;
    }

    public bool IsCompleted => _savedData.Progress >= _levelData.counter;
    public string Name => _levelData.imageName;
    public int Counter => _levelData.counter;
    public int Progress => _savedData.Progress;

    public async UniTask<Texture2D> LoadTexture(){
        if(loadTexture != null)
            return await loadTexture(_levelData.imageUrl);

        return null;
    }

    public void Open(){
        openLevel?.Invoke(this);
    }

    public void AddProgress(){
        _savedData.AddProgress();
        OnProgressUpdated?.Invoke();
        if(IsCompleted){
            OnCompleted?.Invoke();
        }
    }

    public void Save(){
        saveLevel?.Invoke(_savedData);
    }
}