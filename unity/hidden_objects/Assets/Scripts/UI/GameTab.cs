using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTab : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _counter;

    public Action BackToMenu { get; set; }
    public Level _activeLevel;

    private void Start(){
        _backButton.onClick.AddListener(() => Back());
    }

    public async void Init(Level level){
        _activeLevel = level;
        _activeLevel.OnProgressUpdated += UpdateProgress;
        _activeLevel.OnCompleted += Back;
        UpdateProgress();
    }

    private void UpdateProgress(){
        _counter.text = $"{_activeLevel.Counter - _activeLevel.Progress}";
    }

    private void Back(){
        _activeLevel.OnProgressUpdated -= UpdateProgress;
        _activeLevel.OnCompleted -= Back;
        BackToMenu?.Invoke();
    }
}
