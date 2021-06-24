using System;

namespace EfSqlForeignKeyBug
{
    public class UserEntity
    {
        public Guid Id { get; set; }

        public Guid AccessCodeId { get; set; }

        public string Name { get; set; }

        public virtual AccessCodeEntity AccessCode { get; set; }
    }
}
