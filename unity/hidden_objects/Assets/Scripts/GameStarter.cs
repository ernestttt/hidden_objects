using UnityEngine;

public class GameStarter : MonoBehaviour 
{
    [SerializeField] private GameConfig _config;
    [SerializeField] private UIManager uIManager;

    private async void Start(){
        DataManager dataManager= new DataManager(_config);
        await dataManager.Init();
        uIManager.InitImages(dataManager.Levels);
    }
}