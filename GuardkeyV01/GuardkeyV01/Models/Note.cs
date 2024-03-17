using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GuardkeyV01.Models
{
    [SQLite.Table("Note")]
    public class Note
    {

        [PrimaryKey, AutoIncrement]
        public int NoteId { get; set; }

        public string ResourceName { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Description { get; set; }

        [Ignore] // Ignore this property for database mapping
        public ICollection<Category> Categories { get; set; }

    }
}
