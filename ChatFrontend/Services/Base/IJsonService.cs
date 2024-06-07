namespace ChatFrontend.Services.Base
{
    public interface IJsonService
    {
        string Serialize(object obj);
        object Deserialize(string json);
        T Deserialize<T>(string json);
    }
}
