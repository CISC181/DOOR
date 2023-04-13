using DOOR.EF.Data;
using DOOR.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text.Json;
using System.Runtime.InteropServices;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using System.Drawing;
using Microsoft.AspNetCore.Identity;
using DOOR.Server.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Data;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Numerics;
using DOOR.Shared.DTO;
using DOOR.Shared.Utils;
using DOOR.Server.Controllers.Common;
using static System.Collections.Specialized.BitVector32;
using Grade = DOOR.EF.Models.Grade;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeController : BaseController
    {
        public GradeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetGrade")]
        public async Task<IActionResult> GetGrade()
        {
            List<GradeDTO> lst = await _context.Grades
                .Select(sp => new GradeDTO
                {
                    SchoolId= sp.SchoolId,
                    StudentId = sp.StudentId,
                    CreatedBy = sp.CreatedBy,
                    SectionId = sp.SectionId,
                    CreatedDate = sp.CreatedDate,
                    GradeTypeCode= sp.GradeTypeCode,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    NumericGrade = sp.NumericGrade,
                    Comments = sp.Comments
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetGrade/{_SchoolId,_letterGrade}")]
        public async Task<IActionResult> GetGrade(int _SchoolId,int _StudentId, int _SectionId, string _GradeTypeCode, byte _GradeCodeOccurence)
        {
            GradeDTO? lst = await _context.Grades
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.StudentId == _StudentId)
                .Where(x => x.SectionId == _SectionId)
                .Where(x => x.GradeCodeOccurrence == _GradeCodeOccurence)
                .Where(x => x.GradeTypeCode == _GradeTypeCode)
                .Select(sp => new GradeDTO
                {
                    SchoolId = sp.SchoolId,
                    StudentId = sp.StudentId,
                    CreatedBy = sp.CreatedBy,
                    SectionId = sp.SectionId,
                    CreatedDate = sp.CreatedDate,
                    GradeTypeCode = sp.GradeTypeCode,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    GradeCodeOccurrence = sp.GradeCodeOccurrence,
                    NumericGrade = sp.NumericGrade,
                    Comments = sp.Comments
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostGrade")]
        public async Task<IActionResult> PostGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                Grade c = await _context.Grades
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.StudentId == _GradeDTO.StudentId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Grade
                    {
                        SchoolId = _GradeDTO.SchoolId,
                        StudentId = _GradeDTO.StudentId,
                        CreatedBy = _GradeDTO.CreatedBy,
                        SectionId = _GradeDTO.SectionId,
                        CreatedDate = _GradeDTO.CreatedDate,
                        GradeTypeCode = _GradeDTO.GradeTypeCode,
                        ModifiedBy = _GradeDTO.ModifiedBy,
                        ModifiedDate = _GradeDTO.ModifiedDate,
                        GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence,
                        NumericGrade = _GradeDTO.NumericGrade,
                        Comments = _GradeDTO.Comments
                    };
                    _context.Grades.Add(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }








        [HttpPut]
        [Route("PutGrade")]
        public async Task<IActionResult> PutGrade([FromBody] GradeDTO _GradeDTO)
        {
            try
            {
                Grade? c = await _context.Grades
                    .Where(x => x.SchoolId == _GradeDTO.SchoolId)
                    .Where(x => x.StudentId == _GradeDTO.StudentId)
                    .Where(x => x.SectionId == _GradeDTO.SectionId)
                    .Where(x => x.GradeCodeOccurrence == _GradeDTO.GradeCodeOccurrence)
                    .Where(x => x.GradeTypeCode == _GradeDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.SchoolId = _GradeDTO.SchoolId;
                    c.StudentId = _GradeDTO.StudentId;
                    c.CreatedBy = _GradeDTO.CreatedBy;
                    c.SectionId = _GradeDTO.SectionId;
                    c.CreatedDate = _GradeDTO.CreatedDate;
                    c.GradeTypeCode = _GradeDTO.GradeTypeCode;
                    c.ModifiedBy = _GradeDTO.ModifiedBy;
                    c.ModifiedDate = _GradeDTO.ModifiedDate;
                    c.GradeCodeOccurrence = _GradeDTO.GradeCodeOccurrence;
                    c.NumericGrade = _GradeDTO.NumericGrade;
                    c.Comments = _GradeDTO.Comments;
                    _context.Grades.Update(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }


        [HttpDelete]
        [Route("DeleteGrade/{_SchoolId}")]
        public async Task<IActionResult> DeleteGrade(int _SchoolId, string _GradeTypeCode, int _StudentId, int _SectionId, byte _GradeCodeOccurence, Grade x)
        {
            try
            {
                Grade c = await _context.Grades
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeCode)
                    .Where(x => x.StudentId == _StudentId)
                    .Where(x => x.SectionId == _SectionId)
                    .Where(x => x.GradeCodeOccurrence == _GradeCodeOccurence)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Grades.Remove(c);
                    await _context.SaveChangesAsync();
                }
            }

            catch (DbUpdateException Dex)
            {
                List<OraError> DBErrors = ErrorHandling.TryDecodeDbUpdateException(Dex, _OraTranslateMsgs);
                return StatusCode(StatusCodes.Status417ExpectationFailed, Newtonsoft.Json.JsonConvert.SerializeObject(DBErrors));
            }
            catch (Exception ex)
            {
                _context.Database.RollbackTransaction();
                List<OraError> errors = new List<OraError>();
                errors.Add(new OraError(1, ex.Message.ToString()));
                string ex_ser = Newtonsoft.Json.JsonConvert.SerializeObject(errors);
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex_ser);
            }

            return Ok();
        }



    }
}