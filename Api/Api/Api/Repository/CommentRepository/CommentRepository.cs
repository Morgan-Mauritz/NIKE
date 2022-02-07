using Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Repository.CommentRepository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly NIKEContext _context;

        public CommentRepository(NIKEContext context)
        {
            _context = context;
        }

        public async Task<Comment> DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment> GetComment(long id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        } 
        public async Task<Comment> UpdateComment()
        {
             await _context.SaveChangesAsync();
        }

        public Task<Comment> GetComments(BaseFilter filter, long userID)
        {
            throw new System.NotImplementedException();
        }
    }
}