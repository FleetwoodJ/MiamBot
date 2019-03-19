using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MiamiBot.Modules {
    public class Help : ModuleBase<SocketCommandContext> {

        [Command("help")]
        public async Task helpTsk() {

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(122, 40, 200);
            Embed.WithDescription("\n" + "**-ticket new <text to include> | Creates a new Ticket for staff to view" + "\n" + "-ticket list | See your currently open ticket requests, or cancel them**");

            await Context.Channel.SendMessageAsync(Context.User.Mention + " Ticket-Bot Help Commands", false, Embed.Build());
        }
    }
}
