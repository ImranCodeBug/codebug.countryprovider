using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Interfaces;
using CodeBug.CountryProvider.Services;
using CodeBug.CountryProvider.Tests.Generator;
using CodeBug.CountryProvider.Utils;
using NSubstitute;
using NUnit.Framework;
using System;

namespace CodeBug.CountryProvider.Tests.Utils
{
    public class CountryQueryVisitorTests
    {
        [Test]
        public void Visit_NullSent_ThrowsException()
        {
            var queryValidator = Substitute.For<IQueryValidator>();
            var countryQueryVistor = new CountryQueryVisitor(queryValidator);
            Assert.Throws<ArgumentNullException>(() => countryQueryVistor.Visit(null));
        }

        [Test]
        public void Visit_CorrectQueryGiven_SearchingByFieldPopulated()
        {
            var query = QueryExpressionGenerator.GenerateQueryExpressionWithCorrectCondtion();
            var queryValidator = Substitute.For<IQueryValidator>();
            var countryQueryVisitor = new CountryQueryVisitor(queryValidator);

            queryValidator.Validate().Returns(true);

            countryQueryVisitor.Visit(query);

            Assert.IsNotNull(countryQueryVisitor.SearchingBy);
            Assert.IsNotNull(countryQueryVisitor.SearchedTerm);
            Assert.IsTrue(countryQueryVisitor.SearchingBy == CountrySchemaNames.Region);
            Assert.IsTrue(countryQueryVisitor.SearchedTerm == "Hello");
        }

        [Test]
        public void Visit_CorrectQueryGiven_SearchingByFieldHasDefault()
        {
            var query = QueryExpressionGenerator.GenerateQueryExpressionWithMultipleCondition();
            var queryValidator = Substitute.For<IQueryValidator>();
            var countryQueryVisitor = new CountryQueryVisitor(queryValidator);

            queryValidator.Validate().Returns(false);

            countryQueryVisitor.Visit(query);

            Assert.IsTrue(countryQueryVisitor.SearchingBy == "all");
        }
    }
}
