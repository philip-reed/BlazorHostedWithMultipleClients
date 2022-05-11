using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

var baseHref = "/demo";

app.MapGet("/", () => Results.Redirect(baseHref));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseWhen(match =>
        match.Request.Path.StartsWithSegments(baseHref),
    demoApp =>
    {
        demoApp.UseRouting();
        demoApp.UseBlazorFrameworkFiles(baseHref);
        demoApp.UseStaticFiles();
        demoApp.UseStaticFiles(baseHref);
        demoApp.UseEndpoints(endpoints =>
        {
            endpoints.MapFallbackToFile(
                baseHref + "/{*path:nonfile}",
                $"{baseHref}/index.html");
        });
    });

//app.UseBlazorFrameworkFiles(baseHref);
//app.UseStaticFiles();
//app.UseStaticFiles(baseHref);

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
//app.MapFallbackToFile("index.html");

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapFallbackToFile(
//        baseHref + "/{*path:nonfile}",
//        $"{baseHref}/index.html");
//});

app.Run();
