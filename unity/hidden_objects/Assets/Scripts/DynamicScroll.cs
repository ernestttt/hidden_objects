using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class DynamicScroll : MonoBehaviour
{
    [SerializeField] private RectTransform _prefab;
    [SerializeField] private int _numberOfItems = 10;
    [SerializeField, Range(2, 3)] private int numberOfColumns = 2;

    private ScrollRect _scrollRect;
    private float _itemSize;
    private List<RectTransform> _items = new List<RectTransform>();

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        Vector3[] corners = new Vector3[4];
        ((RectTransform)(_scrollRect.transform)).GetWorldCorners(corners);
        _itemSize = (corners[2].x - corners[1].x) / numberOfColumns;
        int numberOfRows = _numberOfItems / numberOfColumns + (_numberOfItems % numberOfColumns > 0 ? 1 : 0);
        float contentVerticalSize = numberOfRows * _itemSize;

        _scrollRect.content.sizeDelta = new Vector2(_scrollRect.content.sizeDelta.x, contentVerticalSize);

        for (int i = 0; i < _numberOfItems; i++){
            GameObject item = Instantiate(_prefab.gameObject, _scrollRect.content);
            RectTransform itemTransform = (RectTransform)item.transform;
            Vector2 pos = new Vector2(i % numberOfColumns * _itemSize, -i / numberOfColumns * _itemSize + contentVerticalSize);
            itemTransform.sizeDelta = new Vector2(_itemSize, _itemSize);
            itemTransform.anchoredPosition = pos;
            _items.Add(itemTransform);
        }
    }
}
