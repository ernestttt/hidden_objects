using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField] private ImageTab _imageTab;

    public void InitImages(List<Level> levels){
        _imageTab.Init(levels);
    }
}