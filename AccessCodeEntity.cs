using System;
using System.Collections.Generic;

namespace EfSqlForeignKeyBug {
    public class AccessCodeEntity {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Email { get; set; }

        public Guid? ParentAccessCodeId { get; set; }

        public virtual AccessCodeEntity ParentAccessCode { get; set; }

        public virtual ICollection<AccessCodeEntity> ChildAccessCodes { get; set; }
        public virtual ICollection<UserEntity> Users { get; set; }
    }
}

