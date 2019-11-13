using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public static class SymbolV2
    {
        public static byte STX => 0x02;
        public static byte ETX => 0x03;
        public static byte EOT => 0x04;
        public static byte ENQ => 0x05;
        public static byte ACK => 0x06;
        public static byte DLE => 0x10;
        public static byte NAK => 0x15;
    }
}
