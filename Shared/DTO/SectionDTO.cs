using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DOOR.Shared.DTO
{
    public class SectionDTO
    {
        public int SectionId { get; set; }
        public int CourseNo { get; set; }
        public int SectionNo { get; set; }
        public DateTime StartDateTime { get; set; }
        public int InstructorId { get; set; }
        public int Capacity { get; set; }
        [StringLength(50)]
        public string Location { get; set; } = null!;

        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }

        [StringLength(30)]
        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
        public int SchoolId { get; set; }
    }
}
