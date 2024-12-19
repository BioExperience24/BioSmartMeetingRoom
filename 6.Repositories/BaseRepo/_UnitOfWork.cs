using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace _6.Repositories.Repository;

public class DalSession : IDisposable
{
    private readonly MyDbContext _connection;
    private readonly UnitOfWork _unitOfWork;

    public DalSession(MyDbContextFactory factory)
    {
        _connection = factory.CreateDbContext();
        var cek = _connection.Database.GetDbConnection().ConnectionString;
        _unitOfWork = new UnitOfWork(_connection);
    }

    public UnitOfWork UnitOfWork => _unitOfWork;

    public void Dispose()
    {
        _unitOfWork.Dispose();
        _connection.Dispose();
    }
}

public class MyDbContextFactory(DbContextOptions<MyDbContext> options)
{
    public MyDbContext CreateDbContext()
    {
        return new MyDbContext(options);
    }
}



public interface IUnitOfWork : IDisposable
{
    MyDbContext Dbcon();
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    void Commit();
    void Rollback();
}


public class UnitOfWork(MyDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public MyDbContext Dbcon() { return context; }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _transaction = context.Database.BeginTransaction();
    }

    public void Commit()
    {
        try
        {
            _transaction?.Commit();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public void Rollback()
    {
        try
        {
            _transaction?.Rollback();
        }
        finally
        {
            DisposeTransaction();
        }
    }

    public void Dispose()
    {
        context.Dispose();
        DisposeTransaction();
    }

    private void DisposeTransaction()
    {
        _transaction?.Dispose();
        _transaction = null;
    }
}
