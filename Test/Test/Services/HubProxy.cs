using System;
using System.Collections.Generic;
using Microsoft.AspNet.SignalR.Client;
using Test.Models;
using Test.Utils;

namespace Test
{
    public partial class HubProxy
    {
        private readonly HubConnection _connection;
        public readonly IHubProxy ChatHubProxy;

        public HubProxy()
        {
            var queryStringData = new Dictionary<string, string>
            {
                { "hardware_id", GlobalVariable.HardwareId },
                { "connection_type", "1" }
            };
            _connection = new HubConnection(GlobalVariable.SignalServer, queryStringData);
            ChatHubProxy = _connection.CreateHubProxy("notificationHub");
            ChatHubProxy.On<MessageModel>("broadcastMessage", message => Logger.Info("SendMessageToEtp", $"ID: {message.Id}. Message {message.Message}"));
        }

        public void SendMessage(MessageModel message)
        {
            if (ChatHubProxy != null)
            {
                try
                {
                    ChatHubProxy.Invoke("SendMessageToEtp", GlobalVariable.HardwareId, message);
                }
                catch (Exception e)
                {
                    Logger.Info("SendMessageToEtp", e.StackTrace);
                }
            }
            else
            {
                Logger.Info("SendMessage", "HubProxy not started.");
            }
        }

        public async void StartConnectionAsync() {
            await _connection.Start();
            Logger.Info("StartConnectionAsync", $"HubProxy connected, connection ID: {_connection.ConnectionId}");
        }

        public void StopConnection()
        {
            try
            {
                _connection.Stop();
            }
            catch (Exception e)
            {
                Logger.Info("StopConnection", e.StackTrace);
            }
        }
    }
}
