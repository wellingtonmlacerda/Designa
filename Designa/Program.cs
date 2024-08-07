using Designa.Data;
using Designa.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<DesignaContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DesignaConection"))
);

builder.Services.AddDependency();
builder.Services.AddLocalization();

var app = builder.Build();

//Register Syncfusion license
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration["SyncfusionLicense"]);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(new[] { "en-US", "pt-BR" })
    .AddSupportedUICultures(new[] { "en-US", "pt-BR" }));

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Publicacao}/{action=Index}/{id?}");

app.Run();
