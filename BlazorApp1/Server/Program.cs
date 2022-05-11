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
        
        //This doesnt seem to make any difference
        demoApp.UsePathBase(baseHref);
        
        demoApp.UseEndpoints(endpoints =>
        {
            endpoints.MapFallbackToFile(
                baseHref + "/{*path:nonfile}",
                $"{baseHref}/index.html");
        });
    });

app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.Run();
