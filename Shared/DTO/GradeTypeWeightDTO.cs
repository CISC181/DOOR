using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DOOR.EF.Models;

namespace DOOR.Shared.DTO
{
    public class GradeTypeWeightDTO
    {
        [Precision(8)]
        public int SchoolId { get; set; }//PF
        [Precision(8)]
        public int SectionId { get; set; }//PF
        [StringLength(2)]
        public string GradeTypeCode { get; set; } = null!;//PF
        [Precision(3)]
        public byte NumberPerSection { get; set; }
        [Precision(3)]
        public byte PercentOfFinalGrade { get; set; }
        [Precision(1)]
        public bool DropLowest { get; set; }
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }

    }
}

