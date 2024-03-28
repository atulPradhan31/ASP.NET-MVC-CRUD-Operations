using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace CRUDTest
{
    public class CountriesServiceTest
    {
        private readonly ICountriesService _countriesService;

        //Constructor
        public CountriesServiceTest()
        {
            _countriesService = new CountriesService();
        }

        #region AddCountry Test Block
        //Case 1: When CountryAddRequest is null
        [Fact]
        public void AddCountry_NullCountry()
        {
            //Arrange
            CountryAddRequest? request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });
        }

        //Case 2: When CountryName is null
        [Fact]
        public void AddCountry_NullCountryName()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName = null };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                //Act
                _countriesService.AddCountry(request);
            });

        }

        // Case 3: When country names are duplicate
        [Fact]
        public void AddCountry_DuplicateCountryName()
        {
            //Arrange
            CountryAddRequest request1 = new CountryAddRequest() { CountryName = "Bharat" };
            CountryAddRequest request2 = new CountryAddRequest() { CountryName = "Bharat" };

            //Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _countriesService.AddCountry(request1);
                _countriesService.AddCountry(request2);
            });
        }

        //Case 4: When inputs are correct
        [Fact]
        public void AddCountry_ValidInput()
        {
            //Arrange
            CountryAddRequest request = new CountryAddRequest() { CountryName = "India" };

            //Act
            CountryResponse response = _countriesService.AddCountry(request);

            List<CountryResponse> actualResponse = _countriesService.GetAllCountries();

            //Assert
            Assert.True(response.CountryID != Guid.Empty);
            Assert.Contains(response, actualResponse);
        }

        #endregion


        #region Get All Countries

        //Case 1: List of the countries should be empty by default
        [Fact]
        public void GetAllCountries_EmptyList()
        {
            List<CountryResponse> actual_country_response_list = _countriesService.GetAllCountries();
            Assert.Empty(actual_country_response_list);
        }

        //Case 2: Adding a few countries to the list
        [Fact]
        public void GetAllCountries_AddFewCountries()
        {
            List<CountryAddRequest> requestlist = new List<CountryAddRequest>()
            {
                new CountryAddRequest() { CountryName = "Bharat" },
                new CountryAddRequest() { CountryName = "India" },
                new CountryAddRequest() { CountryName = "Hindustan" },
            };

            List<CountryResponse> countryResponses = new();

            foreach (CountryAddRequest request in requestlist)
                countryResponses.Add(_countriesService.AddCountry(request));

            List<CountryResponse> actualResponseList = _countriesService.GetAllCountries();

            foreach (CountryResponse response in countryResponses)
                Assert.Contains(response, actualResponseList);

        }

        #endregion


        #region GetCountryByCountryID

        //Case 1: The CountryID is null
        [Fact]
        public void GetCountryByCountryID_NullCountryID()
        {
            Guid? guid = null;
            CountryResponse response = _countriesService.GetCountryByCountryID(guid);
            Assert.Null(response);
        }

        //Case 2: A valid countryId is passed
        [Fact]
        public void GetCountryByCountryID_ValidCountryID()
        {
            CountryAddRequest request = new CountryAddRequest() { CountryName = "Japan" };
            CountryResponse response = _countriesService.AddCountry(request);

            CountryResponse countryByGetMethod = _countriesService.GetCountryByCountryID(response.CountryID);

            Assert.Equal(response, countryByGetMethod);
        }

        #endregion
    }
}
