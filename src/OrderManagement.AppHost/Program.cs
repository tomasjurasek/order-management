var builder = DistributedApplication.CreateBuilder(args);


var messaging = builder.AddKafka("messaging")
    .WithKafkaUI();

var orderWriterDb = builder.AddSqlServer("order-writer-db").WithDataVolume();
var orderReaderDb = builder.AddRedis("order-reader-db").WithDataVolume();
var orderIntegratorDb = builder.AddRedis("order-integrator-db").WithDataVolume();

var orderWriter = builder.AddProject<Projects.Order_Writer>("order-writer")
    .WithReference(messaging)
    .WaitFor(messaging)
    .WithReference(orderWriterDb)
    .WaitFor(orderWriterDb);

var orderReader = builder.AddProject<Projects.Order_Reader>("order-reader")
     .WithReference(messaging)
     .WaitFor(messaging)
     .WithReference(orderReaderDb)
     .WaitFor(orderReaderDb);


builder.AddProject<Projects.Order_Integrator>("order-integrator")
    .WithReference(messaging)
    .WaitFor(messaging)
    .WithReference(orderIntegratorDb)
    .WaitFor(orderIntegratorDb);


builder.AddProject<Projects.Order_API>("order-api")
    .WithReference(orderWriter)
    .WithReference(orderReader);


builder.AddProject<Projects.Eshop_Order>("eshop-order")
    .WithExternalHttpEndpoints();


builder.Build().Run();
