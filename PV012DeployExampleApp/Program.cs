using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// ������
app.MapGet("/", async (context) =>
{ 
    var responseData = new {Time = DateTime.Now, Message = "Server is running ..."};
    await context.Response.WriteAsJsonAsync(responseData);
});

// ping
app.MapGet("/ping", async (context) =>
{
    var responseData = new { Time = DateTime.Now, Message = "pong" };
    await context.Response.WriteAsJsonAsync(responseData);
});


app.Run();



TimeSpan remaining;

static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // ��������� ��������
    DateTime otherDate = new DateTime(2023, 5, 30, 10, 0, 0);
    TimeSpan remaining = otherDate - DateTime.Now;

    JObject contect = JObject.Parse(Newtonsoft.Json.JsonConvert.SerializeObject(update));
    Console.WriteLine(contect);
    string otvet = (contect["message"]["from"]["first_name"] + " " + contect["message"]["from"]["last_name"] + "\n�� ��� �� �������� �������� \n" + remaining.Days + " ����\n" + "���\n" +
                      (remaining.Hours + remaining.Days * 24) + " �����\n" + "���\n" +
                      (remaining.Minutes + (remaining.Hours + remaining.Days * 24) * 60) + " �����\n" + "���\n" +
                      (remaining.Seconds + (remaining.Minutes + (remaining.Hours + remaining.Days * 24) * 60) * 60) + " ������");
    if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
    {
        var message = update.Message;
        if (message.Text?.ToLower() == "/time")
        {
            await botClient.SendTextMessageAsync(message.Chat, otvet);
            return;
        }
    }
}

static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    // ��������� ��������
    Console.WriteLine("�������� ������: " + Newtonsoft.Json.JsonConvert.SerializeObject(exception));
}


ITelegramBotClient bot = new TelegramBotClient("6035041242:AAEBzNjMlg-tome33-XFowNTrv8lCPVD08Y");
Console.WriteLine("������� ��� " + bot.GetMeAsync().Result.FirstName);
var cts = new CancellationTokenSource();
var cancellationToken = cts.Token;
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = { }, // receive all update types
};
bot.StartReceiving(
    HandleUpdateAsync,
    HandleErrorAsync,
    receiverOptions,
    cancellationToken
);
Console.ReadLine();
