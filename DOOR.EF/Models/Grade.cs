using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("GRADE")]
    public partial class Grade
    {
        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }
        [Key]
        [Column("STUDENT_ID")]
        [Precision(8)]
        public int StudentId { get; set; }
        [Key]
        [Column("SECTION_ID")]
        [Precision(8)]
        public int SectionId { get; set; }
        [Key]
        [Column("GRADE_TYPE_CODE")]
        [StringLength(2)]
        [Unicode(false)]
        public string GradeTypeCode { get; set; } = null!;
        [Key]
        [Column("GRADE_CODE_OCCURRENCE")]
        [Precision(3)]
        public byte GradeCodeOccurrence { get; set; }
        [Column("NUMERIC_GRADE", TypeName = "NUMBER(5,2)")]
        public decimal NumericGrade { get; set; }
        [Column("COMMENTS", TypeName = "CLOB")]
        public string? Comments { get; set; }
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

        [ForeignKey("SchoolId,SectionId,GradeTypeCode")]
        [InverseProperty("Grades")]
        public virtual GradeTypeWeight GradeTypeWeight { get; set; } = null!;
        [ForeignKey("SectionId,StudentId,SchoolId")]
        [InverseProperty("Grades")]
        public virtual Enrollment S { get; set; } = null!;
        [ForeignKey("SchoolId")]
        [InverseProperty("Grades")]
        public virtual School School { get; set; } = null!;
    }
}
