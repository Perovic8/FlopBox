using FlopBox.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FlopBox.Context
{
    public class FlopBoxdbContext : DbContext
    {
        public FlopBoxdbContext() : base("Flopboxdb")
        {
        }

        public DbSet<File> Files { get; set; }
        public DbSet<Folder> Folders { get; set; }

    }
}