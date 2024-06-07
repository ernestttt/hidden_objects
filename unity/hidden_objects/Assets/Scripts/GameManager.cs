using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

public class GameManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _image;
    [SerializeField] private BoxCollider2D _collider;

    private Level _activeLevel;
    
    private Camera _camera;

    private void Start(){
        _camera = Camera.main;
    }

    public async void OpenLevel(Level level){
        _activeLevel = level;
        
        Texture2D texture = await _activeLevel.LoadTexture();
        if (texture)
        {
            Sprite sprite = 
                Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));

            _image.sprite = sprite;
            AdjustImageScale();
        }
    }

    private void AdjustImageScale()
    {
        Vector3 leftBottom = _camera.ScreenToWorldPoint(Vector3.zero);
        Vector3 rightTop = _camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        float screenWidth = rightTop.x - leftBottom.x;
        float screenHeight = rightTop.y - leftBottom.y;

        float spriteWidth = _image.sprite.bounds.size.x;
        float spriteHeight = _image.sprite.bounds.size.y;

        float widthCoef = screenWidth / spriteWidth;
        float heightCoef = screenHeight / spriteHeight;

        float coef = Mathf.Max(widthCoef, heightCoef);

        transform.localScale = Vector3.one * coef;
        _collider.size = _image.sprite.bounds.size * coef;
    }

    private void Update() {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // for now only one collider
            if (Physics2D.Raycast(ray.origin, ray.direction)){
                _activeLevel.AddProgress();
            }
        }
    }

    private void OnApplicationPause(bool pauseStatus) {
        if(pauseStatus){
            _activeLevel?.Save();
        }
    }

    private void OnApplicationQuit() {
        _activeLevel?.Save();
    }
}
