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
using Instructor = DOOR.EF.Models.Instructor;
using static Duende.IdentityServer.Models.IdentityResources;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : BaseController
    {
        public InstructorController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetInstructor")]
        public async Task<IActionResult> GetInstructor()
        {
            List<InstructorDTO> lst = await _context.Instructors
                .Select(sp => new InstructorDTO
                {
                    SchoolId = sp.SchoolId,
                    InstructorId = sp.InstructorId,
                    Salutation = sp.Salutation,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip,
                    Phone = sp.Phone,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetInstructor/{_SchoolId, _InstructorId, _Zip}")]
        public async Task<IActionResult> GetInstructor(int _SchoolId, int _InstructorId, string _Zip)
        {
            InstructorDTO? lst = await _context.Instructors
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.InstructorId == _InstructorId)
                .Where(x => x.Zip == _Zip)
                .Select(sp => new InstructorDTO
                {
                    SchoolId = sp.SchoolId,
                    InstructorId = sp.InstructorId,
                    Salutation = sp.Salutation,
                    FirstName = sp.FirstName,
                    LastName = sp.LastName,
                    StreetAddress = sp.StreetAddress,
                    Zip = sp.Zip,
                    Phone = sp.Phone,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostInstructor")]
        public async Task<IActionResult> PostInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                Instructor c = await _context.Instructors
                    .Where(x => x.SchoolId == _InstructorDTO.SchoolId)
                    .Where(x => x.InstructorId == _InstructorDTO.InstructorId)
                    .Where(x => x.Zip == _InstructorDTO.Zip)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Instructor
                    {
                        SchoolId = _InstructorDTO.SchoolId,
                        InstructorId = _InstructorDTO.InstructorId,
                        Salutation = _InstructorDTO.Salutation,
                        FirstName = _InstructorDTO.FirstName,
                        LastName = _InstructorDTO.LastName,
                        StreetAddress = _InstructorDTO.StreetAddress,
                        Zip = _InstructorDTO.Zip,
                        Phone = _InstructorDTO.Phone,
                        CreatedBy = _InstructorDTO.CreatedBy,
                        CreatedDate = _InstructorDTO.CreatedDate,
                        ModifiedBy = _InstructorDTO.ModifiedBy,
                        ModifiedDate = _InstructorDTO.ModifiedDate
                    };
                    _context.Instructors.Add(c);
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
        [Route("PutInstructor")]
        public async Task<IActionResult> PutInstructor([FromBody] InstructorDTO _InstructorDTO)
        {
            try
            {
                Instructor? c = await _context.Instructors
                    .Where(x => x.SchoolId == _InstructorDTO.SchoolId)
                    .Where(x => x.Zip == _InstructorDTO.Zip)
                    .Where(x => x.InstructorId == _InstructorDTO.InstructorId)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.SchoolId = _InstructorDTO.SchoolId;
                    c.InstructorId = _InstructorDTO.InstructorId;
                    c.Salutation = _InstructorDTO.Salutation;
                    c.FirstName = _InstructorDTO.FirstName;
                    c.LastName = _InstructorDTO.LastName;
                    c.StreetAddress = _InstructorDTO.StreetAddress;
                    c.Zip = _InstructorDTO.Zip;
                    c.Phone = _InstructorDTO.Phone;
                    c.CreatedBy = _InstructorDTO.CreatedBy;
                    c.CreatedDate = _InstructorDTO.CreatedDate;
                    c.ModifiedBy = _InstructorDTO.ModifiedBy;
                    c.ModifiedDate = _InstructorDTO.ModifiedDate;
                    _context.Instructors.Update(c);
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
        [Route("DeleteInstructor/{_SchoolId}")]
        public async Task<IActionResult> DeleteInstructor(int _SchoolId, string _Zip, int _InstructorId, Instructor x)
        {
            try
            {
                Instructor c = await _context.Instructors
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.Zip == _Zip)
                    .Where(x => x.InstructorId == _InstructorId)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Instructors.Remove(c);
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