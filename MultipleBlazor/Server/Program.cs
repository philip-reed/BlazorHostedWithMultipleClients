var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/app1", StringComparison.OrdinalIgnoreCase), first =>
{
    first.Use((ctx, nxt) =>
    {
        ctx.Request.Path = "/app1" + ctx.Request.Path;
        return nxt();
    });

    first.UsePathBase("/app1");
    first.UseBlazorFrameworkFiles("/app1");
    first.UseStaticFiles();
    first.UseStaticFiles("/app1");
    first.UseRouting();

    first.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/app1/{*path:nonfile}",
            "app1/index.html");
    });
});

app.MapWhen(ctx => ctx.Request.Path.StartsWithSegments("/app2", StringComparison.OrdinalIgnoreCase), second =>
{
    second.Use((ctx, nxt) =>
    {
        ctx.Request.Path = "/app2" + ctx.Request.Path;
        return nxt();
    });

    second.UsePathBase("/app2");
    second.UseBlazorFrameworkFiles("/app2");
    second.UseStaticFiles();
    second.UseStaticFiles("/app2");
    second.UseRouting();

    second.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("/app2/{*path:nonfile}",
            "app2/index.html");
    });
});

app.Run();
