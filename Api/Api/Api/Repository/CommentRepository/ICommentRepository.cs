using Api.Model;
using System.Threading.Tasks;

namespace Api.Repository.CommentRepository
{
    public interface ICommentRepository
    {
        Task<Comment> DeleteComment(Comment comment);
        Task<Comment> GetComment(long id);
        Task<Comment> GetComments(BaseFilter filter, long userID);
        Task UpdateComments();
    }
}
