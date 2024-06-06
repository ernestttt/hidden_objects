public class LevelData{
    public int Id { get; private set;}
    public string ImageUrl { get; private set;}
    public string Name {get; private set;}
    public int Counter {get; private set;}

    public LevelData(int id, string imageUrl, string name, int counter){
        Id = id;
        ImageUrl = imageUrl;
        Name = name;
        Counter = counter;
    }
}