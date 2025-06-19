var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var app = builder.Build();
app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod());

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/generate", (int length, bool upper, bool lower, bool number, bool symbol) =>
{
    try
    {
        string password = PasswordGenerator.Generate(length, upper, lower, number, symbol);
        string strength = PasswordGenerator.GetStrength(password);
        return Results.Ok(new { password, strength });
    }
    catch (Exception ex)
    {
        return Results.BadRequest(new { error = ex.Message });
    }
});

app.Run();