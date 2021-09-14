using GGCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Post> Posts { get; }
        IGenericRepository<Comment> Comments { get; }

        Task Save();
    }
}
