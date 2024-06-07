using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelBlock : MonoBehaviour, IPointerDownHandler
{
    public enum LevelBlockState{
        NotLoaded = 0,
        Loading = 1,
        Loaded = 2,
        Completed = 3,
    }

    [SerializeField] private Image _completedIcon;
    [SerializeField] private TextMeshProUGUI _progressCounter;
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private GameObject _loadingIcon;
    [SerializeField] private GameObject _notLoadedIcon;
    [SerializeField] private Image _clickableImage;
    [SerializeField] private RawImage _image;

    private Level _level;

    private void Reset(){
        _completedIcon.gameObject.SetActive(false);
        _progressCounter.gameObject.SetActive(false);
        _levelName.gameObject.SetActive(false);
        _loadingIcon.gameObject.SetActive(false);
        _notLoadedIcon.gameObject.SetActive(false);
        _clickableImage.gameObject.SetActive(false);
        _image.gameObject.SetActive(false);
    }

    private void SetNotLoadedState(){
        _notLoadedIcon.gameObject.SetActive(true);
    }

    private void SetLoadingState(){
        _loadingIcon.gameObject.SetActive(true);
    }

    private void SetLoadedState(){
        _clickableImage.gameObject.SetActive(true);
        _levelName.gameObject.SetActive(true);
        _levelName.text = _level.Name;
        _progressCounter.gameObject.SetActive(true);
        _progressCounter.text = $"{_level.Progress} / {_level.Counter}";
        _image.gameObject.SetActive(true);
    }

    private void SetCompletedState(){
        _completedIcon.gameObject.SetActive(true);
        _levelName.gameObject.SetActive(true);
        _levelName.text = _level.Name;
    }

    private void SetState(LevelBlockState state){
        Reset();
        switch(state){
            case LevelBlockState.NotLoaded:
                SetNotLoadedState();
                break;
            case LevelBlockState.Loading:
                SetLoadingState();
                break;
            case LevelBlockState.Loaded:
                SetLoadedState();
                break;
            case LevelBlockState.Completed:
                SetCompletedState();
                break;
        }
    }

    public async UniTask Init(Level level){
        _level = level;
        SetState(LevelBlockState.Loading);
        Texture2D texture = await _level.LoadTexture();
        if(texture == null){
            SetState(LevelBlockState.NotLoaded);
        }
        
        _image.texture = texture;

        if(_level.IsCompleted){
            SetState(LevelBlockState.Completed);
        }

        SetState(LevelBlockState.Loaded);
    }

    public void OnPointerDown(PointerEventData eventData){
        Debug.Log("level was clicked");
    }
}
