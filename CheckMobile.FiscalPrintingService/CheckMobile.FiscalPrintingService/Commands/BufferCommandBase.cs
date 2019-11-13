using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public abstract class BufferCommandBase : ICommand
    {
        public BufferCommandBase(BufferCommandCode code, byte flags, byte tid, ICommand command )
        {
            Code = code;
            Flags = flags;
            Tid = tid;
            Command = command;
        }

        public BufferCommandCode Code { get; private set; }

        public byte Flags { get; private set; }

        public byte Tid { get; private set; }

        public ICommand Command { get; private set; }

        public byte[] GetCommandBody()
        {
            var result = (new byte[] { (byte)Code, Flags, Tid })
                .Concat(Command.GetCommandBody())
                .ToArray();

            return result;
        }
    }
}
