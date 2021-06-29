using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IOA.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////注册EncodingProvider实现对中文编码的支持
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //Func<string, LogLevel, bool> filter = (category, level) => true;

            //ILoggerFactory loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new ConsoleLoggerProvider());
            //loggerFactory.AddProvider(new DebugLoggerProvider());
            //ILogger logger = loggerFactory.CreateLogger("App");
            //int eventId = 3721;

            //logger.LogInformation(eventId, "升级到最新版本({version})", "1.0.0.rc2");
            //logger.LogWarning(eventId, "并发量接近上限({maximum}) ", 200);
            //logger.LogError(eventId, "数据库连接失败(数据库：{Database}，用户名：{User})", "TestDb", "sa");

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
