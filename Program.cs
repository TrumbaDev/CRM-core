using CrmCore.Core.Application.User.Commands.CreateUser;
using CrmCore.Core.Application.User.Queries.GetUserById;
using CrmCore.Core.Domain.User.Repositories;
using CrmCore.Infrastructure.Data.User;
using CrmCore.Infrastructure.Data.User.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<CreateUserCommandHandler>();
builder.Services.AddScoped<GetUserByIdQueryHandler>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();