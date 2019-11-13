using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public enum BufferCommandCode : byte
    {
        Add     = 0xC1,
        Ack     = 0xC2,
        Req     = 0xC3,
        Abort   = 0xC4,
        AckAdd  = 0xC5
    }

    public enum JobStatus : byte
    {
        Pending     = 0xA1,
        InProgress  = 0xA2,
        Result      = 0xA3,
        Error       = 0xA4,
        Stopped     = 0xA5,
        AsyncResult = 0xA6,
        AsyncError  = 0xA7,
        Waiting     = 0xA8
    }

    public enum JobError : byte
    {
        Overflow        = 0xB1,
        AlreadyExists   = 0xB2,
        NotFound        = 0xB3,
        IllegalValue    = 0xB4
    }
}
