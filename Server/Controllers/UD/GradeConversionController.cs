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
using GradeConversion = DOOR.EF.Models.GradeConversion;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeConversionController : BaseController
    {
        public GradeConversionController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetGradeConversion")]
        public async Task<IActionResult> GetGradeConversion()
        {
            List<GradeConversionDTO> lst = await _context.GradeConversions
                .Select(sp => new GradeConversionDTO
                {
                    LetterGrade = sp.LetterGrade,
                    GradePoint = sp.GradePoint,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    MaxGrade = sp.MaxGrade,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    MinGrade = sp.MinGrade,
                    SchoolId = sp.SchoolId
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetGradeConversion/{_SchoolId,_letterGrade}")]
        public async Task<IActionResult> GetGradeConversion(int _SchoolId, string _LetterGrade)
        {
            GradeConversionDTO? lst = await _context.GradeConversions
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.LetterGrade == _LetterGrade)
                .Select(sp => new GradeConversionDTO
                {
                    LetterGrade = sp.LetterGrade,
                    GradePoint = sp.GradePoint,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    MaxGrade = sp.MaxGrade,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate,
                    MinGrade = sp.MinGrade,
                    SchoolId = sp.SchoolId,
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostGradeConversion")]
        public async Task<IActionResult> PostGradeConversion([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                GradeConversion c = await _context.GradeConversions
                    .Where(x => x.SchoolId == _GradeConversionDTO.SchoolId)
                    .Where(x => x.LetterGrade == _GradeConversionDTO.LetterGrade)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new GradeConversion
                    {
                        LetterGrade = _GradeConversionDTO.LetterGrade,
                        GradePoint = _GradeConversionDTO.GradePoint,
                        CreatedBy = _GradeConversionDTO.CreatedBy,
                        CreatedDate = _GradeConversionDTO.CreatedDate,
                        MaxGrade = _GradeConversionDTO.MaxGrade,
                        ModifiedBy = _GradeConversionDTO.ModifiedBy,
                        ModifiedDate = _GradeConversionDTO.ModifiedDate,
                        MinGrade = _GradeConversionDTO.MinGrade,
                        SchoolId = _GradeConversionDTO.SchoolId                      
                    };
                    _context.GradeConversions.Add(c);
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
        [Route("PutGradeConversion")]
        public async Task<IActionResult> PutGradeConversion([FromBody] GradeConversionDTO _GradeConversionDTO)
        {
            try
            {
                GradeConversion ?c = await _context.GradeConversions
                    .Where(x => x.SchoolId == _GradeConversionDTO.SchoolId)
                    .Where(x => x.LetterGrade == _GradeConversionDTO.LetterGrade)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.LetterGrade = _GradeConversionDTO.LetterGrade;
                    c.GradePoint = _GradeConversionDTO.GradePoint;
                    c.CreatedBy = _GradeConversionDTO.CreatedBy;
                    c.CreatedDate = _GradeConversionDTO.CreatedDate;
                    c.MaxGrade = _GradeConversionDTO.MaxGrade;
                    c.ModifiedBy = _GradeConversionDTO.ModifiedBy;
                    c.ModifiedDate = _GradeConversionDTO.ModifiedDate;
                    c.MinGrade = _GradeConversionDTO.MinGrade;
                    c.SchoolId = _GradeConversionDTO.SchoolId;
                    _context.GradeConversions.Update(c);
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
        [Route("DeleteGradeConversion/{_SchoolId}")]
        public async Task<IActionResult> DeleteGradeConversion(int _SchoolId, string _LetterGrade, GradeConversion x)
        {
            try
            {
                GradeConversion c = await _context.GradeConversions
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.LetterGrade == _LetterGrade)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.GradeConversions.Remove(c);
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