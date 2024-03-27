using Entities;
using ServiceContracts;
using ServiceContracts.DTO;

namespace Services
{
    public class CountriesService : ICountriesService
    {
        //Creating a private field to store list of countries
        private readonly List<Country> _countries;

        //Constructor
        public CountriesService()
        {
            _countries = new List<Country>();
        }

        
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest)
        {
            if (countryAddRequest == null)
                throw new ArgumentNullException(nameof(countryAddRequest));

            if (countryAddRequest.CountryName == null)
                throw new ArgumentException(nameof(countryAddRequest.CountryName));

            if (_countries.Where(temp => temp.CountryName.Equals(countryAddRequest.CountryName)).Count() > 0)
                throw new ArgumentException("Given country name already exisits ");


            //Convert CountryAddRequest to Country object
            Country country = countryAddRequest.ToCountry();

            //Add new Guid to the Country
            country.CountryID = Guid.NewGuid();

            //Add the country to the list of the countries
            _countries.Add(country);

            //Return the country as an object of CountryResponse
            return country.ToCountryResponse();

        }

        public List<CountryResponse> GetAllCountries()
        {
           return _countries.Select(country => country.ToCountryResponse()).ToList();
        }

        public CountryResponse? GetCountryByCountryID(Guid? countryID)
        {
            if (countryID == null) return null;
            
            Country? countryById = _countries.FirstOrDefault(country => country.CountryID == countryID);

            if (countryById == null) return null;

            return countryById.ToCountryResponse();
        }
    }
}