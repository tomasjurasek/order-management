var builder = DistributedApplication.CreateBuilder(args);


var messaging = builder.AddKafka("messaging")
    .WithKafkaUI();

var orderWriterDb = builder.AddSqlServer("order-writer-db").WithDataVolume();
var orderReaderDb = builder.AddRedis("order-reader-db").WithDataVolume();

builder.AddProject<Projects.Order_Writer>("order-writer")
    .WithReference(messaging)
    .WaitFor(messaging)
    .WithReference(orderWriterDb)
    .WaitFor(orderWriterDb);

builder.AddProject<Projects.Order_Reader>("order-reader")
     .WithReference(messaging)
     .WaitFor(messaging)
     .WithReference(orderReaderDb)
     .WaitFor(orderWriterDb);


builder.AddProject<Projects.Order_Integrator>("order-integrator")
    .WithReference(messaging)
    .WaitFor(messaging);

builder.AddProject<Projects.Order_API>("order-api");


builder.AddProject<Projects.Eshop_Order>("eshop-order")
    .WithExternalHttpEndpoints();


builder.Build().Run();
