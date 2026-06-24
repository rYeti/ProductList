using System.Text.Json;
using System.Text.Json.Serialization;

// Copilot assisted with this minimal API configuration by suggesting
// explicit JSON serialization settings and CORS policy setup.
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    // Use camel-case JSON naming so the front-end model matches the API payload.
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());

// Copilot helped identify CORS as a likely root cause and suggested
// using an unrestricted local development policy. This ensures the
// Blazor client can access the API during development.
app.MapGet("/api/productlist", () =>
{
    // Copilot recommended using a strongly-typed response model
    // instead of anonymous types so the JSON contract is explicit.
    var products = new[]
    {
        new Product(
            Id: 1,
            Name: "Laptop",
            Price: 1200.50,
            Stock: 25,
            Category: new Category(Id: 101, Name: "Electronics")
        ),
        new Product(
            Id: 2,
            Name: "Headphones",
            Price: 50.00,
            Stock: 100,
            Category: new Category(Id: 102, Name: "Accessories")
        )
    };

    return Results.Json(products, new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    });
});

app.Run();

public sealed record Category(int Id, string Name);
public sealed record Product(int Id, string Name, double Price, int Stock, Category Category);