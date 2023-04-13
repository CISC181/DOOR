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

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchoolController : BaseController
    {
        public SchoolController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetSchool")]
        public async Task<IActionResult> GetSchool()
        {
            List<SchoolDTO> lst = await _context.Schools
                .Select(sp => new SchoolDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolName = sp.SchoolName,
                    SchoolId = sp.SchoolId
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetSchool/{_SchoolId}")]
        public async Task<IActionResult> GetSchool(int _SchoolId)
        {
            SchoolDTO? lst = await _context.Schools
                .Where(x => x.SchoolId == _SchoolId)
                .Select(sp => new SchoolDTO
                {
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    SchoolName = sp.SchoolName,
                    SchoolId = sp.SchoolId
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostSchool")]
        public async Task<IActionResult> PostSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            try
            {
                School c = await _context.Schools.Where(x => x.SchoolId == _SchoolDTO.SchoolId).FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new School
                    {
                        CreatedBy = _SchoolDTO.CreatedBy,
                        CreatedDate = _SchoolDTO.CreatedDate,
                        ModifiedBy = _SchoolDTO.ModifiedBy,
                        ModifiedDate = _SchoolDTO.ModifiedDate,
                        SchoolName = _SchoolDTO.SchoolName,
                        SchoolId = _SchoolDTO.SchoolId
                    };
                    _context.Schools.Add(c);
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
        [Route("PutSchool")]
        public async Task<IActionResult> PutSchool([FromBody] SchoolDTO _SchoolDTO)
        {
            try
            {
                School c = await _context.Schools.Where(x => x.SchoolId == _SchoolDTO.SchoolId).FirstOrDefaultAsync();

                if (c != null)
                {
                    c.CreatedBy = _SchoolDTO.CreatedBy;
                    c.CreatedDate = _SchoolDTO.CreatedDate;
                    c.ModifiedBy = _SchoolDTO.ModifiedBy;
                    c.ModifiedDate = _SchoolDTO.ModifiedDate;
                    c.SchoolName = _SchoolDTO.SchoolName;
                    c.SchoolId = _SchoolDTO.SchoolId;
                    _context.Schools.Update(c);
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
        [Route("DeleteSchool/{_SchoolNo}")]
        public async Task<IActionResult> DeleteSchool(int _SchoolId)
        {
            try
            {
                School c = await _context.Schools.Where(x => x.SchoolId == _SchoolId).FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.Schools.Remove(c);
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