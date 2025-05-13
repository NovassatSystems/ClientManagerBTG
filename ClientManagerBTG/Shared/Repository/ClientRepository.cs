namespace ClientManagerBTG.Shared.Repository;

public interface IClientRepository
{
    Task<List<ClientEntity>> GetAllAsync();
    Task SaveAllAsync(List<ClientEntity> clients);
    Task<bool> IsEmptyAsync();
    Task AddAsync(ClientModel model);
    Task UpdateAsync(ClientModel model);
    Task DeleteAsync(Guid id);
    Task<ClientEntity?> GetByIdAsync(Guid id);
}
public class ClientRepository : IClientRepository
{
    private readonly SQLiteAsyncConnection _db;

    public ClientRepository()
    {
        Debug.WriteLine(FileSystem.AppDataDirectory);

        var path = Path.Combine(FileSystem.AppDataDirectory, "clients.db");
        _db = new SQLiteAsyncConnection(path);
        _db.CreateTableAsync<ClientEntity>().Wait();
    }

    public Task<List<ClientEntity>> GetAllAsync() => _db.Table<ClientEntity>().ToListAsync();

    public async Task<ClientEntity?> GetByIdAsync(Guid id)
    {
        return await _db.Table<ClientEntity>()
            .Where(c => c.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task SaveAllAsync(List<ClientEntity> clients)
    {
        await _db.RunInTransactionAsync(tran =>
        {
            foreach (var client in clients)
                tran.InsertOrReplace(client);
        });
    }

    public async Task AddAsync(ClientModel model)
    {
        var entity = (ClientEntity)model;
        await _db.InsertAsync(entity);
    }
    public async Task UpdateAsync(ClientModel model)
    {
        var entity = (ClientEntity)model;
        await _db.UpdateAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _db.DeleteAsync<ClientEntity>(id);
    }

    public async Task<bool> IsEmptyAsync()
    {
        var count = await _db.Table<ClientEntity>().CountAsync();
        return count == 0;
    }
}

