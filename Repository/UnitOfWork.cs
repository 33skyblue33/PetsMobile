using System;
using PetsMobile.Data;
using PetsMobile.Repository.Interface;

namespace PetsMobile.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IPetRepository PetRepository { get; }
        public ICommentRepository CommentRepository { get; }

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            PetRepository = new PetRepository(_context);
            CommentRepository = new CommentRepository(_context);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
