using CrmCore.Core.Application.Task.Commands.CreateTask;
using CrmCore.Core.Domain.Task.Factories;
using CrmCore.Core.Domain.Task.Repositories;
using CrmCore.Core.Domain.User.Factories;
using CrmCore.Core.Domain.User.Repositories;
using CrmCore.Infrastructure.Data.Task;
using CrmCore.Infrastructure.Data.Task.Repositories;
using CrmCore.Infrastructure.Data.User;
using CrmCore.Infrastructure.Data.User.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<UserFactory>();
builder.Services.AddScoped<TaskAggregateFactory>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(CreateTaskCommandHandler).Assembly
    )
);

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