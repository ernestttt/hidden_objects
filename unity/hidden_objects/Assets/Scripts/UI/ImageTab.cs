using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ImageTab : MonoBehaviour
{
    [SerializeField] private LevelBlock _prefab;
    [SerializeField, Range(2, 3)] private int _numberOfColumns = 2;

    private ScrollRect _scrollRect;
    private float _itemSize;
    private List<LevelBlock> _items = new List<LevelBlock>();

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        CalculateImageSize();
    }

    public async void Init(List<Level> levels){
        int numberOfItems = levels.Count;
        int numberOfRows = numberOfItems / _numberOfColumns + (numberOfItems % _numberOfColumns > 0 ? 1 : 0);
        float contentVerticalSize = numberOfRows * _itemSize;

        _scrollRect.content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x, contentVerticalSize);

        for (int i = 0; i < numberOfItems; i++)
        {
            LevelBlock item = Instantiate(_prefab, _scrollRect.content);
            item.Init(levels[i]);
            RectTransform itemTransform = (RectTransform)item.transform;
            Vector2 pos = new Vector2(i % _numberOfColumns * _itemSize, -i / _numberOfColumns * _itemSize + contentVerticalSize);
            itemTransform.sizeDelta = new Vector2(_itemSize, _itemSize);
            itemTransform.anchoredPosition = pos;
            _items.Add(item);
        }
    }

    private void CalculateImageSize(){
        Vector3[] corners = new Vector3[4];
        ((RectTransform)_scrollRect.transform).GetWorldCorners(corners);
        _itemSize = (corners[2].x - corners[1].x) / _numberOfColumns;
    }
}
