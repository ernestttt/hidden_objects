using UnityEngine;

public class GameManager : MonoBehaviour 
{
    [SerializeField] private GameConfig _config;
    [SerializeField] private UIManager uIManager;

    private async void Start(){
        DataManager dataManager= new DataManager(_config);
        dataManager.OpenLevelCallback = uIManager.OpenLevel;
        await dataManager.Init();
        uIManager.InitImages(dataManager.Levels);
    }
}