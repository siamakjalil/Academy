using DataLayer.Context;
using DataLayer.Interfaces;
using DataLayer.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add builder.Services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddDbContext<AcademyDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AcademyConnection"));
});

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();


#region --IOC--

builder.Services.AddScoped<IAdmin, AdminRepository>();
builder.Services.AddScoped<ILog, LogRepository>();
builder.Services.AddScoped<IStudentGroup, StudentGroupRepository>();
builder.Services.AddScoped<ISubject, SubjectRepository>();
builder.Services.AddScoped<ISubjectClass, SubjectClassRepository>();
builder.Services.AddScoped<ITeacher, TeacherRepository>();
builder.Services.AddScoped<ITeacherSubjects, TeacherSubjectsRepository>();
builder.Services.AddScoped<IPreSubject, PreSubjectsRepository>();
builder.Services.AddScoped<IPlan, PlanRepository>();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapControllers();

app.Run();
