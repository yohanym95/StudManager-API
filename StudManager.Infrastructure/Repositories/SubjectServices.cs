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
    public class SubjectServices : GenericService<Subject>, ISubjectServices
    {

        public SubjectServices(DBContext context, ILogger logger) : base(context, logger)
        {


        }

        public override async Task<IEnumerable<Subject>> All()
        {
            try
            {
                return await dbSet.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} All function error", typeof(FeesServices));
                return new List<Subject>();
            }
        }

        public override async Task<bool> Delete(int id)
        {
            try
            {
                var exist = await dbSet.Where(f => f.Id == id).FirstOrDefaultAsync();

                if (exist == null) return false;

                dbSet.Remove(exist);
                return true;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(FeesServices));
                return false;
            }
        }

        public override async Task<Subject> GetById(int id)
        {
            try
            {
                var subject = await dbSet.Include(s => s.Course).Where(f => f.Id == id).FirstOrDefaultAsync();

                if (subject == null) return null;

                return subject;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(FeesServices));
                return new Subject();
            };
        }

        public override async Task<bool> Upsert(Subject entity)
        {
            try
            {
                var existingUser = await dbSet.Where(x => x.Id == entity.Id)
                                                    .FirstOrDefaultAsync();

                if (existingUser == null)
                    return await Add(entity);

                existingUser.CourseId = entity.CourseId;
                existingUser.Name = entity.Name;
                existingUser.SubjectDescription = entity.SubjectDescription;
                existingUser.Credit = entity.Credit;

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
