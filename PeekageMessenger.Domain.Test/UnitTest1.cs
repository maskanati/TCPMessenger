using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using NSubstitute;
using PeekageMessenger.Domain.RequestStrategies;
using PeekageMessenger.Framework;
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

            var client = new ClientModel(new TcpClient(),new ResponseMessageFactory() );
            var result=await client.SendAsync(new HelloRequestStrategy());
            Assert.Equal("Hi",result.Message);
        }
    }

    public class FakeTcpClientFactory
    {
        public static TcpClient Create()
        {
            return new TcpClient();
        }
    }
}
