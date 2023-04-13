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
using Section = DOOR.EF.Models.Section;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class SectionController : BaseController
    {
        public SectionController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetSection")]
        public async Task<IActionResult> GetSection()
        {
            List<SectionDTO> lst = await _context.Sections
                .Select(sp => new SectionDTO
                {
                    SectionId = sp.SectionId,
                    SectionNo = sp.SectionNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    InstructorId = sp.InstructorId,
                    SchoolId = sp.SchoolId,
                    Capacity = (int)sp.Capacity,
                    CourseNo = sp.CourseNo,
                    StartDateTime = (DateTime)sp.StartDateTime
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetSection/{_SectionId}")]
        public async Task<IActionResult> GetSection(int _SectionId)
        {
            SectionDTO? lst = await _context.Sections
                .Where(x => x.SectionId == _SectionId)
                .Select(sp => new SectionDTO
                {
                    SectionId = sp.SectionId,
                    SectionNo = sp.SectionNo,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Location = sp.Location,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    InstructorId = sp.InstructorId,
                    SchoolId = sp.SchoolId,
                    Capacity = (int)sp.Capacity,
                    CourseNo = sp.CourseNo,
                    StartDateTime = (DateTime)sp.StartDateTime
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostSection")]
        public async Task<IActionResult> PostSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                Section c = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new Section
                    {
                        SectionId = _SectionDTO.SectionId,
                        SectionNo = (byte)_SectionDTO.SectionNo,
                        CreatedBy = _SectionDTO.CreatedBy,
                        CreatedDate = _SectionDTO.CreatedDate,
                        Location = _SectionDTO.Location,
                        ModifiedBy = _SectionDTO.ModifiedBy,
                        ModifiedDate = _SectionDTO.ModifiedDate,
                        InstructorId = _SectionDTO.InstructorId,
                        SchoolId = _SectionDTO.SchoolId,
                        Capacity = (byte?)_SectionDTO.Capacity,
                        CourseNo = _SectionDTO.CourseNo,
                        StartDateTime = (DateTime)_SectionDTO.StartDateTime
                    };
                    _context.Sections.Add(c);
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
        [Route("PutSection")]
        public async Task<IActionResult> PutSection([FromBody] SectionDTO _SectionDTO)
        {
            try
            {
                Section c = await _context.Sections.Where(x => x.SectionId == _SectionDTO.SectionId).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.SectionId = _SectionDTO.SectionId;
                    c.SectionNo = (byte)_SectionDTO.SectionNo;
                    c.CreatedBy = _SectionDTO.CreatedBy;
                    c.CreatedDate = _SectionDTO.CreatedDate;
                    c.Location = _SectionDTO.Location;
                    c.ModifiedBy = _SectionDTO.ModifiedBy;
                    c.ModifiedDate = _SectionDTO.ModifiedDate;
                    c.InstructorId = _SectionDTO.InstructorId;
                    c.SchoolId = _SectionDTO.SchoolId;
                    c.Capacity = (byte?)_SectionDTO.Capacity;
                    c.CourseNo = _SectionDTO.CourseNo;
                    c.StartDateTime = (DateTime)_SectionDTO.StartDateTime;
                    _context.Sections.Update(c);
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
        [Route("DeleteSection/{_SectionId}")]
        public async Task<IActionResult> DeleteSection(int _SectionId)
        {
            try
            {
                Section c = await _context.Sections.Where(x => x.SectionId == _SectionId).FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Sections.Remove(c);
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