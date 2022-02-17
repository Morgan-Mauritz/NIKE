using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Api.Model
{
    public partial class Comment
    {
        [Required]
        public long Id { get; set; }
        [Required]
        public long? EntryId { get; set; }
        [Required]
        public long? UserId { get; set; }
        [Required]
        public string Comment1 { get; set; }

        public virtual Entry Entry { get; set; }

        public virtual User User { get; set; }

    }

    public class CommentDTO
    {
        public long Id { get; set; }
        public EntryDto Entry { get; set; }
        public string Text { get; set; }
    }
    public class EditComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    public class CommentWithUserDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Text { get; set; }
    }

    public class AddCommentDTO
    {
        public int EntryID { get; set; }
        public int UserID { get; set; }
        public string Text { get; set; }
    }
}
