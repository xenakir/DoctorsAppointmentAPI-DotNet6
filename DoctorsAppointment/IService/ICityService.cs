global using Microsoft.AspNetCore.Mvc;

namespace DoctorsAppointment.IService
{
    public interface ICityService
    {
        List<City> GetCities();
        City? GetCity(Guid id);
        void AddCity(City city);
        void UpdateCity(City dbCity, UpdateCityDto request);
        void DeleteCity(City dbCity);
    }
}
