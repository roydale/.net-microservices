using Microsoft.EntityFrameworkCore;
using PlatformService.Api.Data;
using PlatformService.Api.Services;
using PlatformService.Api.Services.AsyncDataServices;
using PlatformService.Api.Services.SyncDataServices.Grpc;
using PlatformService.Api.Services.SyncDataServices.Http;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
if (builder.Environment.IsProduction())
{
	Console.WriteLine("---> Using Sql Server database (PlatformDb)");
	builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseSqlServer(builder.Configuration.GetConnectionString("Platform.Sql.Connection")));
}
else
{
	Console.WriteLine("---> Using In-Memory database (InMemoryPlatformDb)");
	builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseInMemoryDatabase("InMemoryPlatformDb"));
}

builder.Services.AddScoped<PlatformsService>();
builder.Services.AddScoped<SyncDataService>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();
builder.Services.AddGrpc();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Console.WriteLine("---> CommandService Endpoint: {0}", builder.Configuration["CommandService"]);

var app = builder.Build();
// ======================================================================================================

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGrpcService<GrpcPlatformService>();
app.MapGet("/protos/platform.proto", async context =>
{
	await context.Response.WriteAsync(File.ReadAllText("Protos/platform.proto"));
});
InitializeDatabase.PrePopulate(app, app.Environment.IsProduction());

app.Run();