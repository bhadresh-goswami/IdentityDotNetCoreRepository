using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSB.Domain;
using TSB.Domain.IdentityContext;
using TSB.Repository.IRepository;
using TSB.Repository.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();



//db connection Step 1
builder.Services.AddDbContext<TSBDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
});

//identity add Step 2
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
    options => options.SignIn.RequireConfirmedAccount = false
)
    .AddEntityFrameworkStores<TSBDbContext>().AddDefaultTokenProviders().AddRoleManager<AppRoleManager>();

;



//Session Configer Step 3
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set the session timeout
                                                    //options.Cookie.HttpOnly = true; // Set additional cookie options if needed
                                                    // Configure other session options as required
});



builder.Services.AddDistributedMemoryCache();


//IdentityServiceConfiguration.ConfigureIdentityServices(builder.Services);



builder.Services.AddScoped<IUserRepository, UserRepository>();













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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
