using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace IOA.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ////ע��EncodingProviderʵ�ֶ����ı����֧��
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            //Func<string, LogLevel, bool> filter = (category, level) => true;

            //ILoggerFactory loggerFactory = new LoggerFactory();
            //loggerFactory.AddProvider(new ConsoleLoggerProvider());
            //loggerFactory.AddProvider(new DebugLoggerProvider());
            //ILogger logger = loggerFactory.CreateLogger("App");
            //int eventId = 3721;

            //logger.LogInformation(eventId, "���������°汾({version})", "1.0.0.rc2");
            //logger.LogWarning(eventId, "�������ӽ�����({maximum}) ", 200);
            //logger.LogError(eventId, "���ݿ�����ʧ��(���ݿ⣺{Database}���û�����{User})", "TestDb", "sa");

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
