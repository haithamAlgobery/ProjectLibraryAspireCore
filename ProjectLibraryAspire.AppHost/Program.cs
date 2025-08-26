using Google.Protobuf.WellKnownTypes;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ProjectLibraryAspire_ApiService>("apiservice");

builder.AddProject<Projects.ProjectLibraryAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);


var postgres = builder.AddPostgres("pg")
    .WithDataVolume()  
    .WithPgAdmin();    

var db = postgres.AddDatabase("MyDb");


var redis = builder.AddRedis("redis");


var api= builder.AddProject<Projects.BookApi>("bookapi").WithReference(redis).WithReference(db);


builder.Build().Run();
