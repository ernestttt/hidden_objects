using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "Custom/GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField] private string contentListPath;

    public string ContentPath => contentListPath;
}
