using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args).Inject();
builder.Host.UseSerilogDefault();
var app = builder.Build();
app.Run();