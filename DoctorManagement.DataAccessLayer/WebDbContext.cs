using DoctorManagement.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DoctorManagement.DataAccessLayer
{
    public class WebDbContext : IdentityDbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options): base(options) { }
        public virtual DbSet<DoctorEntity> Doctors { get; set; }
        public virtual DbSet<PatientEntity> Patients { get; set; }
        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<ContactEntity> Contacts { get; set; }
        public virtual DbSet<DataSourceEntity> DataSource { get; set; }

    }
}
