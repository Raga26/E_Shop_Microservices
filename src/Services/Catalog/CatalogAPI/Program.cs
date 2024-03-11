var builder = WebApplication.CreateBuilder(args);

// add services to container

var app = builder.Build();

// configure the http request pipeline

app.Run();
