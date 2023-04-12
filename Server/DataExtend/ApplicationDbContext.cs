using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using DOOR.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOOR.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.Extensions.Options;


namespace DOOR.Server.Data
{
    public partial class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.HasDefaultSchema("UD_COLEJM")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            builder.ToUpperCaseTables();
            builder.ToUpperCaseColumns();
            builder.ToUpperCaseForeignKeys();


            // builder.AddFootprintColumns();
            builder.FinalAdjustments();




        }
    }
}