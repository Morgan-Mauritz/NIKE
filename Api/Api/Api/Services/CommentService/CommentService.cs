using Api.Exceptions;
using Api.Model;
using Api.Repository;
using Api.Repository.CommentRepository;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Api.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IRepository<User> userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<CommentWithUserDTO> PostComment(AddCommentDTO comment, string apiKey)
        {
            var user = await _userRepository.GetByApiKey(apiKey);
            if (user == null || user.Id != comment.UserID)
            {
                throw new UnauthorizedAccessException("Du har ej tillgång till denna åtgärd");
            }

            var mappedComment = _mapper.Map<Comment>(comment); 
            await _commentRepository.PostComment(mappedComment);
            return _mapper.Map<CommentWithUserDTO>(mappedComment); 
        }

        public async Task<CommentDTO> DeleteComment(long id, string apiKey)
        {
            var user = await _userRepository.GetByApiKey(apiKey);
            var comment = await _commentRepository.GetComment(id);
            if (comment == null)
            {
                throw new NotFoundException("Kommentaren kunde ej hittas");
            }
            if (user == null || user.Id != comment.UserId)
            {
                throw new UnauthorizedAccessException("Du har ej tillgång till denna åtgärd");
            }

            comment = await _commentRepository.DeleteComment(comment);

            return _mapper.Map<CommentDTO>(comment);

        }

        public async Task<CommentDTO> UpdateComment(EditComment editComment, string apiKey)
        {
            var user = await _userRepository.GetByApiKey(apiKey);
            var comment = await _commentRepository.GetComment(editComment.Id);

            if (comment == null)
            {
                throw new NotFoundException("Kommentaren kunde ej hittas");
            }
            if (user == null || user.Id != comment.UserId)
            {
                throw new UnauthorizedAccessException("Du har ej tillgång till denna åtgärd");
            }
            comment.Comment1 = editComment.Text;
            await _commentRepository.UpdateComments();

            return _mapper.Map<CommentDTO>(comment);

        }

        public Task<CommentDTO> GetComment(long Id, string apiKey)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<CommentDTO>> ListComments(BaseFilter filter, string apiKey)
        {
            throw new System.NotImplementedException();
        }
    }
}