using System;

[Serializable]
public class SavedLevelProgress
{
    private int _id;
    private int _progress;
    
    public int Id => _id;
    public int Progress => _progress;

    public void AddProgress(){
        _progress++;
    }
}