using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceSimulatorMain.Models;

namespace VideoServiceSimulatorMain.Services
{
    public class VideoService
    {
        private readonly List<Video> _videos;
        private readonly List<User> _users;
        private int _nextVideoId = 1;

        public VideoService()
        {
            _videos = new List<Video>();
            _users = new List<User>();
        }

        public Video CreateVideo(int creatorId, string title, string description, string url)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            if (string.IsNullOrWhiteSpace(url))
                throw new ArgumentException("URL cannot be empty");

            var creator = _users.FirstOrDefault(u => u.Id == creatorId)
                ?? throw new ArgumentException("Creator not found");

            var video = new Video
            {
                Id = _nextVideoId++,
                Title = title,
                Description = description ?? "",
                Url = url,
                CreatorId = creatorId,
                Creator = creator,
                UploadDate = DateTime.Now,
                Views = 0,
                Comments = new List<Comment>()
            };

            _videos.Add(video);
            creator.Videos.Add(video);
            return video;
        }

        public Video GetVideo(int videoId)
        {
            return _videos.FirstOrDefault(v => v.Id == videoId)
                ?? throw new ArgumentException("Video not found");
        }

        public List<Video> GetAllVideos()
        {
            return _videos.OrderByDescending(v => v.UploadDate).ToList();
        }

        public List<Video> GetVideosByCreator(int creatorId)
        {
            return _videos
                .Where(v => v.CreatorId == creatorId)
                .OrderByDescending(v => v.UploadDate)
                .ToList();
        }

        public void UpdateVideo(int videoId, string title, string description)
        {
            var video = GetVideo(videoId);

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Title cannot be empty");

            video.Title = title;
            video.Description = description ?? "";
        }

        public void DeleteVideo(int videoId)
        {
            var video = GetVideo(videoId);
            _videos.Remove(video);
            video.Creator.Videos.Remove(video);
        }

        public void IncrementViews(int videoId)
        {
            var video = GetVideo(videoId);
            video.Views++;
        }

        public List<Video> SearchVideos(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Video>();

            return _videos
                .Where(v =>
                    v.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    v.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .OrderByDescending(v => v.Views)
                .ToList();
        }

        public List<Video> GetTrendingVideos(int count = 10)
        {
            return _videos
                .OrderByDescending(v => v.Views)
                .Take(count)
                .ToList();
        }
    }
}
