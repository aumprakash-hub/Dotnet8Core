using DotnetCore.DataService.Data;
using DotnetCore.DataService.Repositories.Interfaces;
using DotnetCore.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DotnetCore.DataService.Repositories;

public class DriverRepository(AppDbContext context, ILogger logger) : GenericRepository<Driver>(context, logger), IDriverRepository
{
    public override async Task<IEnumerable<Driver>> GetAll()
    {
        try
        {
            return await _dbSet.Where(x => x.Status == 1).AsNoTracking().AsSplitQuery()
                .OrderBy(x => x.AddedDate).ToListAsync();
        }
        catch (Exception exception)
        {
            logger.LogError(exception: exception, "Error in getAll driver repository", typeof(DriverRepository));
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
            logger.LogError(exception: exception, "Error in Delete driver repository", typeof(DriverRepository));
            throw;
        }
    }

    public override async Task<bool> Update(Driver entity)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(x => x.Id == entity.Id);
            if (result is null)
            {
                return false;
            }

            result.DriverNumber = entity.DriverNumber;
            result.UpdatedDate = DateTime.UtcNow;
            result.FirstName = entity.FirstName;
            result.LastName = entity.LastName;
            result.DateOfBirth = entity.DateOfBirth;
            return true;
        }
        catch (Exception exception)
        {
            logger.LogError(exception: exception, "Error in Update driver repository", typeof(DriverRepository));
            throw;
        }
    }
}