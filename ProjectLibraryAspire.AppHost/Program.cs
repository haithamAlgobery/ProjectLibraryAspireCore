using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.ProjectLibraryAspire_ApiService>("apiservice");

builder.AddProject<Projects.ProjectLibraryAspire_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.AddProject<Projects.BookApi>("bookapi");
//builder.AddProject<BookApi>("bookservice");
builder.Build().Run();
