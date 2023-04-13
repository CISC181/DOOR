using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("SECTION")]
    [Index("CourseNo", Name = "SECT_CRSE_FK_I")]
    [Index("InstructorId", Name = "SECT_INST_FK_I")]
    [Index("SectionId", Name = "SECT_PK", IsUnique = true)]
    [Index("SectionNo", "CourseNo", Name = "SECT_SECT2_UK", IsUnique = true)]
    public partial class Section
    {
        public Section()
        {
            Enrollments = new HashSet<Enrollment>();
            GradeTypeWeights = new HashSet<GradeTypeWeight>();
        }

        [Key]
        [Column("SECTION_ID")]
        [Precision(8)]
        public int SectionId { get; set; }
        [Column("COURSE_NO")]
        [Precision(8)]
        public int CourseNo { get; set; }
        [Column("SECTION_NO")]
        [Precision(3)]
        public byte SectionNo { get; set; }
        [Column("START_DATE_TIME", TypeName = "DATE")]
        public DateTime? StartDateTime { get; set; }
        [Column("LOCATION")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Location { get; set; }
        [Column("INSTRUCTOR_ID")]
        [Precision(8)]
        public int InstructorId { get; set; }
        [Column("CAPACITY")]
        [Precision(3)]
        public byte? Capacity { get; set; }
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
        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }

        [ForeignKey("CourseNo,SchoolId")]
        [InverseProperty("Sections")]
        public virtual Course Course { get; set; } = null!;
        [ForeignKey("SchoolId,InstructorId")]
        [InverseProperty("Sections")]
        public virtual Instructor Instructor { get; set; } = null!;
        [ForeignKey("SchoolId")]
        [InverseProperty("Sections")]
        public virtual School School { get; set; } = null!;
        [InverseProperty("S")]
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        [InverseProperty("S")]
        public virtual ICollection<GradeTypeWeight> GradeTypeWeights { get; set; }
    }
}
