using AutoMapper;
using Commander.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
    });

// Connect to DB
var sqlConbuilder = new SqlConnectionStringBuilder();

sqlConbuilder.ConnectionString = builder.Configuration.GetConnectionString("CommanderConnection");
sqlConbuilder.UserID = builder.Configuration["UserID"];
sqlConbuilder.Password = builder.Configuration["Password"];

builder.Services.AddDbContext<CommanderContext>(opt => opt.UseSqlServer(
    sqlConbuilder.ConnectionString
));

// Dependency injection
builder.Services.AddScoped<ICommanderRepo, CommanderRepo>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

////////////////////////////////////////

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


