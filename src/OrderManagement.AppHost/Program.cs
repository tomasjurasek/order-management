var builder = DistributedApplication.CreateBuilder(args);


builder.AddProject<Projects.Eshop_Order>("webfrontend")
    .WithExternalHttpEndpoints();

builder.AddProject<Projects.Order_Worker>("order-worker");

builder.AddProject<Projects.Order_Partners>("order-partners");

builder.AddProject<Projects.Order_Writer>("order-writer");

builder.AddProject<Projects.Order_Reader>("order-reader");

builder.Build().Run();
