using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public static class Command
    {
        public static byte PrintString => 0x4C;
        public static byte OpenShift => 0x9A;
        public static byte OpenSlip => 0x92;
        public static byte CancelSlip => 0x59;
        public static byte PayMoney => 0x49;
        public static byte ReturnMoney => 0x4F;
        public static byte Total => 0x99;
        public static byte CloseSlip => 0x4A;
        public static byte AddPosition => 0xE6;
        public static byte RequestStatus => 0x3F;
    }
}
