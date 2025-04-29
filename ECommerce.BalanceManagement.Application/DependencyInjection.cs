using ECommerce.BalanceManagement.Application.Common.Interfaces;
using ECommerce.BalanceManagement.Application.Common.Models.Audit;
using ECommerce.BalanceManagement.Application.Common.Behaviours;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ECommerce.BalanceManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddScoped<IAuditLogService, AuditLogService>();

        return services;
    }
}