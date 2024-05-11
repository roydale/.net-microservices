using CommandService.Api.Data;
using CommandService.Api.Data.Repositories;
using CommandService.Api.EventProcessing;
using CommandService.Api.Services;
using CommandService.Api.Services.AsyncDataServices;
using CommandService.Api.Services.SyncDataServices.Grpc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//if (builder.Environment.IsProduction())
//{
//	Console.WriteLine("---> Using Sql Server database (CommandDb)");
//	builder.Services.AddDbContext<AppDbContext>(options =>
//			options.UseSqlServer(builder.Configuration.GetConnectionString("Command.Sql.Connection")));
//}
//else
//{
Console.WriteLine("---> Using In-Memory database (InMemoryCommandDb)");
builder.Services.AddDbContext<AppDbContext>(options =>
		options.UseInMemoryDatabase("InMemoryCommandDb"));
//}

builder.Services.AddScoped<PlatformsService>();
builder.Services.AddScoped<CommandsService>();

builder.Services.AddScoped<ICommandRepository, CommandRepository>();
builder.Services.AddScoped<IPlatformRepository, PlatformRepository>();

builder.Services.AddScoped<IPlatformDataClient, GrpcPlatformDataClient>();
builder.Services.AddSingleton<IEventProcessor, EventProcessor>();
builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
InitializeDatabase.PrePopulate(app);

app.Run();
