using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using MemeJudger.Services;

namespace MemeJudger.Modules
{
    /// <summary>
    /// Role group, regroups every command related to the setup or getter of the "not funny" role
    /// </summary>
    [Group("Role")]
    public class RoleModule: ModuleBase<SocketCommandContext>
    {
        /// <summary>
        /// Set command for assigning a new role as "not funny" role
        /// </summary>
        /// <param name="role">the new role to assign as "not funny" role</param>
        /// <returns></returns>
        [Command("set")]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task Set(IRole role)
        {
            BotProperties.SetRole(role);
            await ReplyAsync("✅ Successfully set " + role + " as sentenced Role !");
        }
        
        /// <summary>
        /// Get command to see the current role that is assigned each time a user is considered a not funny
        /// </summary>
        /// <returns></returns>
        [Command("get")]
        public async Task Get()
        {
            await ReplyAsync("The current sentenced Role is following: " +BotProperties.GetRole());
        }
    }
}