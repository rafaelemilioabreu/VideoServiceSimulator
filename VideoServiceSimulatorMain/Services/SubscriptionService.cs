using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoServiceSimulatorMain.Models;

namespace VideoServiceSimulatorMain.Services
{
    public class SubscriptionService
    {
        private readonly List<Subscription> _subscriptions;
        private readonly List<User> _users;

        public SubscriptionService()
        {
            _subscriptions = new List<Subscription>();
            _users = new List<User>();
        }

        public void Subscribe(int subscriberId, int creatorId)
        {
            if (subscriberId == creatorId)
                throw new ArgumentException("Users cannot subscribe to themselves");

            if (IsSubscribed(subscriberId, creatorId))
                throw new ArgumentException("User is already subscribed to this channel");

            var subscription = new Subscription
            {
                Id = _subscriptions.Count + 1,
                SubscriberId = subscriberId,
                CreatorId = creatorId,
                SubscribedDate = DateTime.Now,
                NotificationsEnabled = true
            };

            _subscriptions.Add(subscription);
        }

        public void Unsubscribe(int subscriberId, int creatorId)
        {
            var subscription = _subscriptions.FirstOrDefault(
                s => s.SubscriberId == subscriberId && s.CreatorId == creatorId);

            if (subscription != null)
                _subscriptions.Remove(subscription);
        }

        public List<User> GetSubscribers(int creatorId)
        {
            return _subscriptions
                .Where(s => s.CreatorId == creatorId)
                .Select(s => _users.FirstOrDefault(u => u.Id == s.SubscriberId))
                .Where(u => u != null)
                .ToList();
        }

        public List<User> GetSubscriptions(int subscriberId)
        {
            return _subscriptions
                .Where(s => s.SubscriberId == subscriberId)
                .Select(s => _users.FirstOrDefault(u => u.Id == s.CreatorId))
                .Where(u => u != null)
                .ToList();
        }

        public bool IsSubscribed(int subscriberId, int creatorId)
        {
            return _subscriptions.Any(
                s => s.SubscriberId == subscriberId && s.CreatorId == creatorId);
        }

        public int GetSubscriberCount(int creatorId)
        {
            return _subscriptions.Count(s => s.CreatorId == creatorId);
        }
    }
}
