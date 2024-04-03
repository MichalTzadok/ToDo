using System.Diagnostics;
using ToDo.Models;

namespace ToDo.Middlewares;

public class logMiddleware
{
    private RequestDelegate next;
    private readonly string logFilePath;
    private User user;


    public logMiddleware(RequestDelegate next, string logFilePath)
    {
        this.next = next;
        this.logFilePath = logFilePath;
    }

    public async Task Invoke(HttpContext c)
    { var action = $"{c.Request.Path}.{c.Request.Method}";
            var sw = new Stopwatch();
            sw.Start();
            WriteLogToFile($"{action} started at {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}");

            await next(c);

            WriteLogToFile( $"{action} ended at {sw.ElapsedMilliseconds} ms. UserId: {c.User?.FindFirst("id")?.Value ?? "unknown"}");
          
    }    


    private void WriteLogToFile(string logMessage)
        {  
         lock (this)
        {
            using (StreamWriter sw = File.AppendText(logFilePath))
            {
                sw.WriteLine(logMessage);
            }
        }
            
        }
}

public static partial class MiddleExtensions
{
    public static IApplicationBuilder UselogMiddleware(this IApplicationBuilder builder, string logFilePath)
    {
        return builder.UseMiddleware<logMiddleware>(logFilePath);
    }
}