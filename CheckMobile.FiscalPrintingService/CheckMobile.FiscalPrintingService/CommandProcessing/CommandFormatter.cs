using CheckMobile.FiscalPrintingService.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CheckMobile.FiscalPrintingService.CommandProcessing
{
    public class CommandFormatter
    {
        private static byte _commandId = 0x00;

        private readonly ICommand _command;
       
        public CommandFormatter(ICommand command)
        {
            _command = command;            
        }
     

        public byte [] CreatePackage()
        {
            var len = GetLength(_command.GetCommandBody().Length);

            var result = new byte[] { SymbolV3.STX }
                .Concat(len);

            var crcData = (new byte[] { GetNextCommandId() })
                .Concat(_command.GetCommandBody())
                .ToArray();

            var crc = CalculateCRC8(crcData);

            crcData = crcData.Concat(new byte[] { crc }).ToArray();

            crcData = ByteStuff(crcData, 1);

            result = result.Concat(crcData);


            return result.ToArray();
        }
     

        private CommandFormatter MaskSequence(byte [] buffer)
        {
            var startIndex = 0;

            while (true)
            {
                var index = Array.FindIndex<byte>(buffer, startIndex, b => b == SymbolV2.DLE || b == SymbolV2.ETX);

                if (index < 0)
                {
                    break;
                }

              
            }

            return this;            
        }

        private byte []  ByteStuff(byte [] buffer, int startIndex)
        {
            var result = buffer.ToList();

            while (true)
            {
                var index = result.FindIndex(startIndex, b => b == SymbolV3.STX || b == SymbolV3.ESC);

                if (index < 0)
                {
                    break;
                }

                var part = (result[index] == SymbolV3.STX) ? new List<byte>() { SymbolV3.ESC, SymbolV3.TSTX } : new List<byte>() { SymbolV3.ESC, SymbolV3.TESC };
                result.RemoveAt(index);
                result.InsertRange(index, part);

                startIndex = index + 2;

                if (result.Count < startIndex)
                {
                    break;
                }
            }

            return result.ToArray();
        }

        private byte [] GetLength(int len)
        {            
            byte low = Convert.ToByte(len & 0x7F);
            byte high = Convert.ToByte(len >> 7);

            var result = new byte[] { low, high };

            return result;
        }

     
        private byte GetNextCommandId()
        {
            _commandId = (++_commandId >= (byte)0xFF) ? (byte)0x01 : _commandId;
            return _commandId;
        }

        public static byte CalculateCRC(byte [] data)
        {
            byte crc = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                crc ^= data[i];
            }

            return crc;
        }


        public static byte CalculateCRC8(byte[] data)
        {
            byte crc = 0xFF;
            for (int i = 0; i < data.Length; i++)
            {
                crc ^= data[i];
                for (int k = 0; k < 8; k++)
                {
                    byte flag = Convert.ToByte(crc & 0x80);
                    crc <<= 1;
                    if (flag != 0)
                    {
                        crc ^= 0x31;
                    }
                }

            }

            crc ^= 0x00;

            return crc;
        }


        /*

        unsigned char dallas_crc8(const unsigned char * data, const unsigned int size)
        {
            unsigned char crc = 0;
            for ( unsigned int i = 0; i < size; ++i )
            {
                unsigned char inbyte = data[i];
                for ( unsigned char j = 0; j < 8; ++j )
                {
                    unsigned char mix = (crc ^ inbyte) & 0x01;
                    crc >>= 1;
                    if ( mix ) crc ^= 0x8C;
                    inbyte >>= 1;
                }
            }
            return crc;
        }         

                 */
    }
}
