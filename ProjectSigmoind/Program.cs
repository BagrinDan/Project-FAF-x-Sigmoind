using ProjectSigmoind.BussinesLayer.AI.Entity;
using ProjectSigmoind.BussinesLayer.AI.Interface;

var builder = WebApplication.CreateBuilder(args);

// Подключение MVC-контроллеров и Razor-страниц
builder.Services.AddControllersWithViews();

// Регистрация сервиса для общения с AI (MentorGPT)
builder.Services.AddHttpClient<IMentorGPT, MentorGPT>();

var app = builder.Build();

// Конфигурация пайплайна
if(!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Настройка маршрутизации
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
