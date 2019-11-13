using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public class PrintStringCommand : CommandBase<string>
    {
        public PrintStringCommand(string data)
            : base(Command.PrintString, data)
        {

        }

        protected override byte[] ConvertDataToByteArray()
        {
            var result = Encoding.ASCII.GetBytes(this.Data);

            return result;
        }
    }
}
