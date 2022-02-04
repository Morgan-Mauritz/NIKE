using Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.CommentService
{
    public interface ICommentService
    {
        Task<CommentDTO> DeleteComment(long Id, string apiKey);
        Task<CommentDTO> GetComment(long Id, string apiKey);
        Task<List<CommentDTO>> ListComments(BaseFilter filter, string apiKey);

    }
}
