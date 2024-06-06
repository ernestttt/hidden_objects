using UnityEngine;

public class GameStarter : MonoBehaviour 
{
    [SerializeField] private GameConfig _config;

    private async void Start(){
        DataManager dataManager= new DataManager(_config);
        await dataManager.Init();
    }
}