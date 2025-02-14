using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoServiceSimulatorMain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Video> Videos { get; set; } = new List<Video>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Subscription> Subscribers { get; set; } = new List<Subscription>(); 
        public List<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    }
}
