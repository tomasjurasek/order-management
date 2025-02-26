var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.Eshop_Order>("webfrontend")
    .WithExternalHttpEndpoints();


builder.AddProject<Projects.Order_Partners>("order-partners");

builder.AddProject<Projects.Order_Writer>("order-writer");

builder.AddProject<Projects.Order_Reader>("order-reader");

builder.AddProject<Projects.Order_Integrator>("order-integrator");

builder.AddProject<Projects.Order_API>("order-api");

builder.Build().Run();
