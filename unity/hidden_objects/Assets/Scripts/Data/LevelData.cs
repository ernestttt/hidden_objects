using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class LevelData
    {
        public int id;
        public string imageUrl;
        public string imageName;
        public int counter;
    }

    [Serializable]
    public class LevelsResult
    {
        public LevelData[] levels;
    }
}