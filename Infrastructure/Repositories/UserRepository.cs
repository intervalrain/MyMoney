using System.Data;
using Applications.Repositories;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Entities;
using Dapper;
using Infrastructure.Common;

namespace Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserDbContext userDbContext)
            : base(userDbContext)
        {
        }

        public List<Domain.User> GetAllUsers()
        {
            var users = GetAll();
            return users.Select(x => new Domain.User(x.UserName)).ToList();
        }

        public Domain.User FindUserById(string userName)
        {
            var user = Find(new { UserName = userName });
            return new Domain.User(user.UserName);
        }

        public void Update(Domain.User user)
        {
            throw new NotImplementedException();
        }

        public void Create(Domain.User user)
        {
            var count = GetSerialNumber(GetAll());
            Insert(new User
            {
                UserId = count,
                UserName = user.UserName
            });
        }

        public void Delete(Domain.User user)
        {
            var id = Find(user.UserName).UserId;
            base.Delete(id);
        }

        private int GetSerialNumber(IEnumerable<User> users)
        {
            int curr = 1;
            var hashSet = new HashSet<int>();
            foreach (var user in users)
            {
                hashSet.Add(user.UserId);
                while (hashSet.Contains(curr))
                {
                    curr++;
                }
            }
            return curr;
        }
    }
}

