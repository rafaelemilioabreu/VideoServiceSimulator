using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceSimulatorMain.Models;

namespace VideoServiceSimulatorMain.Services
{
    public class CommentService
    {
        private readonly List<Comment> _comments;
        private readonly List<User> _users;
        private readonly List<Video> _videos;
        private int _nextCommentId = 1;

        public CommentService()
        {
            _comments = new List<Comment>();
            _users = new List<User>();
            _videos = new List<Video>();
        }

        public Comment AddComment(int userId, int videoId, string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Comment content cannot be empty");

            var user = _users.FirstOrDefault(u => u.Id == userId)
                ?? throw new ArgumentException("User not found");

            var video = _videos.FirstOrDefault(v => v.Id == videoId)
                ?? throw new ArgumentException("Video not found");

            var comment = new Comment
            {
                Id = _nextCommentId++,
                Content = content,
                CreatedAt = DateTime.Now,
                UserId = userId,
                User = user,
                VideoId = videoId,
                Video = video
            };

            _comments.Add(comment);
            return comment;
        }

        public void DeleteComment(int commentId)
        {
            var comment = _comments.FirstOrDefault(c => c.Id == commentId)
                ?? throw new ArgumentException("Comment not found");

            _comments.Remove(comment);
        }

        public Comment GetComment(int commentId)
        {
            return _comments.FirstOrDefault(c => c.Id == commentId)
                ?? throw new ArgumentException("Comment not found");
        }

        public List<Comment> GetVideoComments(int videoId)
        {
            return _comments
                .Where(c => c.VideoId == videoId)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }

        public List<Comment> GetUserComments(int userId)
        {
            return _comments
                .Where(c => c.UserId == userId)
                .OrderByDescending(c => c.CreatedAt)
                .ToList();
        }

        public void UpdateComment(int commentId, string newContent)
        {
            if (string.IsNullOrWhiteSpace(newContent))
                throw new ArgumentException("Comment content cannot be empty");

            var comment = _comments.FirstOrDefault(c => c.Id == commentId)
                ?? throw new ArgumentException("Comment not found");

            comment.Content = newContent;
        }

        public int GetCommentCount(int videoId)
        {
            return _comments.Count(c => c.VideoId == videoId);
        }
    }
}
