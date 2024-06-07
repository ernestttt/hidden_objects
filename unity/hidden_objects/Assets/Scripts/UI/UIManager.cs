using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ImageTab _imageTab;
    [SerializeField] private GameTab _gameTab;
    [SerializeField] private GameObject _unableToLoadError;

    private void Start(){
        _gameTab.BackToMenu += OpenMenu;
    }

    public void InitImages(List<Level> levels){
        _imageTab.Init(levels);
    }

    public void OpenLevel(Level level){
        Reset();
        _gameTab.gameObject.SetActive(true);
        _gameTab.Init(level);
    }

    public void OpenMenu(){
        Reset();
        _imageTab.gameObject.SetActive(true);
    }

    public void ShowUnableToLoadError()
    {
        _unableToLoadError.SetActive(true);
    }

    private void Reset(){
        _imageTab.gameObject.SetActive(false);
        _gameTab.gameObject.SetActive(false);
        _unableToLoadError.SetActive(false);
    }
}