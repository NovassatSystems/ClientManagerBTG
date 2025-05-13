namespace ClientManagerBTG.Features.Clients;

public partial class ClientModel : ObservableObject
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }

    [ObservableProperty] public bool isNew;
    [ObservableProperty] public bool isEdited;

    #region Aux
    public static implicit operator ClientEntity(ClientModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Lastname = model.Lastname,
        Age = model.Age,
        Address = model.Address
    };
    #endregion
}


public class ClientEntity
{
    [PrimaryKey]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("lastname")]
    public string Lastname { get; set; }

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("address")]
    public string Address { get; set; }

    public static implicit operator ClientModel(ClientEntity entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Lastname = entity.Lastname,
        Age = entity.Age,
        Address = entity.Address
    };
}