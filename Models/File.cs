﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlopBox.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Folder { get; set; }
    }
}