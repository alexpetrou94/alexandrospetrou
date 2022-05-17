using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.HttpOverrides;
using alexandrospetrou.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ChatService>();
builder.Services.AddSingleton<TelegramBotService>(
    s => new TelegramBotService(
        builder.Configuration["telegram-apikey"],
        builder.Configuration["telegram-chatid"]
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    //see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} 

else {
    
}

//app.UseHttpsRedirection(); 
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//allow for it to work on the Raspberry Pi4 with NGinX
// app.UseForwardedHeaders(new ForwardedHeadersOptions {
//     ForwardedHeaders = ForwardedHeaders.All
// });

app.Run();
