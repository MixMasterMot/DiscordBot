﻿using System;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Discord;
//for windows 7 support
using Discord.Net.Providers.WS4Net;

namespace MotBot1
{
    class Program
    {
        static void Main(string[] args) => new Program().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;
        private IServiceProvider _services;

        public async Task RunBotAsync()
        {
            //for windows 8 + support
            //_client = new DiscordSocketClient();
            //for windows 7 support
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                WebSocketProvider = WS4NetProvider.Instance,
            });

            _commands = new CommandService();

            _services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton(_commands)
                .BuildServiceProvider();
            //string botToken = "NDExNTIwNjg1NDIxMjMyMTM4.DV9nvA.BJm9pYyMOyqHFM0kZEjyNxUeIF8";

            //event subscriptions
            _client.Log += Log;
            _client.UserJoined += AnnounceUserJoined;
            await RegisterCommandsAsync();

            string botToken = Modules.Config.GetValue("botToken");
            await _client.LoginAsync(Discord.TokenType.Bot, botToken);

            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task AnnounceUserJoined(SocketGuildUser user)
        {
            var guild = user.Guild;
            var channel = guild.DefaultChannel;
            await channel.SendMessageAsync("Welcome, " + user.Mention);
        }

        private Task Log(LogMessage arg)
        {
            Console.WriteLine(arg);
            return Task.FromResult(0);
        }

        public async Task RegisterCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly());

        }

        private async Task HandleCommandAsync(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            if (message == null || message.Author.IsBot)
            {
                return;
            }
            int argPos = 0;
            string cmdPrefix = Modules.Config.GetValue("cmdPrefix1");
            if (message.HasStringPrefix(cmdPrefix, ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos))
            {
                var contex = new SocketCommandContext(_client, message);
                var ressult = await _commands.ExecuteAsync(contex, argPos, _services);
                if (!ressult.IsSuccess)
                {
                    Console.WriteLine(ressult.ErrorReason);
                }

            }
        }
    }
}
