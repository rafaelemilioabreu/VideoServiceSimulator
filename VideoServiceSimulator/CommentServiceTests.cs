using System.Reflection;
using VideoServiceSimulatorMain.Models;
using VideoServiceSimulatorMain.Services;

namespace VideoServiceSimulatorTest
{
    public class CommentServiceTests
    {
        private readonly CommentService _commentService;
        private readonly User _testUser;
        private readonly Video _testVideo;

        public CommentServiceTests()
        {
            _commentService = new CommentService();

            _testUser = new User { Id = 1, Username = "testuser" };
            _testVideo = new Video { Id = 1, Title = "Test Video" };

            var usersField = typeof(CommentService).GetField("_users", BindingFlags.NonPublic | BindingFlags.Instance);
            var videosField = typeof(CommentService).GetField("_videos", BindingFlags.NonPublic | BindingFlags.Instance);

            var users = (List<User>)usersField.GetValue(_commentService);
            var videos = (List<Video>)videosField.GetValue(_commentService);

            users.Add(_testUser);
            videos.Add(_testVideo);
        }

        [Fact]
        public void AddComment_ValidData_ShouldAddComment()
        {
            var comment = _commentService.AddComment(1, 1, "Test comment");

            Assert.NotNull(comment);
            Assert.Equal("Test comment", comment.Content);
            Assert.Equal(1, comment.UserId);
            Assert.Equal(1, comment.VideoId);
        }

        [Fact]
        public void AddComment_EmptyContent_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                _commentService.AddComment(1, 1, ""));
        }

        [Fact]
        public void GetVideoComments_ShouldReturnCommentsOrderedByDate()
        {
            _commentService.AddComment(1, 1, "First comment");
            Thread.Sleep(10); // Ensure different timestamps
            _commentService.AddComment(1, 1, "Second comment");

            var comments = _commentService.GetVideoComments(1);

            Assert.Equal(2, comments.Count);
            Assert.Equal("Second comment", comments[0].Content);
            Assert.Equal("First comment", comments[1].Content);
        }

        [Fact]
        public void UpdateComment_ValidData_ShouldUpdateContent()
        {
            var comment = _commentService.AddComment(1, 1, "Original content");

            _commentService.UpdateComment(comment.Id, "Updated content");

            var updatedComment = _commentService.GetComment(comment.Id);
            Assert.Equal("Updated content", updatedComment.Content);
        }

        [Fact]
        public void DeleteComment_ExistingComment_ShouldRemoveComment()
        {
            var comment = _commentService.AddComment(1, 1, "Test comment");

            _commentService.DeleteComment(comment.Id);

            Assert.Throws<ArgumentException>(() =>
                _commentService.GetComment(comment.Id));
        }

        [Fact]
        public void GetUserComments_ShouldReturnUserComments()
        {
            _commentService.AddComment(1, 1, "Comment 1");
            _commentService.AddComment(1, 1, "Comment 2");

            var comments = _commentService.GetUserComments(1);

            Assert.Equal(2, comments.Count);
            Assert.All(comments, c => Assert.Equal(1, c.UserId));
        }

        [Fact]
        public void GetCommentCount_ShouldReturnCorrectCount()
        {
            _commentService.AddComment(1, 1, "Comment 1");
            _commentService.AddComment(1, 1, "Comment 2");
            _commentService.AddComment(1, 1, "Comment 3");

            var count = _commentService.GetCommentCount(1);

            Assert.Equal(3, count);
        }
    }
}