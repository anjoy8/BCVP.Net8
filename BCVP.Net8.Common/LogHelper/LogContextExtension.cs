using Serilog.Context;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace BCVP.Net8.Common.LogHelper;

public class LogContextExtension : IDisposable
{
    private readonly Stack<IDisposable> _disposableStack = new Stack<IDisposable>();

    public static LogContextExtension Create => new();
    public static readonly string LogSource = "LogSource";
    public static readonly string AopSql = "AopSql";
    public static readonly string FileMessageTemplate = "{NewLine}Date：{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}LogLevel：{Level}{NewLine}Message：{Message}{NewLine}{Exception}" + new string('-', 100);

    public void AddStock(IDisposable disposable)
    {
        _disposableStack.Push(disposable);
    }

    public IDisposable SqlAopPushProperty(ISqlSugarClient db)
    {
        AddStock(LogContext.PushProperty(LogSource, AopSql));

        return this;
    }


    public void Dispose()
    {
        while (_disposableStack.Count > 0)
        {
            _disposableStack.Pop().Dispose();
        }
    }
}