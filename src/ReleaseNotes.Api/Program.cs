var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.MapGet("/health", () => Results.Ok(new { status = "ok" }));

app.MapGet("/version", () =>
{
    var version = typeof(Program).Assembly
        .GetName()
        .Version?
        .ToString() ?? "0.0.0";

    return Results.Ok(new { version });
});

app.Run();