﻿using Microsoft.Extensions.DependencyInjection.Extensions;

using Sqlist.NET.Tools;
using Sqlist.NET.Tools.Commands;
using Sqlist.NET.Tools.Handlers;
using Sqlist.NET.Tools.Infrastructure;
using Sqlist.NET.Tools.Logging;

using ExecutionContext = Sqlist.NET.Tools.Infrastructure.ExecutionContext;

namespace Microsoft.Extensions.DependencyInjection;

internal static class ServiceCollectionExtensions
{
    public static void AddSqlistTools(this IServiceCollection services)
    {
        services.AddCommonServices();
        services.TryAddSingleton<IApplicationExecutor, EmbeddedAppExecutor>();
    }

    public static void AddCommonServices(this IServiceCollection services)
    {
        services.TryAddSingleton<IAuditor, Auditor>();
#if DEBUG
        services.TryAddSingleton<TestHandler>();
        services.TryAddSingleton<TestCommand>();
#endif
        services.TryAddSingleton<MigrationHandler>();
        services.TryAddSingleton<MigrationCommand>();

        services.TryAddSingleton<IProcessManager, ProcessManager>();
        services.TryAddSingleton<ICommandTransmitter, CommandTransmitter>();
        services.TryAddSingleton<ICommandInitializer, CommandInitializer>();
        services.TryAddSingleton<IExecutionContext, ExecutionContext>();

        services.AddHostedService<CommandHandlerService>();
    }

    public static void RemoveServices<T>(this IServiceCollection services)
    {
        var descriptors = services.Where(d => d.ServiceType == typeof(T)).ToList();
        foreach (var descriptor in descriptors)
        {
            services.Remove(descriptor);
        }
    }

}
