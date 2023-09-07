using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Proiect.BusinessLogic.Base;

namespace Proiect.BusinessLogic.Implementation.City;

public class CityService : BaseService
{
    public CityService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
    {
        
    }

    public List<string?> GetAllCities()
    {
        var cities = UnitOfWork.City.Get().Select(p => p.Name).ToList();
        return cities;
    }
}