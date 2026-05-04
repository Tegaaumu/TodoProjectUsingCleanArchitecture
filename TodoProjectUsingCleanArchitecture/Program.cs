using TodoProjectUsingCleanArchitecture;
using TodoProjectUsingCleanArchitecture.Application.Database.Real;
using TodoProjectUsingCleanArchitecture.Presentation.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddApplication();
builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFlutter", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",
                "https://yourflutterapp.com"
              )
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowFlutter", policy =>
//    {
//        policy.AllowAnyHeader()
//              .AllowAnyMethod()
//              .SetIsOriginAllowed(_ => true)
//              .AllowCredentials();
//    });
//});
//builder.Services.AddScoped<ITodoListRepositories, TodoListRepositories>();

var app = builder.Build();

// Initialize the database
using (var scope = app.Services.CreateScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
    await initializer.InitializeAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFlutter");

app.UseAuthorization();

app.MapControllers();
app.MapHub<TodoProjectUsingCleanArchitecture.Presentation.Hubs.TodoHub>("/todohub");

app.Run();
