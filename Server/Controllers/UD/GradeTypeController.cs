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
using GradeType = DOOR.EF.Models.GradeType;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeTypeController : BaseController
    {
        public GradeTypeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetGradeType")]
        public async Task<IActionResult> GetGradeType()
        {
            List<GradeTypeDTO> lst = await _context.GradeTypes
                .Select(sp => new GradeTypeDTO
                {
                    SchoolId = sp.SchoolId,
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Description = sp.Description,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetGradeType/{_SchoolId,_GradeTypeCode}")]
        public async Task<IActionResult> GetGradeType(int _SchoolId, string _GradeTypeCode)
        {
            GradeTypeDTO? lst = await _context.GradeTypes
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.GradeTypeCode == _GradeTypeCode)
                .Select(sp => new GradeTypeDTO
                {
                    SchoolId = sp.SchoolId,
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    Description = sp.Description,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostGradeType")]
        public async Task<IActionResult> PostGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                GradeType c = await _context.GradeTypes
                    .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new GradeType
                    {
                        SchoolId = _GradeTypeDTO.SchoolId,
                        GradeTypeCode = _GradeTypeDTO.GradeTypeCode,
                        CreatedBy = _GradeTypeDTO.CreatedBy,
                        CreatedDate = _GradeTypeDTO.CreatedDate,
                        Description = _GradeTypeDTO.Description,
                        ModifiedBy = _GradeTypeDTO.ModifiedBy,
                        ModifiedDate = _GradeTypeDTO.ModifiedDate
                    };
                    _context.GradeTypes.Add(c);
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
        [Route("PutGradeType")]
        public async Task<IActionResult> PutGradeType([FromBody] GradeTypeDTO _GradeTypeDTO)
        {
            try
            {
                GradeType? c = await _context.GradeTypes
                    .Where(x => x.SchoolId == _GradeTypeDTO.SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.SchoolId = _GradeTypeDTO.SchoolId;
                    c.GradeTypeCode = _GradeTypeDTO.GradeTypeCode;
                    c.CreatedBy = _GradeTypeDTO.CreatedBy;
                    c.CreatedDate = _GradeTypeDTO.CreatedDate;
                    c.Description = _GradeTypeDTO.Description;
                    c.ModifiedBy = _GradeTypeDTO.ModifiedBy;
                    c.ModifiedDate = _GradeTypeDTO.ModifiedDate;
                    _context.GradeTypes.Update(c);
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
        [Route("DeleteGradeType/{_SchoolId, _GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeType(int _SchoolId, string _GradeTypeCode, GradeType x)
        {
            try
            {
                GradeType c = await _context.GradeTypes
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.GradeTypeCode == _GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.GradeTypes.Remove(c);
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