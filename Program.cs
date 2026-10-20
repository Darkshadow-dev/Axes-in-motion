using Axes_in_Motion_Server;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
string sqlConnectionString = Environment.GetEnvironmentVariable("AXES_IN_MOTION_SQL_CONNECTION")
    ?? "Server=localhost;Database=AxesInMotion;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;Connect Timeout=15;";
var store = new SqlServerStore(sqlConnectionString);
builder.Services.AddSingleton(store);

string serverUrls = Environment.GetEnvironmentVariable("AXES_IN_MOTION_SERVER_URLS") ?? "http://localhost:5080";
builder.WebHost.UseUrls(serverUrls);

var app = builder.Build();
app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    await context.Response.WriteAsJsonAsync(new
    {
        error = "The request could not access SQL Server. See the server console for the database error."
    });
}));

app.UseDefaultFiles();
app.UseStaticFiles();

string solutionRoot = Path.GetFullPath(Path.Combine(app.Environment.ContentRootPath, ".."));
app.MapGet("/AxesInMotion.sql", () => Results.File(Path.Combine(solutionRoot, "AxesInMotion.sql"), "text/plain"));
app.MapGet("/ViewSavedData.sql", () => Results.File(Path.Combine(solutionRoot, "ViewSavedData.sql"), "text/plain"));

app.MapGet("/api/info", () => Results.Ok(new
{
    service = "Axes in Motion data server",
    database = "SQL Server / AxesInMotion",
    endpoints = new[] { "/health", "/api/history?userName=...", "/api/entries/latest?userName=...&gameId=...", "/api/entries" }
}));

app.MapGet("/health", async (SqlServerStore sql, CancellationToken cancellationToken) =>
{
    try
    {
        bool databaseReady = await sql.CheckDatabaseAsync(cancellationToken);
        return databaseReady
            ? Results.Ok(new { status = "ok", database = "connected" })
            : Results.Json(new { status = "database_not_ready", database = "missing_schema" }, statusCode: StatusCodes.Status503ServiceUnavailable);
    }
    catch (Exception exception)
    {
        app.Logger.LogError(exception, "SQL Server health check failed.");
        return Results.Json(new { status = "database_unavailable", database = "disconnected" }, statusCode: StatusCodes.Status503ServiceUnavailable);
    }
});

app.MapGet("/api/history", async (string? userName, SqlServerStore sql, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(userName))
        return Results.BadRequest(new { error = "Provide the userName query parameter." });

    return Results.Ok(await sql.GetHistoryAsync(userName.Trim(), cancellationToken));
});

app.MapGet("/api/entries/latest", async (string? userName, int? gameId, SqlServerStore sql, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(userName))
        return Results.BadRequest(new { error = "Provide the userName query parameter." });
    if (gameId is null or <= 0)
        return Results.BadRequest(new { error = "Provide a positive gameId query parameter." });

    SavedEntryDto? entry = await sql.GetLatestAsync(userName.Trim(), gameId.Value, cancellationToken);
    return entry is null ? Results.NotFound() : Results.Ok(entry);
});

app.MapPost("/api/entries", async (SaveEntryRequest? request, SqlServerStore sql, CancellationToken cancellationToken) =>
{
    if (request is null)
        return Results.BadRequest(new { error = "A record body is required." });
    if (string.IsNullOrWhiteSpace(request.UserName) || request.UserName.Trim().Length > 80)
        return Results.BadRequest(new { error = "User name must be between 1 and 80 characters." });
    if (request.GameId <= 0 || string.IsNullOrWhiteSpace(request.GameName) || request.GameName.Trim().Length > 200)
        return Results.BadRequest(new { error = "A valid game ID and game name are required." });
    if (request.Values is null || request.Values.Count == 0 || request.Values.Count > 100)
        return Results.BadRequest(new { error = "Provide between 1 and 100 field values." });

    foreach ((string field, string value) in request.Values)
    {
        if (string.IsNullOrWhiteSpace(field) || field.Trim().Length > 100 || value is null || value.Length > 4000)
            return Results.BadRequest(new { error = "Field names must be 1–100 characters and values may not exceed 4,000 characters." });
    }

    SavedEntryDto entry = await sql.SaveAsync(request, cancellationToken);
    return Results.Created($"/api/entries/latest?userName={Uri.EscapeDataString(entry.UserName)}&gameId={entry.GameId}", entry);
});

app.Run();
