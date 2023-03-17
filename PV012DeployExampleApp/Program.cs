var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// корень
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
