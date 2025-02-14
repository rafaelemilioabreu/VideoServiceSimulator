using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoServiceSimulatorMain.Models
{
    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public DateTime UploadDate { get; set; }
        public int Views { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}
