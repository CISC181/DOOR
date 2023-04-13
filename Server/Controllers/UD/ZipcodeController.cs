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
using Zipcode = DOOR.EF.Models.Zipcode;
using static Duende.IdentityServer.Models.IdentityResources;

namespace CSBA6.Server.Controllers.app
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZipcodeController : BaseController
    {
        public ZipcodeController(DOOROracleContext _DBcontext,
            OraTransMsgs _OraTransMsgs)
            : base(_DBcontext, _OraTransMsgs)

        {
        }


        [HttpGet]
        [Route("GetZipcode")]
        public async Task<IActionResult> GetZipcode()
        {
            List<ZipcodeDTO> lst = await _context.Zipcodes
                .Select(sp => new ZipcodeDTO
                {
                    Zip = sp.Zip,
                    City = sp.City,
                    State = sp.State,
                    CreatedBy = sp.CreatedBy,
                    CreatedDate = sp.CreatedDate,
                    ModifiedBy = sp.ModifiedBy,
                    ModifiedDate = sp.ModifiedDate
                }).ToListAsync();
            return Ok(lst);
        }


        [HttpGet]
        [Route("GetZipcode/{_Zip}")]
        public async Task<IActionResult> GetZipcode(string _Zip)
    {
        ZipcodeDTO? lst = await _context.Zipcodes
            .Where(x => x.Zip == _Zip)
            .Select(sp => new ZipcodeDTO
            {
                Zip = sp.Zip,
                City = sp.City,
                State = sp.State,
                CreatedBy = sp.CreatedBy,
                CreatedDate = sp.CreatedDate,
                ModifiedBy = sp.ModifiedBy,
                ModifiedDate = sp.ModifiedDate
            }).FirstOrDefaultAsync();
        return Ok(lst);
    }


    [HttpPost]
    [Route("PostZipcode")]
    public async Task<IActionResult> PostZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
    {
        try
        {
            Zipcode c = await _context.Zipcodes
                .Where(x => x.Zip == _ZipcodeDTO.Zip)
                .FirstOrDefaultAsync();

            if (c == null)
            {
                c = new Zipcode
                {
                    Zip = _ZipcodeDTO.Zip,
                    City = _ZipcodeDTO.City,
                    State = _ZipcodeDTO.State,
                    CreatedBy = _ZipcodeDTO.CreatedBy,
                    CreatedDate = _ZipcodeDTO.CreatedDate,
                    ModifiedBy = _ZipcodeDTO.ModifiedBy,
                    ModifiedDate = _ZipcodeDTO.ModifiedDate
                };
                _context.Zipcodes.Add(c);
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
    [Route("PutZipcode")]
    public async Task<IActionResult> PutZipcode([FromBody] ZipcodeDTO _ZipcodeDTO)
    {
        try
        {
            Zipcode? c = await _context.Zipcodes
                .Where(x => x.Zip == _ZipcodeDTO.Zip)
                .FirstOrDefaultAsync();

            if (c != null)
            {
                c.Zip = _ZipcodeDTO.Zip;
                c.City = _ZipcodeDTO.City;
                c.State = _ZipcodeDTO.State;
                c.CreatedBy = _ZipcodeDTO.CreatedBy;
                c.CreatedDate = _ZipcodeDTO.CreatedDate;
                c.ModifiedBy = _ZipcodeDTO.ModifiedBy;
                c.ModifiedDate = _ZipcodeDTO.ModifiedDate;
                _context.Zipcodes.Update(c);
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
    [Route("DeleteZipcode/{_SchoolId}")]
    public async Task<IActionResult> DeleteZipcode(string _Zip, Zipcode x)
    {
        try
        {
            Zipcode c = await _context.Zipcodes
                .Where(x => x.Zip == _Zip)
                .FirstOrDefaultAsync();

            if (c != null)
            {
                _context.Zipcodes.Remove(c);
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