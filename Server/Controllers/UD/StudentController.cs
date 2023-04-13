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
using Student = DOOR.EF.Models.Student;
using static Duende.IdentityServer.Models.IdentityResources;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : BaseController
    {
        public StudentController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            List<StudentDTO> lst = await _context.Students
                .Select(sp => new StudentDTO
                {
                    StudentId = sp.StudentId,
                    Salutation = sp.Salutation,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip,
                    Phone = sp.Phone,
                    Employer = sp.Employer,
                    RegistrationDate = sp.RegistrationDate,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetStudent/{_StudentId, _SchoolId}")]
        public async Task<IActionResult> GetStudent(int _StudentId, int _SchoolId)
        {
            StudentDTO? lst = await _context.Students
                .Where(x => x.StudentId == _StudentId)
                .Where(x => x.SchoolId == _SchoolId)
                .Select(sp => new StudentDTO
                {
                    StudentId = sp.StudentId,
                    Salutation = sp.Salutation,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip,
                    Phone = sp.Phone,
                    Employer = sp.Employer,
                    RegistrationDate = sp.RegistrationDate,
                    SchoolId = sp.SchoolId,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostStudent")]
        public async Task<IActionResult> PostStudent([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                Student c = await _context.Students
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .Where(x => x.SchoolId == _StudentDTO.SchoolId)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Student
                    {
                        StudentId = _StudentDTO.StudentId,
                        Salutation = _StudentDTO.Salutation,
                        FirstName = _StudentDTO.FirstName,
                        LastName = _StudentDTO.LastName,
                        StreetAddress = _StudentDTO.StreetAddress,
                        Zip = _StudentDTO.Zip,
                        Phone = _StudentDTO.Phone,
                        Employer = _StudentDTO.Employer,
                        RegistrationDate = _StudentDTO.RegistrationDate,
                        SchoolId = _StudentDTO.SchoolId,
                        CreatedBy = _StudentDTO.CreatedBy,
                        CreatedDate = _StudentDTO.CreatedDate,
                        ModifiedBy = _StudentDTO.ModifiedBy,
                        ModifiedDate = _StudentDTO.ModifiedDate
                    };
                    _context.Students.Add(c);
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
        [Route("PutStudent")]
        public async Task<IActionResult> PutStudent([FromBody] StudentDTO _StudentDTO)
        {
            try
            {
                Student? c = await _context.Students
                    .Where(x => x.StudentId == _StudentDTO.StudentId)
                    .Where(x => x.SchoolId == _StudentDTO.SchoolId)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.StudentId = _StudentDTO.StudentId;
                    c.Salutation = _StudentDTO.Salutation;
                    c.FirstName = _StudentDTO.FirstName;
                    c.LastName = _StudentDTO.LastName;
                    c.StreetAddress = _StudentDTO.StreetAddress;
                    c.Zip = _StudentDTO.Zip;
                    c.Phone = _StudentDTO.Phone;
                    c.Employer = _StudentDTO.Employer;
                    c.RegistrationDate = _StudentDTO.RegistrationDate;
                    c.SchoolId = _StudentDTO.SchoolId;
                    c.CreatedBy = _StudentDTO.CreatedBy;
                    c.CreatedDate = _StudentDTO.CreatedDate;
                    c.ModifiedBy = _StudentDTO.ModifiedBy;
                    c.ModifiedDate = _StudentDTO.ModifiedDate;
                    _context.Students.Update(c);
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
        [Route("DeleteStudent/{_SchoolId}")]
        public async Task<IActionResult> DeleteStudent(int _StudentId, int _SchoolId, Student x)
        {
            try
            {
                Student c = await _context.Students
                    .Where(x => x.StudentId == _StudentId)
                    .Where(x => x.SchoolId == _SchoolId)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Students.Remove(c);
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