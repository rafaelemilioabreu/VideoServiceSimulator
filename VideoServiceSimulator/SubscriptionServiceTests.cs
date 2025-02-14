using VideoServiceSimulatorMain.Services;

namespace VideoServiceSimulatorTest
{
    public class SubscriptionServiceTests
    {
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionServiceTests()
        {
            _subscriptionService = new SubscriptionService();
        }

        [Fact]
        public void Subscribe_ShouldCreateNewSubscription()
        {
            _subscriptionService.Subscribe(1, 2);

            Assert.True(_subscriptionService.IsSubscribed(1, 2));
        }

        [Fact]
        public void Subscribe_ToSelf_ShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() =>
                _subscriptionService.Subscribe(1, 1));
        }

        [Fact]
        public void Subscribe_WhenAlreadySubscribed_ShouldThrowException()
        {
            _subscriptionService.Subscribe(1, 2);

            Assert.Throws<ArgumentException>(() =>
                _subscriptionService.Subscribe(1, 2));
        }

        [Fact]
        public void Unsubscribe_ShouldRemoveSubscription()
        {
            _subscriptionService.Subscribe(1, 2);

            _subscriptionService.Unsubscribe(1, 2);

            Assert.False(_subscriptionService.IsSubscribed(1, 2));
        }

        [Fact]
        public void GetSubscriberCount_ShouldReturnCorrectCount()
        {
            _subscriptionService.Subscribe(1, 3);
            _subscriptionService.Subscribe(2, 3);
            _subscriptionService.Subscribe(4, 3);

            var count = _subscriptionService.GetSubscriberCount(3);

            Assert.Equal(3, count);
        }
    }
}