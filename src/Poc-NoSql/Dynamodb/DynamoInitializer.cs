using AspNetCore.AsyncInitialization;
using Poc_NoSql.Models;
using ServiceStack.Aws.DynamoDb;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Poc_NoSql.Dynamodb
{
    [ExcludeFromCodeCoverage]
    public class DynamoInitializer : IAsyncInitializer
    {
        private readonly IPocoDynamo _pocoDynamo;
        public DynamoInitializer(IPocoDynamo pocoDynamo) => _pocoDynamo = pocoDynamo;

        public Task InitializeAsync()
        {
            _pocoDynamo.RegisterTable<Tariff>();
            _pocoDynamo.InitSchema();
            return Task.CompletedTask;
        }
    }
}
