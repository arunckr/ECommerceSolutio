using ECommerceApi;

var builder = WebApplication.CreateBuilder(args);

//create new instance of Startup
var startup = new StartUp(builder.Configuration);

//configure all services
startup.ConfigureServices(builder.Services);

var app = builder.Build();

//configure the pipeline
startup.Configure(app, builder.Environment);

//run
app.Run();
