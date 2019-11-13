using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public class AddBufferCommand : BufferCommandBase
    {
        public AddBufferCommand(ICommand command, byte tid, byte flags = 0b00000001)
            : base(BufferCommandCode.Add, flags, tid, command)
        {

        }
    }
}
