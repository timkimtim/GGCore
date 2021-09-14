using GGCore.Data;
using GGCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GGCore.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IGenericRepository<Post> _posts;
        private IGenericRepository<Comment> _comments;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IGenericRepository<Post> Posts => _posts ??= new GenericRepository<Post>(_context);
        public IGenericRepository<Comment> Comments => _comments ??= new GenericRepository<Comment>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
