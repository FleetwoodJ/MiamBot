using Discord;
using Discord.Commands;
using MiamBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiamBot.Modules {
    public class Ticket : ModuleBase<SocketCommandContext> {

        [Command("ticket")]
        public async Task ticketTsk(TicketAction ticketAction, [Remainder] string input = "") {

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(122, 40, 200);

            switch(ticketAction) {
                case TicketAction.Add:
                    Console.WriteLine("added");
                    break;
                default:
                    Console.WriteLine("Invalid!");
                    break;

            }

            if(input.Length < 1) {
                Embed.WithDescription
                    (
                        "\n **Ticket Commands**" + "\n" +
                        "-ticket new <text to include> | Creates a new Ticket for staff to view" + "\n" +
                        "-ticket list | See your currently open ticket requests, or cancel them**" + "\n" +
                        "-ticket view | Admin command to manage current tickets" + "\n"
                    );

                await Context.Channel.SendMessageAsync(Context.User.Mention + " Not enough arguments", false, Embed.Build());

            } else {
                await newTicket(Context, input);
            }
        }

        public async Task newTicket(SocketCommandContext context, string input) {
            await Context.Channel.SendMessageAsync(Context.User.Mention + " Ticket Created with text: " + input, false);
        }
    }
}
