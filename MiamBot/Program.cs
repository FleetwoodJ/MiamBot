using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace MiamBot { 
    class Program {

        private DiscordSocketClient _client;
        private CommandService _commands;
        private CommandHandler _handler;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync() {
            _client = new DiscordSocketClient(new DiscordSocketConfig {
                LogLevel = LogSeverity.Debug,
            });

            _commands = new CommandService(new CommandServiceConfig {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug,
            });

            _client.MessageReceived += Client_MessageReceived;
            _handler = new CommandHandler(_client);

            await _client.StartAsync();

            

            _client.Ready += Client_Ready;
            _client.Log += Client_Log;

            await _client.LoginAsync(TokenType.Bot, "NTU1OTMyOTMyMzA1MzIyMDAw.D2yZpA.WJoflFa_Do0W0p1wUMTcxgMhPN8", true);

            await Task.Delay(-1);
        }

        private async Task Client_Log(LogMessage Message) {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
        }

        private async Task Client_Ready() {
            await _client.SetGameAsync("www.miami-liferp.com");
        }

        private async Task Client_MessageReceived(SocketMessage arg) {
            
        }
    }
}
