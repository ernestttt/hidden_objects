using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ImageTab _imageTab;
    [SerializeField] private GameTab _gameTab;

    public void InitImages(List<Level> levels){
        _imageTab.Init(levels);
    }

    public void OpenLevel(Level level){
        _gameTab.gameObject.SetActive(true);
        _gameTab.Init(level);
    }
}