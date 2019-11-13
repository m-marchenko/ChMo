using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.Commands
{
    public abstract class CommandBase : ICommand
    {

        protected CommandBase(byte code)
        {
            Code = code;            
        }

        public byte Code { get; protected set; }
        

        public virtual byte[] GetCommandBody()
        {
            var result = new byte[] { Code };

            return result;
        }
        
    }

    public abstract class CommandBase<TData> : CommandBase
    {
        protected CommandBase(byte code, TData data)
            : base(code)
        {
            Code = code;
            Data = data;
        }

        public TData Data { get; private set; }

        public override byte[] GetCommandBody()
        {
            var result = new List<byte>(base.GetCommandBody());

            result.AddRange(ConvertDataToByteArray());

            return result.ToArray();
        }

        protected abstract byte[] ConvertDataToByteArray();

    }
}
