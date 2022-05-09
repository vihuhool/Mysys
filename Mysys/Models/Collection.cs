using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mysys.Models
{
    public class Collection
    {
        public enum Themes
        {
           Displates,
           Stamps,
           Coins,
           Cards,
           Vinyl,
           Comic,
           Posters,
           Dolls,
           Toys,
           Funko,
           Figures,
           Cars,
           Tableware,
           Magnets,
           Autographs,
           Bookmarks,
           Books,
           Candys,
           Plants,
           Postcards,
           Magazines,
           Cans,
           Bottles,
           Tattoos,
           Lighters,
           Ashtrays,
           Photos,
           Clocks,
           Anime,
           Pens
        }

        public enum Fields
        {
            Text,
            Date,
            Boolean
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Themes Theme { get; set; }
        public string ImageURL { get; set; }

        public int AdditionalTextFields { get; set; }
        public int AdditionalDateTimeFields { get; set; }
        public int AdditionalBoolFields { get; set; }
       
       

        public class Add
        {
            public int Id { get; set; }
            public int CollectionID { get; set; }
            [ForeignKey("CollectionID")]
            public string Content { get; set; }
            public string Field { get; set; }

            public string FieldType { get; set; }
            public Add()
            {

            }
        }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<Add> Adds { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
