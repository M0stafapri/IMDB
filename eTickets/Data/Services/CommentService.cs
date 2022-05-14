using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class CommentService : EntityBaseRepository<Comment>, ICommentService
    {
        private readonly AppDbContext _context;
        public CommentService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewCommentAsync(NewCommentVM data)
        {
            var newComment = new Comment()
            {
                Id = data.Id,
                Body = data.Body,
                MovieId = data.MovieId
                //PaperId = "2"

            };
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();




        }
    }
}
