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
using GradeTypeWeight = DOOR.EF.Models.GradeTypeWeight;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class GradeTypeWeightController : BaseController
    {
        public GradeTypeWeightController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetGradeTypeWeight")]
        public async Task<IActionResult> GetGradeTypeWeight()
        {
            List<GradeTypeWeightDTO> lst = await _context.GradeTypeWeights
                .Select(sp => new GradeTypeWeightDTO
                {
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    DropLowest = sp.DropLowest,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetGradeTypeWeight/{_SchoolId, _SectionId, _GradeTypeCode}")]
        public async Task<IActionResult> GetGradeTypeWeight(int _SchoolId, int _SectionID, string _GradeTypeCode)
        {
            GradeTypeWeightDTO? lst = await _context.GradeTypeWeights
                .Where(x => x.SchoolId == _SchoolId)
                .Where(x => x.SectionId == _SectionID)
                .Where(x => x.GradeTypeCode == _GradeTypeCode)
                .Select(sp => new GradeTypeWeightDTO
                {
                    SchoolId = sp.SchoolId,
                    SectionId = sp.SectionId,
                    GradeTypeCode = sp.GradeTypeCode,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    NumberPerSection = sp.NumberPerSection,
                    PercentOfFinalGrade = sp.PercentOfFinalGrade,
                    DropLowest = sp.DropLowest,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).FirstOrDefaultAsync();
            return Ok(lst);
        }


        [HttpPost]
        [Route("PostGradeTypeWeight")]
        public async Task<IActionResult> PostGradeTypeWeight([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                GradeTypeWeight c = await _context.GradeTypeWeights
                    .Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c == null)
                {
                    c = new GradeTypeWeight
                    {
                        SchoolId = _GradeTypeWeightDTO.SchoolId,
                        SectionId = _GradeTypeWeightDTO.SectionId,
                        GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode,
                        CreatedBy = _GradeTypeWeightDTO.CreatedBy,
                        CreatedDate = _GradeTypeWeightDTO.CreatedDate,
                        NumberPerSection = _GradeTypeWeightDTO.NumberPerSection,
                        PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade,
                        DropLowest = _GradeTypeWeightDTO.DropLowest,
                        ModifiedBy = _GradeTypeWeightDTO.ModifiedBy,
                        ModifiedDate = _GradeTypeWeightDTO.ModifiedDate
                    };
                    _context.GradeTypeWeights.Add(c);
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
        [Route("PutGradeTypeWeight")]
        public async Task<IActionResult> PutGradeTypeWeight([FromBody] GradeTypeWeightDTO _GradeTypeWeightDTO)
        {
            try
            {
                GradeTypeWeight? c = await _context.GradeTypeWeights
                    .Where(x => x.SchoolId == _GradeTypeWeightDTO.SchoolId)
                    .Where(x => x.SectionId == _GradeTypeWeightDTO.SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeWeightDTO.GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    c.SchoolId = _GradeTypeWeightDTO.SchoolId;
                    c.SectionId = _GradeTypeWeightDTO.SectionId;
                    c.GradeTypeCode = _GradeTypeWeightDTO.GradeTypeCode;
                    c.CreatedBy = _GradeTypeWeightDTO.CreatedBy;
                    c.CreatedDate = _GradeTypeWeightDTO.CreatedDate;
                    c.NumberPerSection = _GradeTypeWeightDTO.NumberPerSection;
                    c.PercentOfFinalGrade = _GradeTypeWeightDTO.PercentOfFinalGrade;
                    c.DropLowest = _GradeTypeWeightDTO.DropLowest;
                    c.ModifiedBy = _GradeTypeWeightDTO.ModifiedBy;
                    c.ModifiedDate = _GradeTypeWeightDTO.ModifiedDate;
                    _context.GradeTypeWeights.Update(c);
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
        [Route("DeleteGradeTypeWeight/{_SchoolId, _SectionId, _GradeTypeCode}")]
        public async Task<IActionResult> DeleteGradeTypeWeight(int _SchoolId, int _SectionId, string _GradeTypeCode, GradeTypeWeight x)
        {
            try
            {
                GradeTypeWeight c = await _context.GradeTypeWeights
                    .Where(x => x.SchoolId == _SchoolId)
                    .Where(x => x.SectionId == _SectionId)
                    .Where(x => x.GradeTypeCode == _GradeTypeCode)
                    .FirstOrDefaultAsync();

                if (c != null)
                {
                    _context.GradeTypeWeights.Remove(c);
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