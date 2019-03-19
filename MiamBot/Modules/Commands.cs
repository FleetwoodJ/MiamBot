using Discord;
using Discord.Commands;
using MiamBot.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiamBot.Modules {
    public class Commands : ModuleBase<SocketCommandContext> {


        [Command("help")]
        public async Task helpTsk()
        {

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(122, 40, 200);
            Embed.WithDescription

                (
                    "\n **Ticket Commands**" + "\n" +
                    "-ticket new <text to include> | Creates a new Ticket for staff to view" + "\n" +
                    "-ticket list | See your currently open ticket requests, or cancel them**" + "\n" +
                    "-ticket view | Admin command to manage current tickets" + "\n"
                );

            await Context.Channel.SendMessageAsync(Context.User.Mention + " How to use Miam-Bot", false, Embed.Build());
        }

        [Command("ticket")]
        public async Task ticketTsk(TicketAction ticketAction, [Remainder] string input = "") {

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(122, 40, 200);

            if (input.Length < 4 && ticketAction == TicketAction.New) {

                Embed.WithTitle("Ticket message too short!");
                Embed.WithDescription("Your ticket message wasn't long enough, it \n must be at least 4 characters");

                await Context.Channel.SendMessageAsync(Context.User.Mention, false, Embed.Build());

                return;
            }

            switch(ticketAction) {
                case TicketAction.New:
                    await newTicket(Context, input);
                    break;
                default:
                    Console.WriteLine("Invalid!");
                    break;

            }
        }

        public async Task newTicket(SocketCommandContext context, string input) {
            await Context.Channel.SendMessageAsync(Context.User.Mention + " Ticket Created with text: " + input, false);
        }
    }
}
