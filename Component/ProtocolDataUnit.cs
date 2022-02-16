using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Component
{
    public static class ProtocolDataUnit
    {
        public struct HEADER
        {
            public byte Tag;
            public uint Syn;
            public uint Ack;
            public int DataLength;
            public ulong DataChecksum;
            public ulong HeaderChecksum;
        }

        public static byte[] ToBytes(this HEADER header)
        {
            int size = Marshal.SizeOf(header);
            byte[] buffer = new byte[size];

            IntPtr ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(header, ptr, true);
            Marshal.Copy(ptr, buffer, 0, size);
            Marshal.FreeHGlobal(ptr);
            return buffer;
        }

        public static void Parse(ref this HEADER header,byte[] Buffer)
        {
            int size = Marshal.SizeOf(header);
            IntPtr ptr = Marshal.AllocHGlobal(size);
            Int32.Parse("1");
            Marshal.Copy(Buffer, 0, ptr, size);
            header=(HEADER)Marshal.PtrToStructure(ptr, header.GetType());
            Marshal.FreeHGlobal(ptr);
        }
    }


}
