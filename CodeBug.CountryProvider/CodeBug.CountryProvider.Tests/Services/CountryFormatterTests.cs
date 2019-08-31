using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Services;
using CodeBug.CountryProvider.Tests.Generator;
using Microsoft.Xrm.Sdk;
using NUnit.Framework;

namespace CodeBug.CountryProvider.Tests.Services
{
    public class CountryFormatterTests
    {
        private readonly string _argCode = "ARG";
        private readonly CountryFormatter _countryFormatter;
        public CountryFormatterTests()
        {
            _countryFormatter = new CountryFormatter();
        }
        [Test]
        public void ConvertToEntity_CountryWithCode_ReturnsEntity()
        {
            var country = CountryDataGenerator.GetCountryWithOutCode();
            country.Code = _argCode;

            var entity = _countryFormatter.ConvertToEntity(country);
            Assert.IsNotNull(entity);
            Assert.True(entity.Attributes.ContainsKey(CountrySchemaNames.CountryId));
        }

        [Test]
        public void ConvertToEntity_NoCountry_ReturnsNull()
        {
            var entity = _countryFormatter.ConvertToEntity(null);
            Assert.IsNull(entity);
        }

        [Test]
        public void ConvertToEntity_NoCode_ThrowsError()
        {
            var country = CountryDataGenerator.GetCountryWithOutCode();
            Assert.Throws<InvalidPluginExecutionException>(() => _countryFormatter.ConvertToEntity(country));
        }
    }
}
