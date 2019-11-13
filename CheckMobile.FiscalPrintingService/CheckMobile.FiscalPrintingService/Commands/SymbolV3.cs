using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public static class SymbolV3
    {
        public static byte STX => 0xFE;
        public static byte ESC => 0xFD;
        public static byte TSTX => 0xEE;
        public static byte TESC => 0xED;
    }
}
