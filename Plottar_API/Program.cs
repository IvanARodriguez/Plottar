using Plottar_API.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve database username and password from configuration
var dbUsername = builder.Configuration["plottar_db_username"];
var dbPassword = builder.Configuration["plottar_db_password"];


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (connectionString != null)
{
    connectionString = connectionString
        .Replace("{USERNAME}", dbUsername ?? "NULL")
        .Replace("{PASSWORD}", dbPassword ?? "NULL");
} else
{
  Console.WriteLine("Connection string is null");
}

// Configure database connection
builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

// Test database connection
try
{
  using (var conn = new NpgsqlConnection(connectionString))
  {
    conn.Open();

    using (var cmd = new NpgsqlCommand("SELECT 1", conn))
    {
      var result = cmd.ExecuteScalar();
      if (result != null && result.ToString() == "1")
      {
        Console.WriteLine("Connection to PostgreSQL successful!");
      }
      else
      {
        Console.WriteLine("Connection to PostgreSQL failed.");
      }
    }
  }
}
catch (NpgsqlException ex)
{
  Console.WriteLine($"Error connecting to PostgreSQL: {ex.Message}");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
