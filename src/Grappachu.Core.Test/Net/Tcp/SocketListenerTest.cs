using System;
using System.Net;
using System.Net.Sockets;
using Grappachu.Core.Net.Tcp;
using NUnit.Framework;
using SharpTestsEx;

namespace Grappachu.Core.Test.Net.Tcp
{
    [TestFixture]
    class SocketListenerTest
    {
        private const int PORT = 11342;
        private SocketListener _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new SocketListener(IPAddress.Any, PORT);
        }

        [TearDown]
        public void TearDown()
        {
            if (_sut != null)
            {
                _sut.Dispose();
            }
            _sut = null;
        }


        [Test]
        public void Start_Should_Change_Listening_State()
        {
            _sut.IsListening.Should().Be.False();

            _sut.Start();

            _sut.IsListening.Should().Be.True();
        }

        [Test]
        public void Stop_Should_Change_Listening_State()
        {
            _sut.Start();
            _sut.IsListening.Should().Be.True();

            _sut.Stop();

            _sut.IsListening.Should().Be.False();
        }

        [Test]
        public void Start_Should_begin_listening()
        {
            const string message = "TestMessage";
            bool eventRaised = false;
            _sut.MessageReceived += (sender, args) =>
            {
                eventRaised = true;
            };

            _sut.Start();

            SendMessage(message);
            eventRaised.Should().Be.True();
        }

        [Test]
        public void Stop_Should_end_listening()
        {
            const string message = "This is a test message";
            bool eventRaised = false;
            _sut.MessageReceived += (sender, args) =>
            {
                eventRaised = true;
                Assert.Fail();
            };
            _sut.Start();

            _sut.Stop();
            this.Executing(x => x.SendMessage(message)).Throws<SocketException>();

            eventRaised.Should().Be.False();
        }

        [TestCase("")]
        [TestCase("Message 1")]
        [TestCase("Message 2")]
        public void Should_Notify_Messages_on_a_socket(string message)
        {
            bool eventRaised = false;
            _sut.MessageReceived += (sender, args) =>
            {
                args.Message.Should().Be.EqualTo(message);
                eventRaised = true;
            };

            _sut.Start();
            var response = SendMessage(message);

            eventRaised.Should().Be.True();
            response.Should().Be.NullOrEmpty();
        }



         

        private string SendMessage(string message)
        {
            using (var client = new TcpClient("localhost", PORT))
            {
                var data = System.Text.Encoding.ASCII.GetBytes(message);
                String responseData;
                using (var stream = client.GetStream())
                {
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Sent: {0}", message);
                    data = new Byte[256];
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Received: {0}", responseData);
                }
                return responseData;
            }
        }

    }
}
