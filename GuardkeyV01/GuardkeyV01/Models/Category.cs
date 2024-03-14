using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GuardkeyV01.Models
{
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int NoteId { get; set; }

        [ForeignKey(nameof(NoteId))]
        [InverseProperty("Categorys")]
        public Note Record { get; set; }

    }
}
