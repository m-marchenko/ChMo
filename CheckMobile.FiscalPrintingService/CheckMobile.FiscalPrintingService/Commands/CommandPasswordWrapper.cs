using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public class CommandPasswordWrapper : ICommand
    {
        private readonly ICommand _command;
        private readonly byte[] _password;

        public CommandPasswordWrapper(ICommand command, byte [] password)
        {
            _command = command;
            _password = password;
        }

        public byte[] GetCommandBody()
        {
            var result = (new byte[] { })
                .Concat(_password)
                .Concat(_command.GetCommandBody())
                .ToArray();

            return result;
        }
    }
}
