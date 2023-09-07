using Proiect.BusinessLogic.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proiect.BusinessLogic.Implementation.Countries
{
	public class CountriesService : BaseService
	{
		public CountriesService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
		{

		}

		public List<string?> GetAvailableCountries()
		{
			var countryList = UnitOfWork.Country.Get().Select(p => p.Name).ToList();
			return countryList;
		}
	}
}
