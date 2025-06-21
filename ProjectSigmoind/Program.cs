using ProjectSigmoind.BussinesLayer.AI.Entity;
using ProjectSigmoind.BussinesLayer.AI.Interface;

var builder = WebApplication.CreateBuilder(args);

// ����������� MVC-������������ � Razor-�������
builder.Services.AddControllersWithViews();

// ����������� ������� ��� ������� � AI (MentorGPT)
builder.Services.AddHttpClient<IMentorGPT, MentorGPT>();

var app = builder.Build();

// ������������ ���������
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// ��������� �������������
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
