using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotBot1.Modules
{
    public class Ping : ModuleBase<SocketCommandContext>
    {
        [Command("Reply")]
        public async Task PingAsync([RemainderAttribute]string reply="Blyat")
        {
            await ReplyAsync(reply);
        }

        [Command("hang")]
        public async Task HangManStart(string ltr ="new")
        {
            if (ltr == "new")
            {
                await ReplyAsync("Hang the hangman", false, HangMan.NewGame());
                return;
            }
            else if (ltr == "hint")
            {
                Tuple<string, Embed> tpl = HangMan.Hint();
                if (tpl.Item1 != "")
                {
                    await ReplyAsync(tpl.Item1);
                }
                else
                {
                    await ReplyAsync("", false, tpl.Item2);
                }
            }
            else if (ltr.Length > 1)
            {
                await ReplyAsync("Only one leter at a time");
            }
            else
            {
                Tuple<string, Embed> tpl = HangMan.HangManWordContains(ltr);
                if (tpl.Item1 != "")
                {
                    await ReplyAsync(tpl.Item1);
                }
                else
                {
                    await ReplyAsync("", false, tpl.Item2);
                }
            }
          
        }

        [Command("help")]
        public async Task GetHelp()
        {
            await ReplyAsync("", false, Help.Helpper());
        }
    }
}
