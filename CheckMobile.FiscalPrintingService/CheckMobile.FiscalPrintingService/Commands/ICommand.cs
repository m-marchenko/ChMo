using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public interface ICommand
    {
        byte[] GetCommandBody();
    }
}
