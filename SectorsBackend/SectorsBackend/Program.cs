using Microsoft.EntityFrameworkCore;
using SectorsBackend.Data;
using SectorsBackend.Repositories;
using SectorsBackend.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPooledDbContextFactory<AppDbContext>(options 
	=> options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbContext")));
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddSingleton<ISectorsRepository, SectorsRepository>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();

builder.Services.AddControllers();
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

using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;

	var context = services.GetRequiredService<AppDbContext>();
	context.Database.EnsureCreated();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x => x
				.AllowAnyHeader()
				.AllowAnyMethod()
				.SetIsOriginAllowed(origin => true));

app.Run();
