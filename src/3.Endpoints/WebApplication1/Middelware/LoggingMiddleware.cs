using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        var requestBody = await ReadRequestBody(request);
        Log.Information("درخواست دریافت شد | روش: {Method} | مسیر: {Path} | بدنه: {Body}",
        request.Method, request.Path, requestBody);

    
        await _next(context);
        stopwatch.Stop();

       
        var response = context.Response;
        Log.Information("پاسخ ارسال شد | وضعیت: {StatusCode} | زمان پردازش: {ElapsedMilliseconds} میلی‌ثانیه",
        response.StatusCode, stopwatch.ElapsedMilliseconds);
    }

    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        if (request.ContentLength == null || request.ContentLength == 0)
            return "بدون محتوا";

        request.EnableBuffering();
        using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        request.Body.Position = 0; 
        return body;
    }
}