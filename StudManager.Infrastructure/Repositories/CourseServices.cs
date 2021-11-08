using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudManager.Core.Entities;
using StudManager.Core.Repositories;
using StudManager.Infrastructure.Data;
using StudManager.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudManager.Infrastructure.Repositories
{
    public class CourseServices : GenericService<Course>, ICourseServices
    {
        public CourseServices(DBContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Course>> All()
        {
            try
            {
                return await dbSet.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} All function error", typeof(CourseServices));
                return new List<Course>();
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Delete function error", typeof(CourseServices));
                return false;
            }
        }

        public override async Task<bool> Upsert(Course entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.CourseName = entity.CourseName;
                existingUser.CourseNo = entity.CourseNo;
                existingUser.Qualifications = entity.Qualifications;

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Repo} Upsert function error", typeof(CourseServices));
                return false;
            }
        }
    }
}
