using CodeBug.CountryProvider.Services;
using CodeBug.CountryProvider.Tests.Generator;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Tests.Services
{
    public class QueryValidatorTests
    {
        private readonly ITracingService _tracingService = Substitute.For<ITracingService>();

        [Test]
        public void Validate_QueryWithNoCondition_False()
        {
            var queryValidator = new QueryValidator(_tracingService, new QueryExpression("Test"));

            Assert.IsFalse(queryValidator.Validate());
        }

        [Test]
        public void Validate_QueryWithWrongOperator_False()
        {
            var queryValidator = new QueryValidator(_tracingService, QueryExpressionGenerator.GenerateQueryExpressionWithWrongOperator());

            Assert.IsFalse(queryValidator.Validate());
        }

        [Test]
        public void Validate_QueryWithWrongField_False()
        {
            var queryValidator = new QueryValidator(_tracingService, QueryExpressionGenerator.GenerateQueryExpressionWithWrongField());

            Assert.IsFalse(queryValidator.Validate());
        }

        [Test]
        public void Validate_QueryWithMoreConditions_False()
        {
            var queryValidator = new QueryValidator(_tracingService, QueryExpressionGenerator.GenerateQueryExpressionWithMultipleCondition());

            Assert.IsFalse(queryValidator.Validate());
        }

        [Test]
        public void Validate_QueryWithCorrectCondition_True()
        {
            var queryValidator = new QueryValidator(_tracingService, QueryExpressionGenerator.GenerateQueryExpressionWithCorrectCondtion());

            Assert.True(queryValidator.Validate());
        }
    }
}
