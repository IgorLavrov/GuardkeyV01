﻿using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GuardkeyV01.Models
{
    [SQLite.Table("Category")]
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CategoryName { get; set; }

      
    }
   
}
