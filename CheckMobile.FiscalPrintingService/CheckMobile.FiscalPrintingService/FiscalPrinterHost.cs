using CheckMobile.FiscalPrintingService.Transport;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService
{
    public class FiscalPrinterHost : IHostedService
    {
        IFiscalPrinterTransport _transceiver;

        public FiscalPrinterHost(IFiscalPrinterTransport transceiver)
        {
            _transceiver = transceiver;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _transceiver.MessageReceived += (s, e) => {
                var hex = string.Join(" ", e.Body.Select(b => b.ToString("X2")));
                Console.WriteLine(hex);
            };

            await _transceiver.ListenToPrinter();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
