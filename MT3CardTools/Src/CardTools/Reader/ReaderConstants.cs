using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MT3CardTools.Src.CardTools.Reader
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
            ReadData = 0x33,
            CancelJob = 0x40,
            WriteData = 0x53,
            EjectCard = 0x80,
            Clean = 0xA0,
            DispenseCard = 0xB0,
            GetVersion = 0xF0,
            CheckBattery = 0xF5
        }

        public enum EP
        {
            OK = 0x30,
		    ReadError = 0x31,
		    WriteError = 0x32,
		    CardJam = 0x33,
		    MotorError = 0x34,          // transport system motor error
		    PrintError  = 0x35,
		    Error = 0x38,               // generic error
		    BatteryError = 0x40,        // low battery voltage
		    SystemError = 0x41,         // reader/writer system err
		    ReadError_Track1 = 0x51,
		    ReadError_Track2 = 0x52,
		    ReadError_Track3 = 0x53,
		    ReadError_Track1_2 = 0x54,
		    ReadError_Track1_3 = 0x55,
		    ReadError_Track2_3 = 0x56,
	    }

        public enum ES
        {
            Idle = 0x30,
		    IllegalCommand = 0x32,
		    RunningCommand = 0x33,
		    AwaitingCard = 0x34,
		    DispenserEmpty = 0x35,
		    NoDispenser = 0x36,
		    DispenserFull = 0x37,
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
