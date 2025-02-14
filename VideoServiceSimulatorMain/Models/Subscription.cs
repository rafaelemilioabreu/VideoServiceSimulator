using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoServiceSimulatorMain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public int SubscriberId { get; set; }  
        public User Subscriber { get; set; }
        public int CreatorId { get; set; }       
        public User Creator { get; set; }
        public DateTime SubscribedDate { get; set; }
        public bool NotificationsEnabled { get; set; }
    }
}
