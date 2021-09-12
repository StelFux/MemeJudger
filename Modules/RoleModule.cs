using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    [Group("Role")]
    public class RoleModule: ModuleBase<SocketCommandContext>
    {
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Set(IRole role)
        {
            BotProperties.SetRole(role);
            await ReplyAsync("✅ Successfully set " + role + " as sentenced Role !");
        }

        [Command("get")]
        public async Task Get()
        {
            await ReplyAsync("The current sentenced Role is following: " +BotProperties.GetRole());
        }
    }
}