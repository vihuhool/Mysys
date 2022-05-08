using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mysys.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string ImageURL { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TextField> TextFields { get; set; }
        public virtual ICollection<DateTimeField> DateTimeFields { get; set; }
        public virtual ICollection<BoolField> BoolFields { get; set; }
        

        public int CollectionID { get; set; }
        [ForeignKey("CollectionID")]
        public virtual Collection Collection { get; set; }
        
        public virtual ICollection<Tag> Tags { get; set; }
    }
    public class TextField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }

        public int ItemId { get; set; }
        public TextField()
        {
            
        }
    }

    public class DateTimeField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Content { get; set; }
        public int ItemId { get; set; }
        public DateTimeField()
        {
            
        }
    }

    public class BoolField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Content { get; set; }
        public int ItemId { get; set; }
        public BoolField()
        {
            
        }
    }
  
}
