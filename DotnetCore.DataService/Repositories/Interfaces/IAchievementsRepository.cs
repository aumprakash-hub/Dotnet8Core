using DotnetCore.Entities.DbSet;

namespace DotnetCore.DataService.Repositories.Interfaces;

public interface IAchievementsRepository: IGenericRepository<Achievement>
{
    Task<Achievement?> GetDriverAchievementAsync(Guid driverId);
}