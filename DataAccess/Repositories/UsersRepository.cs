using DataAccess.Context;
using DataAccess.Entities;
using DataAccess.Interfaces;

namespace DataAccess.Repositories
{
    public class UsersRepository : Repository<Users>, IUsersRepository
    {
        public UsersRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }
    }
}
