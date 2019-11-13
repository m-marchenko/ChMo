using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using CheckMobile.FiscalPrintingService.Commands;
using Microsoft.Extensions.Options;

namespace CheckMobile.FiscalPrintingService.Transport
{
    public class FiscalPrinterTransceiver : IFiscalPrinterTransport
    {
        private readonly string _address;
        private readonly int _port;
        private readonly TcpClient _tcpClient;

        private NetworkStream _stream;

        /// <summary>
        /// .ctor 
        /// </summary>
        /// <param name="address">printer ip address</param>
        /// <param name="port">printer port</param>
        public FiscalPrinterTransceiver(IOptions<FiscalPrinterSettings> options)
        {
            _address = options.Value.Address;
            _port = options.Value.Port;

            _tcpClient = new TcpClient(_address, _port);

            _stream = _tcpClient.GetStream();

            //Task.Factory.StartNew(async () => await ListenToPrinter());
        }

        public event EventHandler<MessageReceivedArgs> MessageReceived;
        
        public Task SendPackage(byte [] buffer)
        {            
            _stream.Write(buffer, 0, buffer.Length);
            return Task.CompletedTask;
        }

        public Task ListenToPrinter()
        {
            while(true)
            {
                try
                {
                    var buffer = new byte[1024];
                    var length = _stream.Read(buffer, 0, buffer.Length);

                    if (length == 0)
                    {
                        continue;
                    }

                    OnMesageReceived(new MessageReceivedArgs(buffer.Take(length).ToArray()));
                }
                catch (Exception err)
                {
                    // вероятно, надо переподключиться
                }
            }
        }
    
        private void OnMesageReceived(MessageReceivedArgs args)
        {
            MessageReceived?.Invoke(this, args);
        }
    }
}
