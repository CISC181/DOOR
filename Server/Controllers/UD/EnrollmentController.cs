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
using Enrollment = DOOR.EF.Models.Enrollment;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : BaseController
    {
        public EnrollmentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetEnrollment")]
        public async Task<IActionResult> GetEnrollment()
        {
            List<EnrollmentDTO> lst = await _context.Enrollments
                .Select(sp => new EnrollmentDTO
                {
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetEnrollment/{_StudentId,_SectionId, _SchoolId}")]
        public async Task<IActionResult> GetEnrollment(int _StudentId, int _SectionId, int _SchoolId)
        {
            EnrollmentDTO? lst = await _context.Enrollments
                .Where(x => x.StudentId == _StudentId)
                .Where(x => x.SectionId == _SectionId)
                .Where(x => x.SchoolId == _SchoolId)
                .Select(sp => new EnrollmentDTO
                {
                    StudentId = sp.StudentId,
                    SectionId = sp.SectionId,
                    EnrollDate = sp.EnrollDate,
                    FinalGrade = sp.FinalGrade,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolId = sp.SchoolId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostEnrollment")]
        public async Task<IActionResult> PostEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                Enrollment c = await _context.Enrollments
                    .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Enrollment
                    {
                        StudentId = _EnrollmentDTO.StudentId,
                        SectionId = _EnrollmentDTO.SectionId,
                        EnrollDate = _EnrollmentDTO.EnrollDate,
                        FinalGrade = _EnrollmentDTO.FinalGrade,
                        CreatedBy = _EnrollmentDTO.CreatedBy,
                        CreatedDate = _EnrollmentDTO.CreatedDate,
                        ModifiedBy = _EnrollmentDTO.ModifiedBy,
                        ModifiedDate = _EnrollmentDTO.ModifiedDate,
                        SchoolId = _EnrollmentDTO.SchoolId
                    };
                    _context.Enrollments.Add(c);
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
        [Route("PutEnrollment")]
        public async Task<IActionResult> PutEnrollment([FromBody] EnrollmentDTO _EnrollmentDTO)
        {
            try
            {
                Enrollment ?c = await _context.Enrollments
                    .Where(x => x.StudentId == _EnrollmentDTO.StudentId)
                    .Where(x => x.SectionId == _EnrollmentDTO.SectionId)
                    .Where(x => x.SchoolId == _EnrollmentDTO.SchoolId)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.StudentId = _EnrollmentDTO.StudentId;
                    c.SectionId = _EnrollmentDTO.SectionId;
                    c.CreatedBy = _EnrollmentDTO.CreatedBy;
                    c.CreatedDate = _EnrollmentDTO.CreatedDate;
                    c.EnrollDate = _EnrollmentDTO.EnrollDate;
                    c.ModifiedBy = _EnrollmentDTO.ModifiedBy;
                    c.ModifiedDate = _EnrollmentDTO.ModifiedDate;
                    c.FinalGrade = _EnrollmentDTO.FinalGrade;
                    c.SchoolId = _EnrollmentDTO.SchoolId;
                    _context.Enrollments.Update(c);
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
        [Route("DeleteEnrollment/{_StudentId, _SectionId, _SchoolId}")]
        public async Task<IActionResult> DeleteEnrollment(int _StudentId, int _SectionId, int _SchoolId)
        {
            try
            {
                Enrollment c = await _context.Enrollments
                    .Where(x => x.StudentId == _StudentId)
                    .Where(x => x.SectionId == _SectionId)
                    .Where(x => x.SchoolId == _SchoolId)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Enrollments.Remove(c);
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