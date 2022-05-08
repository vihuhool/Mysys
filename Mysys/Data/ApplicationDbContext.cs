using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mysys.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static Mysys.Models.Collection;

namespace Mysys.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Item> Items { get; set; }

        public DbSet<Add> Adds { get; set; }
        public DbSet<TextField> TextFields { get; set; }
        public DbSet<DateTimeField> DateTimeFields { get; set; }
        public DbSet<BoolField> BoolFields { get; set; }
    }
}
