using UserControl.Model;

var builder = WebApplication.CreateBuilder(args);



string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddPostgres(connectionString);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
