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
    public class FeesServices : GenericService<Fees>, IFeesServices
    {
        public FeesServices(DBContext context, ILogger logger) : base(context, logger)
        {

        }

        public override async Task<IEnumerable<Fees>> All()
        {
            try
            {
                return await dbSet.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} All function error", typeof(FeesServices));
                return new List<Fees>();
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

        public override async Task<Fees> GetById(int id)
        {
            try
            {
                var fees = await dbSet.Where(f => f.Id == id).FirstOrDefaultAsync();

                if (fees == null) return null;




                return fees;

            }
            catch (Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(FeesServices));
                return new Fees();
            }
        }


    }
}
