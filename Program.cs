using System;
using System.Linq;

namespace EfSqlForeignKeyBug
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Migrating");
            using (var context = new ReproContext())
            {
                context.Migrate();
            }

            Console.WriteLine("Inserting User (works)");
            using (var context = new ReproContext())
            {
                var accessCode = context.AccessCodes.Single(x => x.Code == "123");
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    AccessCodeId = accessCode.Id,
                    Name = "Test User",
                };

                context.Users.Add(user);
                context.SaveChanges();
            }

            Console.WriteLine("Inserting User (broke)");
            using (var context = new ReproContext())
            {
                var accessCode = context.AccessCodes.Single(x => x.Code == "456");
                var user = new UserEntity
                {
                    Id = Guid.NewGuid(),
                    AccessCodeId = accessCode.Id,
                    Name = "Test User 2",
                };

                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
