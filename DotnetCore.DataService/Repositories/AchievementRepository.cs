using DotnetCore.DataService.Data;
using DotnetCore.DataService.Repositories.Interfaces;
using DotnetCore.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotnetCore.DataService.Repositories;

public class AchievementRepository(AppDbContext context, ILogger logger) : GenericRepository<Achievement>(context, logger), IAchievementsRepository
{
    public async Task<Achievement?> GetDriverAchievementAsync(Guid driverId)
    {
        try
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.DriverId == driverId);
        }
        catch (Exception exception)
        {
            logger.LogError(exception: exception, "Error in GetDriverAchievementAsync achievement repository", typeof(AchievementRepository));
            throw;
        }
    }
    
    public override async Task<IEnumerable<Achievement>> GetAll()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1).AsNoTracking().AsSplitQuery()
                .OrderBy(x => x.AddedDate).ToListAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception: exception, "Error in getAll driver repository", typeof(AchievementRepository));
            throw;
        }
    }

    public override async Task<bool> Delete(Guid id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (result is null)
            {
                return false;
            }

            result.Status = 0;
            result.UpdatedDate = DateTime.UtcNow;
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception: exception, "Error in Delete driver repository", typeof(AchievementRepository));
            throw;
        }
    }

    public override async Task<bool> Update(Achievement entity)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result is null)
            {
                return false;
            }

            result.FastestLap = entity.FastestLap;
            result.UpdatedDate = DateTime.UtcNow;
            result.PolePosition = entity.PolePosition;
            result.RaceWins = entity.RaceWins;
            result.WorldChampionship = entity.WorldChampionship;
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception: exception, "Error in Update driver repository", typeof(AchievementRepository));
            throw;
        }
    }
}