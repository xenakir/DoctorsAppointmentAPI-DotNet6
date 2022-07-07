namespace DoctorsAppointment.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<City>().Navigation(c => c.Polyclinics).AutoInclude();
        //    modelBuilder.Entity<Polyclinic>().Navigation(p => p.City).AutoInclude();
        //}

        public DbSet<City> Cities { get; set; }
        public DbSet<Polyclinic> Polyclinics { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Specialization> Specializations { get; set; }
    }
}
