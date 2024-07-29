using DotnetCore.DataService.Data;
using DotnetCore.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

namespace DotnetCore.DataService.Repositories;

public class UnitOfWork: IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    public IDriverRepository Drivers { get; }
    public IAchievementsRepository Achievements { get; }

    public UnitOfWork(AppDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        var logger = loggerFactory.CreateLogger("logs");

        Drivers = new DriverRepository(context: context, logger: logger);
        Achievements = new AchievementRepository(context: context, logger: logger);
    }
    
    public async Task<bool> CompleteAsync()
    {
        var result =  await _context.SaveChangesAsync();
        return result > 0;
    }

    public void Dispose()
    {
        _context.DisposeAsync();
    }
}