using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameTab : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private TextMeshProUGUI _counter;
    [SerializeField] private RawImage _image;

    private float _width;

    private void Start(){
        //_backButton.onClick.AddListener(() => BackToMenu());
        Vector3[] corners = new Vector3[4];
        ((RectTransform)transform).GetWorldCorners(corners);
        _width = corners[2].x - corners[1].x;
    }

    public async void Init(Level level){
        Texture2D texture = await level.LoadTexture();
        _image.texture = texture;

        float heightRatio = texture.height / texture.width;
        _image.rectTransform.sizeDelta = new Vector2(_width, _width * heightRatio);

        _counter.text = $"{level.Progress} / {level.Counter}";
    }
}
