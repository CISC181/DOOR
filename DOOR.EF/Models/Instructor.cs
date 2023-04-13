using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("INSTRUCTOR")]
    public partial class Instructor
    {
        public Instructor()
        {
            Sections = new HashSet<Section>();
        }

        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }
        [Key]
        [Column("INSTRUCTOR_ID")]
        [Precision(8)]
        public int InstructorId { get; set; }
        [Column("SALUTATION")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Salutation { get; set; }
        [Column("FIRST_NAME")]
        [StringLength(25)]
        [Unicode(false)]
        public string FirstName { get; set; } = null!;
        [Column("LAST_NAME")]
        [StringLength(25)]
        [Unicode(false)]
        public string LastName { get; set; } = null!;
        [Column("STREET_ADDRESS")]
        [StringLength(50)]
        [Unicode(false)]
        public string StreetAddress { get; set; } = null!;
        [Column("ZIP")]
        [StringLength(5)]
        [Unicode(false)]
        public string Zip { get; set; } = null!;
        [Column("PHONE")]
        [StringLength(15)]
        [Unicode(false)]
        public string? Phone { get; set; }
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

        [ForeignKey("SchoolId")]
        [InverseProperty("Instructors")]
        public virtual School School { get; set; } = null!;
        [ForeignKey("Zip")]
        [InverseProperty("Instructors")]
        public virtual Zipcode ZipNavigation { get; set; } = null!;
        [InverseProperty("Instructor")]
        public virtual ICollection<Section> Sections { get; set; }
    }
}
