using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mysys.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public int CollectionID { get; set; }
        [ForeignKey("CollectionID")]
        public virtual Collection Collection { get; set; }
    }
}
