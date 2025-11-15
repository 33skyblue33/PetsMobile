using Microsoft.AspNetCore.Mvc;
using PetsMobile.Entities;

namespace PetsMobile.Repository.Interface
{
    public interface IUnitOfWork
    {
        IPetRepository PetRepository { get; }
        ICommentRepository CommentRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
