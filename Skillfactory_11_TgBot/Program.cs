using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using static Skillfactory_11_TgBot.Bot;
using Skillfactory_11_TgBot.Controllers;
using Skillfactory_11_TgBot.Services;
using Skillfactory_11_TgBot.Configuration;

namespace Skillfactory_11_TgBot
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
            AppSettings appSettings = BuildAppSettings();
            services.AddSingleton(BuildAppSettings());

            services.AddSingleton<IStorage, MemoryStorage>();
            // Подключаем контроллеры сообщений и кнопок
            services.AddTransient<DefaultMessageController>();
            services.AddTransient<VoiceMessageController>();
            services.AddTransient<TextMessageController>();
            services.AddTransient<InlineKeyboardController>();
            // Регистрируем объект TelegramBotClient c токеном подключения
            services.AddSingleton<ITelegramBotClient>(provider => new TelegramBotClient("5850721035:AAHImhMDRWIVpTQKvlzMPl7K9RzAmgz_Cxo"));
            // Регистрируем постоянно активный сервис бота
            services.AddHostedService<Bot>();
            services.AddSingleton<IFileHandler, AudioFileHandler>();

            static AppSettings BuildAppSettings()
            {
                return new AppSettings()
                {
                    DownloadsFolder = "C:\\Users\\Toshka\\Downloads",
                    BotToken = "5850721035:AAHImhMDRWIVpTQKvlzMPl7K9RzAmgz_Cxo",
                    AudioFileName = "audio",
                    InputAudioFormat = "ogg",
                };
            }
        }

        static AppSettings BuildAppSettings()
        {
            return new AppSettings()
            {
                DownloadsFolder = "C:\\Users\\Toshka\\Downloads",
                BotToken = "5850721035:AAHImhMDRWIVpTQKvlzMPl7K9RzAmgz_Cxo",
                AudioFileName = "audio",
                InputAudioFormat = "ogg",
                OutputAudioFormat = "wav", // Новое поле
            };
        }
    }
}