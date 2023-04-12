using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("ZIPCODE")]
    public partial class Zipcode
    {
        public Zipcode()
        {
            Instructors = new HashSet<Instructor>();
        }

        [Key]
        [Column("ZIP")]
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;
        [Column("CITY")]
        [StringLength(25)]
        [Unicode(false)]
        public string? City { get; set; }
        [Column("STATE")]
        [StringLength(2)]
        [Unicode(false)]
        public string? State { get; set; }
        [Column("CREATED_BY")]
        [StringLength(30)]
        [Unicode(false)]
        public string CreatedBy { get; set; } = null!;
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime CreatedDate { get; set; }
        [Column("MODIFIED_BY")]
        [StringLength(30)]
        [Unicode(false)]
        public string ModifiedBy { get; set; } = null!;
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime ModifiedDate { get; set; }

        [InverseProperty("ZipNavigation")]
        public virtual ICollection<Instructor> Instructors { get; set; }
    }
}
