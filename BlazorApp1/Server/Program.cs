using Microsoft.AspNetCore.ResponseCompression;

//var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllersWithViews();
//builder.Services.AddRazorPages();

//var app = builder.Build();

//var baseHref = "/demo";

////app.MapGet("/", () => Results.Redirect(baseHref));

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseWebAssemblyDebugging();
//}
//else
//{
//    app.UseExceptionHandler("/Error");
//}

//app.UsePathBase(baseHref);

//app.UseWhen(match =>
//        match.Request.Path.StartsWithSegments(baseHref),
//    demoApp =>
//    {
//        //This doesnt seem to make any difference or breaks the app completely
//        //May need to remove <base href="/demo/"> from Index.html but doing so also doesnt seem to make a difference
//        //demoApp.UsePathBase(baseHref);

//        demoApp.UseRouting();
//        demoApp.UseBlazorFrameworkFiles(baseHref);
//        demoApp.UseStaticFiles();
//        demoApp.UseStaticFiles(baseHref);
        
//        demoApp.UseEndpoints(endpoints =>
//        {
//            endpoints.MapFallbackToFile(
//                baseHref + "/{*path:nonfile}",
//                $"{baseHref}/index.html");
//        });
//    });

//app.UseRouting();


//app.MapRazorPages();
//app.MapControllers();

//app.Run();


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("/demo/{*path:nonfile}", "/demo/index.html");

await app.RunAsync();
