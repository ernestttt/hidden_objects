using System;
using System.Collections.Generic;

[Serializable]
public class LevelData{
    public int id;
    public string imageUrl;
    public string imageName;
    public int counter;

    // public int Id => id;
    // public string ImageUrl => imageUrl;
    // public string Name => name;
    // public int Counter => counter;

    // public LevelData(int id, string imageUrl, string name, int counter){
    //     this.id = id;
    //     this.imageUrl = imageUrl;
    //     this.name = name;
    //     this.counter = counter;
    // }
}

[Serializable]
public class LevelsResult{
    public LevelData[] levels;
}