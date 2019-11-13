using CheckMobile.FiscalPrintingService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Transport
{
    public interface IFiscalPrinterTransport
    {
        Task SendPackage(byte [] buffer);

        event EventHandler<MessageReceivedArgs> MessageReceived;

        Task ListenToPrinter();
    }

    public class MessageReceivedArgs
    {
        public MessageReceivedArgs(byte [] body)
        {
            Body = body;
        }

        public byte [] Body { get; private set; }
    }
}
