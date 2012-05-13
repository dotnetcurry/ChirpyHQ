using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChirpyHQ.Domain.Model
{
    public class Chirp
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int InReplyToId { get; set; }
        public IList<Chirp> Replies { get; set; }
        public string [] Tags { get; set; }
        public int UserId;
        
        public Chirp()
        {
            //Tags = new List<ChirpTag>();
           // User = new ChirpyUser();
        }
    }
}
