using Edulingual.Api.Extensions;
using Edulingual.Common.Constants;
using EduLingual.Common.Helper;

var builder = WebApplication.CreateBuilder(args);

DatabaseHelper.InitConfiguration(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomSwagger(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(CorsConstants.APP_CORS_POLICY);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
DatabaseHelper.ExecuteDbUp(AppDomain.CurrentDomain.FriendlyName, DatabaseConstants.POSTGRESQL_NAME);
app.Run();
