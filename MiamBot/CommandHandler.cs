﻿using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using MiamBot.Enums;

namespace MiamBot {
    public class CommandHandler {

        private DiscordSocketClient _client;
        private CommandService _service;
        public CommandHandler(DiscordSocketClient client) {
            _client = client;
            _service = new CommandService();
            _service.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            _client.MessageReceived += HandleCommandAsync;
        }

        private async Task HandleCommandAsync(SocketMessage s) {
            var msg = s as SocketUserMessage;

            if(msg == null) {
                return;
            }

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
                
            if(msg.HasStringPrefix("-", ref argPos)) {
                var result = await _service.ExecuteAsync(context, argPos, null);
                if(!result.IsSuccess && result.Error != CommandError.UnknownCommand && result.Error != CommandError.ParseFailed && result.Error != CommandError.BadArgCount) {
                    Console.WriteLine(result.ErrorReason);
                }

                if (result.Error == CommandError.ParseFailed) {
                    if(context.Message.ToString().Contains("ticket") && !context.Message.ToString().Contains("new") && !context.Message.ToString().Contains("remove") && !context.Message.ToString().Contains("view")) {

                        if(context.Message.ToString().Length < 2)
                        {
                            Console.WriteLine("Too short!");
                        }

                        EmbedBuilder Embed = new EmbedBuilder();
                        Embed.WithColor(122, 40, 200);

                        Embed.WithTitle("Invalid Argument");

                        Embed.WithDescription("Valid arguments: " + getTicketActions());

                        await context.Channel.SendMessageAsync(context.User.Mention, false, Embed.Build());
                    }
                } else if (result.Error == CommandError.BadArgCount) {
                    if(context.Message.ToString().Contains("ticket")) {

                        EmbedBuilder Embed = new EmbedBuilder();
                        Embed.WithColor(122, 40, 200);

                        Embed.WithTitle("Not enough arguments!");
                        Embed.WithDescription (
                                "\n **Ticket Commands**" + "\n" +
                                "-ticket new <text to include> | Creates a new Ticket for staff to view" + "\n" +
                                "-ticket list | See your currently open ticket requests, or cancel them**" + "\n" +
                                "-ticket view | Admin command to manage current tickets" + "\n"
                            );

                        await context.Channel.SendMessageAsync(context.User.Mention, false, Embed.Build());
                    }
                }
            }
        }

        private string getTicketActions() {

            List<String> _validActions = new List<String>();

            foreach (TicketAction action in Enum.GetValues(typeof(TicketAction)))  {
                _validActions.Add(action.ToString().ToLower());
            }

            return String.Join(", ", _validActions);
        }
    }
}
