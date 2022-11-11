using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.CardTools.ReaderNew
{
    class ReaderConstants
    {
        public const byte STX = 0x02;
        public const byte ETX = 0x03;
        public const byte ENQ = 0x05;
        public const byte ACK = 0x06;

        public enum ERequestType : byte
        {
            Init = 0x10,
            GetStatus = 0x20,
            Read = 0x33,
            Cancel = 0x40,
            Write = 0x53,
            PrintImage = 0x7B,
            Eject = 0x80,
            Clean = 0xA0,
            GetCard = 0xB0,
            GetVersion = 0xF0,
            BatteryCheck = 0xF5
        }

        public enum ER : byte
        {
            NoCard = 0x30,
            CardOverMag = 0x31,
            CardOverThermal = 0x32,
            CardBetweenThermalDispenser = 0x33,
            CardInSlot = 0x34
        }

        public enum EP : byte
        {
            NoError = 0x30,
            ReadError = 0x31,
            WriteError = 0x32,
            BlockError = 0x33,
            MotorError = 0x34,
            PrintError = 0x35,
            IllegalError = 0x38,
            BatteryError = 0x40,
            SystemError = 0x41,
            Track1Error = 0x51,
            Track2Error = 0x52,
            Track3Error = 0x53,
            Track12Error = 0x54,
            Track13Error = 0x55,
            Track23Error = 0x56
        }

        public enum ES : byte
        {
            JobEnd = 0x30,
            CantExecuteCommand = 0x32,
            ExecutingCommand = 0x33,
            AwaitingCard = 0x34,
            EmptyDispenser = 0x35,
            NoDispenser = 0x36,
            FullDispenser = 0x37,
        }

        public static byte Checksum(byte[] data, byte payloadSize)
        {
            byte checksum = payloadSize;
            foreach (var b in data)
                checksum ^= b;
            return checksum;
        }
    }
}
