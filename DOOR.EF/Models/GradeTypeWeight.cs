using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Table("GRADE_TYPE_WEIGHT")]
    public partial class GradeTypeWeight
    {
        public GradeTypeWeight()
        {
            Grades = new HashSet<Grade>();
        }

        [Key]
        [Column("SCHOOL_ID")]
        [Precision(8)]
        public int SchoolId { get; set; }
        [Key]
        [Column("SECTION_ID")]
        [Precision(8)]
        public int SectionId { get; set; }
        [Key]
        [Column("GRADE_TYPE_CODE")]
        [StringLength(2)]
        [Unicode(false)]
        public string GradeTypeCode { get; set; } = null!;
        [Column("NUMBER_PER_SECTION")]
        [Precision(3)]
        public byte NumberPerSection { get; set; }
        [Column("PERCENT_OF_FINAL_GRADE")]
        [Precision(3)]
        public byte PercentOfFinalGrade { get; set; }
        [Column("DROP_LOWEST")]
        [Precision(1)]
        public bool DropLowest { get; set; }
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

        [ForeignKey("SchoolId,GradeTypeCode")]
        [InverseProperty("GradeTypeWeights")]
        public virtual GradeType GradeType { get; set; } = null!;
        [ForeignKey("SectionId,SchoolId")]
        [InverseProperty("GradeTypeWeights")]
        public virtual Section S { get; set; } = null!;
        [ForeignKey("SchoolId")]
        [InverseProperty("GradeTypeWeights")]
        public virtual School School { get; set; } = null!;
        [InverseProperty("GradeTypeWeight")]
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
