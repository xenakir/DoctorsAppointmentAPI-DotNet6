namespace DoctorsAppointment.Service
{
    public class CityService : ControllerBase, ICityService
    {
        private readonly DataContext _context;

        public CityService(DataContext context)
        {
            _context = context;
        }
        public List<City> GetCities()
        {
            return _context.Cities.ToList();
        }

        public City? GetCity(Guid id)
        {
            return _context.Cities.
                Include(_ => _.Polyclinics)
                .ThenInclude(_ => _.Doctors)
                .FirstOrDefault(_ => _.Id == id);
        }

        public void AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
        }

        public void UpdateCity(City dbCity, UpdateCityDto request)
        {
            dbCity.Name = request.Name;
            dbCity.Type = request.Type;
            _context.SaveChanges();
        }

        public void DeleteCity(City dbCity)
        {
            _context.Cities.Remove(dbCity);
            _context.SaveChanges();
        }
    }
}
