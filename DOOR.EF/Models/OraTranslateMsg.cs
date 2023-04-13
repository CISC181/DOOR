using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DOOR.EF.Models
{
    [Keyless]
    [Table("ORA_TRANSLATE_MSG")]
    public partial class OraTranslateMsg
    {
        [Column("ORA_TRANSLATE_MSG_ID")]
        [StringLength(38)]
        [Unicode(false)]
        public string? OraTranslateMsgId { get; set; }
        [Column("ORA_CONSTRAINT_NAME")]
        [StringLength(128)]
        [Unicode(false)]
        public string? OraConstraintName { get; set; }
        [Column("ORA_ERROR_MESSAGE")]
        [StringLength(200)]
        [Unicode(false)]
        public string? OraErrorMessage { get; set; }
        [Column("CREATED_BY")]
        [StringLength(38)]
        [Unicode(false)]
        public string? CreatedBy { get; set; }
        [Column("CREATED_DATE", TypeName = "DATE")]
        public DateTime? CreatedDate { get; set; }
        [Column("MODIFIED_BY")]
        [StringLength(38)]
        [Unicode(false)]
        public string? ModifiedBy { get; set; }
        [Column("MODIFIED_DATE", TypeName = "DATE")]
        public DateTime? ModifiedDate { get; set; }
    }
}
