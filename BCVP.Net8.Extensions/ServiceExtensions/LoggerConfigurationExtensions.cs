using BCVP.Net8.Common.LogHelper;
using BCVP.Net8.Extensions.ServiceExtensions;
using Blog.Core.Common;
using Serilog;
using Serilog.Events;
using Serilog.Filters;
using SqlSugar;

namespace BCVP.Net8.Extensions.ServiceExtensions;

public static class LoggerConfigurationExtensions
{
    public static LoggerConfiguration WriteToConsole(this LoggerConfiguration loggerConfiguration)
    {
        //输出普通日志
        loggerConfiguration = loggerConfiguration.WriteTo.Logger(lg =>
            lg
            .Filter.ByIncludingOnly(WithPropertyExt<string>(LogContextExtension.LogSource, s => !LogContextExtension.AopSql.Equals(s)))
            .WriteTo.Console());

        //输出SQL
        loggerConfiguration = loggerConfiguration.WriteTo.Logger(lg =>
            lg
            .Filter.ByIncludingOnly(Matching.WithProperty<string>(LogContextExtension.LogSource, s => LogContextExtension.AopSql.Equals(s)))
            .WriteTo.Console());

        return loggerConfiguration;
    }

    public static LoggerConfiguration WriteToFile(this LoggerConfiguration loggerConfiguration)
    {
        //输出普通日志
        loggerConfiguration = loggerConfiguration.WriteTo.Logger(lg =>
            lg
            .Filter.ByIncludingOnly(WithPropertyExt<string>(LogContextExtension.LogSource, s => !LogContextExtension.AopSql.Equals(s)))
            .WriteTo.Async(s => s.File(Path.Combine("Logs", @"Log.txt"), rollingInterval: RollingInterval.Day,
                outputTemplate: LogContextExtension.FileMessageTemplate, retainedFileCountLimit: 31)));
        //输出SQL
        loggerConfiguration = loggerConfiguration.WriteTo.Logger(lg =>
            lg
            .Filter.ByIncludingOnly(Matching.WithProperty<string>(LogContextExtension.LogSource, s => LogContextExtension.AopSql.Equals(s)))
            .WriteTo.Async(s => s.File(Path.Combine("Logs", LogContextExtension.AopSql, @"AopSql.txt"), rollingInterval: RollingInterval.Day,
                    outputTemplate: LogContextExtension.FileMessageTemplate, retainedFileCountLimit: 31)));
        return loggerConfiguration;
    }

    public static Func<LogEvent, bool> WithPropertyExt<T>(string propertyName, Func<T, bool> predicate)
    {
        //如果不包含属性 也认为是true
        return e =>
        {
            if (!e.Properties.TryGetValue(propertyName, out var propertyValue)) return true;

            return propertyValue is ScalarValue { Value: T value } && predicate(value);
        };
    }

}