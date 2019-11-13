using CheckMobile.FiscalPrintingService.CommandProcessing;
using CheckMobile.FiscalPrintingService.Commands;
using CheckMobile.FiscalPrintingService.Transport;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Tests
{
    [TestClass]
    public class CommandFormatterTest
    {
        [TestMethod]
        public void CommandFormatter_CreatePackage()
        {
            var command = new PrintStringCommand("123");
            var commandPwd = new CommandPasswordWrapper(command, new byte[] { 0x00, 0x00 });
            var bufferCommand = new AddBufferCommand(commandPwd, 0xFA, 0x03);

            var formatter = new CommandFormatter(bufferCommand);

            var result = formatter.CreatePackage();
        }

        [TestMethod]
        public void CommandFormatter_CRC()
        {
            //var buffer = new byte[] { 0x2b, 0xc1, 0x00, 0x15, 0x00,0x00, 0x4c, 0x31, 0x32, 0x33 };

            var buffer = new byte[] { 0x1b, 0xc1, 0x00, 0x0d, 0x00, 0x00, 0x4c, 0x31, 0x32, 0x33 };

            var result = CommandFormatter.CalculateCRC8(buffer);

            Assert.AreEqual(0x4C, result);

            buffer = new byte[] { 0x2d, 0xc1, 0x01, 0x16, 0x00, 0x00, 0x3f };

            result = CommandFormatter.CalculateCRC8(buffer);

            Assert.AreEqual(0x4A, result);
        }

        [TestMethod]
        public void CommandFormatter_Common()
        {
            var command = new PrintStringCommand("123");

            var formatter = new CommandFormatter(command);

            var result = formatter.CreatePackage();

        }

/*
        [TestMethod]
        public async Task SendCommand()
        {
            var command = new RequestStatusCommand();
            var commandPwd = new CommandPasswordWrapper(command, new byte[] { 0x00, 0x00 });
            var bufferCommand = new AddBufferCommand(commandPwd, 0x01, 0x01);

            var formatter = new CommandFormatter(bufferCommand);

            var buffer = formatter.CreatePackage();

            var transceiver = new FiscalPrinterTransceiver("192.168.42.131", 5555);

            transceiver.MessageReceived += (s, e) => {
                var hex = string.Join(" ", e.Body.Select(b => b.ToString("X2")));
                Assert.AreEqual(0xFE, e.Body[0]);
            };

            Task.Factory.StartNew(async () => await transceiver.ListenToPrinter());

            await transceiver.SendPackage(buffer);

        }
*/
    }
}
