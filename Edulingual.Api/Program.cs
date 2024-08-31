using Edulingual.Api.Extensions;
using Edulingual.Caching.Extensions;
using Edulingual.Common.Constants;
using Edulingual.Service.Extensions;
using Edulingual.Common.Helper;
using Serilog;
using OfficeOpenXml;

var builder = WebApplication.CreateBuilder(args);

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
DatabaseHelper.InitConfiguration(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.RegisterCaching(builder.Configuration);
builder.AddSerilog();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
app.UseCors(CorsConstants.APP_CORS_POLICY);
app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
DatabaseHelper.ExecuteDbUp(AppDomain.CurrentDomain.FriendlyName, DatabaseConstants.POSTGRESQL_NAME);
app.Run();
