using Repository;
using Repository.Implement;
using Repository.Interface;
using SinalRAssignment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSession();
builder.Services.AddSignalR();

builder.Services.AddDbContext<SignalRDBContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IChatGroupRepository, ChatGroupRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseEndpoints(endpoints =>
{
	endpoints.MapHub<ChatHub>("/chathub");
});

app.UseAuthorization();

app.MapRazorPages();

app.Run();
