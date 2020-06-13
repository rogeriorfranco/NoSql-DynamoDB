using System.Diagnostics.CodeAnalysis;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Poc_NoSql.Models;
using ServiceStack.Aws.DynamoDb;

namespace Poc_NoSql.Dynamodb
{
    [ExcludeFromCodeCoverage]
    public static class DynamoServiceExtension
    {
        public static void AddDynamoDB(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            if (hostingEnvironment.IsDevelopment())
            {
                var dynamoDBConfig = configuration.GetSection("DynamoDB");
                var client = new AmazonDynamoDBClient(dynamoDBConfig["AccessKey"],
                                                      dynamoDBConfig["SecretKey"],
                                                      new AmazonDynamoDBConfig
                                                      {
                                                          ServiceURL = dynamoDBConfig["ServiceURL"]
                                                      });

                services.AddSingleton<IAmazonDynamoDB>(client);

                var pocoDynamo = new PocoDynamo(client);

                services.AddSingleton<IPocoDynamo>(pocoDynamo);

                pocoDynamo.RegisterTable<Tariff>();
                pocoDynamo.InitSchema();

            }
            else if (hostingEnvironment.EnvironmentName != "Test")
            {
                services.AddDefaultAWSOptions(configuration.GetAWSOptions());
                services.AddAWSService<IAmazonDynamoDB>();
                services.AddSingleton<IPocoDynamo>(p => new PocoDynamo(p.GetService<IAmazonDynamoDB>()));
            }

            services.AddScoped<IDynamoDBContext, DynamoDBContext>();
        }
    }
}

