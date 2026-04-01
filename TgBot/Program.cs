using DataProvider;
using DataProvider.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;


using var cts = new CancellationTokenSource();

var services = new ServiceCollection();
services.AddSingleton<HttpClient>();
services.AddSingleton<ApiClient>();
services.AddSingleton<IProductFactory, ProductFactory>();


var bot = new TelegramBotClient(File.ReadAllText("token.txt").Trim(), cancellationToken:cts.Token);
var me = await bot.GetMe();

bot.OnMessage += BotOnMessage;
bot.OnError += OnError;
bot.OnUpdate += OnUpdate;

async Task OnError(Exception exception, HandleErrorSource source)
{
    Console.WriteLine(exception.Message);
}

Console.ReadLine();
cts.Cancel();
return;

async Task BotOnMessage(Message message, UpdateType type)
{
    if (message.Text is null) return;
    if (message.Type == MessageType.Text)
    {
        if (message.Text.StartsWith("/"))
        {
            if (message.Text == "/start")
            {
                await bot.SendMessage(message.Chat, "Welcome! Pick one direction:", replyMarkup: new InlineKeyboardButton[] {"Left", "Right"});
            }
        }
    }
}

async Task OnUpdate(Update update)
{
    if (update is { CallbackQuery: { } query })
    {
        await bot.AnswerCallbackQuery(query.Id, $"You picked {query.Data}");
        await bot.SendMessage(query.Message!.Chat, $"User {query.From} clicked on {query.Data}");
    }
}