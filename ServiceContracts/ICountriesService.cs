using ServiceContracts.DTO;

namespace ServiceContracts
{
    /// <summary>
    /// Represents buisness login for manipulation country entity
    /// </summary>
    public interface ICountriesService
    {
        /// <summary>
        /// Adds a country object to the list of the countries
        /// </summary>
        /// <param name="countryAddRequest">Country object to add</param>
        /// <returns>Returns the country object after adding it (including the newly generated country id)</returns>
        public CountryResponse AddCountry(CountryAddRequest? countryAddRequest);

        /// <summary>
        /// Returns a list of all countries
        /// </summary>
        /// <returns>Returns a list of countries as object of CountryResponse</returns>
        List<CountryResponse> GetAllCountries();

        /// <summary>
        /// Get a country by country_id
        /// </summary>
        /// <param name="countryID">Nullable country_id of Guid type</param>
        /// <returns>An object of CountryResponse for matching Guid</returns>
        CountryResponse? GetCountryByCountryID(Guid? countryID);

    }
}