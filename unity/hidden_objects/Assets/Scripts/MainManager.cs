using UnityEngine;

public class MainManager : MonoBehaviour 
{
    [SerializeField] private GameConfig _config;
    [SerializeField] private UIManager uIManager;
    [SerializeField] private GameManager gameManager;

    private async void Start(){
        DataManager dataManager= new DataManager(_config);
        dataManager.OpenLevelCallback += uIManager.OpenLevel;
        dataManager.OpenLevelCallback += gameManager.OpenLevel;
        await dataManager.Init();
        uIManager.OpenMenu();
        uIManager.InitImages(dataManager.Levels);
        dataManager.LoadingError += uIManager.ShowUnableToLoadError;
    }
}