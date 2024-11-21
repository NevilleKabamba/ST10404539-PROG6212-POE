using ContractMonthlyClaimSys.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContractMonthlyClaimSys.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Claim> Claims { get; set; }
        public DbSet<UploadedFile> UploadedFiles { get; set; }
    }
}
