using CodeBug.CountryProvider.Constants;
using CodeBug.CountryProvider.Interfaces;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBug.CountryProvider.Services
{
    public class QueryValidator : IQueryValidator
    {
        private readonly string[] _supportedColumns = new string[] { CountrySchemaNames.Capital, CountrySchemaNames.Region };
        private readonly int[] _supportedOperator = new int[] { (int)ConditionOperator.Like, (int)ConditionOperator.Equal };        
        private readonly ITracingService _tracingService;
        private readonly QueryExpression _query;

        private DataCollection<ConditionExpression> _allExpressions => _query.Criteria.Conditions;

        public QueryValidator(ITracingService tracingService, QueryExpression query)
        {
            _tracingService = tracingService;
            _query = query;            
        }

        public bool Validate()
        {
            if (_allExpressions == null || _allExpressions.Count() == 0)
            {
                _tracingService.Trace($"No condition is given. Returning all records");
                return false;
            }

            if (_allExpressions.Any(t => !_supportedOperator.Contains((int)t.Operator)))
            {
                _tracingService.Trace($"No supported operator given. Returning all records");
                return false;
            }

            if (_allExpressions.Any(t => !_supportedColumns.Contains(t.AttributeName)))
            {
                _tracingService.Trace($"No supported field name given. Returning all records");
                return false;
            }

            if (_allExpressions.Count() > 1)
            {
                _tracingService.Trace($"More than one expression is given. Returning all records");
                return false;
            }
            return true;
        }
    }
}
