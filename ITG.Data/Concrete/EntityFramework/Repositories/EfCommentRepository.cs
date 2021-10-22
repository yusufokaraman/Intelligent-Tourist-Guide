﻿using ITG.Data.Abstract;
using ITG.Entities.Concrete;
using ITG.Shared.Data.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace ITG.Data.Concrete.EntityFramework.Repositories
{
    public class EfCommentRepository : EfEntityRepositoryBase<Comment>, ICommentRepository
    {
        public EfCommentRepository(DbContext context) : base(context)
        {
        }
    }
}
