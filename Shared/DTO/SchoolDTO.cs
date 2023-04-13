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
    public class SchoolDTO
    {
        public int SchoolId { get; set; }
        [StringLength(30)]
        public string SchoolName { get; set; } = null!;
        [StringLength(30)]
        public string CreatedBy { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        [StringLength(30)]

        public string ModifiedBy { get; set; } = null!;
        public DateTime ModifiedDate { get; set; }
    }
}
