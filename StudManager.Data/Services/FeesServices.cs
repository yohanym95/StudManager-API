using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using StudManager.Data.Context;
using StudManager.Data.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace StudManager.Data.Services
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

            }catch(Exception e)
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

            }catch(Exception e)
            {
                _logger.LogError(e, "{Repo} Delete function error", typeof(FeesServices));
                return false;
            }
        }

    }
}
