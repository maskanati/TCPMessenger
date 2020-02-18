using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Extensions;
using PeekageMessenger.Application;
using PeekageMessenger.Application.Contract;
using PeekageMessenger.Domain.Contract;
using PeekageMessenger.Domain.Contract.Requests;
using PeekageMessenger.Domain.RequestStrategies;
using PeekageMessenger.Framework;
using PeekageMessenger.Framework.Core;
using PeekageMessenger.Framework.Core.Extensions;
using PeekageMessenger.Infrastructure.TCP;
using Xunit;

namespace PeekageMessenger.Domain.Test
{

    public class HelloMessageTests
    {
        [Fact]
        public async Task Should_return_Hi_when_client_send_hello()
        {
            var tcpClient = FakeTcpClientFactory.Create();


            var client = new ClientModel(tcpClient, new ResponseMessageFactory());
            var request=new RequestFactory().Create(client, "Hello");

            var taskRequest = Task.Run( () => client.SendAsync(request).Result);
             tcpClient.ReadMessageAsync();
             var result= taskRequest.Result.Message;

             Assert.Equal("Hi", result);
        }
    }

    public class FakeTcpClientFactory
    {
        public static IConnectionClient Create()
        {
            var _appClient = Substitute.For<IConnectionClient>();
            _appClient.ReadMessageAsync().Returns(Task.FromResult("Hi"));
            return _appClient;
        }
    }
}
