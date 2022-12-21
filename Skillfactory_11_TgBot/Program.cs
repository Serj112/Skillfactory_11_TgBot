using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using static VoiceTexterBot.Bot;

namespace VoiceTexterBot
{
    public class Program
    {
        public static async Task Main()
        {
            Console.OutputEncoding = Encoding.Unicode;

            // Объект, отвечающий за постоянный жизненный цикл приложения
            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) => ConfigureServices(services)) // Задаем конфигурацию
                .UseConsoleLifetime() // Позволяет поддерживать приложение активным в консоли
                .Build(); // Собираем

            Console.WriteLine("Сервис запущен");
            // Запускаем сервис
            await host.RunAsync();
            Console.WriteLine("Сервис остановлен");
        }

        static void ConfigureServices(IServiceCollection services)
        {
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5850721035:AAHImhMDRWIVpTQKvlzMPl7K9RzAmgz_Cxo"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
        }

        public void Configure(IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Log.LoggerFactory = loggerFactory;
        }
        private readonly ILogger _logger = Log.CreateLogger<Program>();

        public void CustomClass()
        {
            _logger.LogInformation("test message");
        }

    }
}