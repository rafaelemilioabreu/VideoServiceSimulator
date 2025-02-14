using System.Reflection;
using VideoServiceSimulatorMain.Models;
using VideoServiceSimulatorMain.Services;

namespace VideoServiceSimulatorTest
{
    public class VideoServiceTests
    {

        private readonly VideoService _videoService;
        private readonly User _testCreator;

        public VideoServiceTests()
        {
            _videoService = new VideoService();

            _testCreator = new User { Id = 1, Username = "testcreator", Videos = new List<Video>() };

            var usersField = typeof(VideoService).GetField("_users", BindingFlags.NonPublic | BindingFlags.Instance);
            var users = (List<User>)usersField.GetValue(_videoService);
            users.Add(_testCreator);
        }

        [Fact]
        public void CreateVideo_ValidData_ShouldCreateVideo()
        {
            var video = _videoService.CreateVideo(1, "Test Video", "Description", "http://test.url");

            Assert.NotNull(video);
            Assert.Equal("Test Video", video.Title);
            Assert.Equal("Description", video.Description);
            Assert.Equal("http://test.url", video.Url);
            Assert.Equal(1, video.CreatorId);
            Assert.Equal(0, video.Views);
        }

        [Theory]
        [InlineData("", "Description", "http://test.url")]
        [InlineData("Title", "Description", "")]
        [InlineData(null, "Description", "http://test.url")]
        public void CreateVideo_InvalidData_ShouldThrowException(string title, string description, string url)
        {
            Assert.Throws<ArgumentException>(() =>
                _videoService.CreateVideo(1, title, description, url));
        }

        [Fact]
        public void CreateVideo_NonExistingCreator_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                _videoService.CreateVideo(999, "Title", "Description", "http://test.url"));
        }

        [Fact]
        public void GetVideo_ExistingVideo_ShouldReturnVideo()
        {
            var createdVideo = _videoService.CreateVideo(1, "Test Video", "Description", "http://test.url");

            var video = _videoService.GetVideo(createdVideo.Id);

            Assert.NotNull(video);
            Assert.Equal(createdVideo.Id, video.Id);
        }

        [Fact]
        public void GetVideo_NonExistingVideo_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                _videoService.GetVideo(999));
        }

        [Fact]
        public void UpdateVideo_ValidData_ShouldUpdateVideo()
        {
            var video = _videoService.CreateVideo(1, "Original Title", "Original Description", "http://test.url");

            _videoService.UpdateVideo(video.Id, "Updated Title", "Updated Description");

            var updatedVideo = _videoService.GetVideo(video.Id);
            Assert.Equal("Updated Title", updatedVideo.Title);
            Assert.Equal("Updated Description", updatedVideo.Description);
        }

        [Fact]
        public void DeleteVideo_ExistingVideo_ShouldRemoveVideo()
        {
            var video = _videoService.CreateVideo(1, "Test Video", "Description", "http://test.url");

            _videoService.DeleteVideo(video.Id);

            Assert.Throws<ArgumentException>(() =>
                _videoService.GetVideo(video.Id));
            Assert.DoesNotContain(video, _testCreator.Videos);
        }

        [Fact]
        public void IncrementViews_ShouldIncreaseViewCount()
        {
            var video = _videoService.CreateVideo(1, "Test Video", "Description", "http://test.url");

            _videoService.IncrementViews(video.Id);

            Assert.Equal(1, video.Views);
        }

        [Fact]
        public void SearchVideos_ShouldReturnMatchingVideos()
        {
            _videoService.CreateVideo(1, "First Test Video", "Description", "http://test1.url");
            _videoService.CreateVideo(1, "Second Video Test", "Test Description", "http://test2.url");
            _videoService.CreateVideo(1, "Another Video", "Not matching", "http://test3.url");

            var results = _videoService.SearchVideos("test");

            Assert.Equal(2, results.Count);
        }

        [Fact]
        public void GetTrendingVideos_ShouldReturnMostViewedVideos()
        {
            var video1 = _videoService.CreateVideo(1, "Video 1", "Description", "http://test1.url");
            var video2 = _videoService.CreateVideo(1, "Video 2", "Description", "http://test2.url");
            var video3 = _videoService.CreateVideo(1, "Video 3", "Description", "http://test3.url");

            for (int i = 0; i < 5; i++) _videoService.IncrementViews(video1.Id);
            for (int i = 0; i < 3; i++) _videoService.IncrementViews(video2.Id);
            _videoService.IncrementViews(video3.Id);

            var trendingVideos = _videoService.GetTrendingVideos(2);

            Assert.Equal(2, trendingVideos.Count);
            Assert.Equal(video1.Id, trendingVideos[0].Id); // Most views
            Assert.Equal(video2.Id, trendingVideos[1].Id); // Second most views
        }
    }
}