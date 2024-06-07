using System;

namespace Data
{
    [Serializable]
    public class SavedLevelProgress
    {
        private int _id;
        private int _progress;

        public int Id => _id;
        public int Progress => _progress;

        public SavedLevelProgress(int id)
        {
            _id = id;
            _progress = 0;
        }

        public void AddProgress()
        {
            _progress++;
        }
    }
}