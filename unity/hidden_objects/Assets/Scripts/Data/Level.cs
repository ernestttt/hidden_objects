using Cysharp.Threading.Tasks;
using UnityEngine;

public class Level
{
    private LevelData _levelData;
    private SavedLevelProgress _savedData;

    private DataManager _dataManager;

    public Level(LevelData levelData, SavedLevelProgress progress, DataManager dataManager){
        this._levelData = levelData;
        this._savedData = progress;
        _dataManager = dataManager;
    }

    public bool IsCompleted => _savedData.Progress >= _levelData.counter;
    public string Name => _levelData.imageName;
    public int Counter => _levelData.counter;
    public int Progress => _savedData.Progress;

    public async UniTask<Texture2D> LoadTexture(){
        return await _dataManager.LoadTexture(_levelData.imageUrl);
    }
}