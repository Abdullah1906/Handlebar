using HandlebarPractice.Helpers;
using HandlebarPractice.Interfaces;
using HandlebarPractice.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// ?? REGISTER CUSTOMER SERVICE
builder.Services.AddScoped<ICustomer, CustomerService>();
builder.Services.AddScoped<ITemplateResolver,TemplateResolver>();
builder.Services.AddScoped<ITemplate,TemplateService>();
builder.Services.AddScoped<IPdf, PdfService>();
HandlebarsHelpers.RegisterHelpers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HandleBar}/{action=Index}/{id?}");

app.Run();
