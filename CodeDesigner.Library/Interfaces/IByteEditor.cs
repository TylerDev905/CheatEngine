using System;
using System.Diagnostics;

namespace CodeDesigner.Library
{
    public interface IByteEditor
    {
        byte[] Read(int address, int byteLength);
        byte[] Read(string addressHex, int byteLength);
        byte[] Read(UIntPtr address, int byteLength);
        void Write(int address, byte[] data);
        void Write(string addressHex, byte[] data);
        void Write(UIntPtr address, byte[] data);
    }
}