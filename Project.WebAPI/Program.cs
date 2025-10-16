using Microsoft.EntityFrameworkCore;
using Project.Application.Commands;
using Project.Application.Queries;
using Project.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSignalR();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// MediatR
builder.Services.AddMediatR(configuration =>
{
    configuration.RegisterServicesFromAssembly(typeof(GetMessagesQuery).Assembly);
    configuration.RegisterServicesFromAssembly(typeof(SendMessageCommand).Assembly);
});

// Repository
builder.Services.AddScoped<IChatRepository, ChatRepository>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials() 
            .SetIsOriginAllowed(_ => true));
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowAll");

app.MapControllers();

app.MapHub<Project.Infrastructure.ChatHub>("/chathub");

var url = "https://localhost:7202/index.html";
Task.Run(async () =>
{
    await Task.Delay(2000);
    try
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Browser open nahi hua: {ex.Message}");
    }
});

app.Run();



