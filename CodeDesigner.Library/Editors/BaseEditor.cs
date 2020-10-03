using CodeDesigner.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Editors
{
    public abstract class BaseEditor : IByteEditor, ISnapShotable
    {
        protected byte[] _bytes { get; set; } = new byte[33554432];

        public byte[] Read(int address, int byteLength)
            => _bytes
                .Skip(address)
                .Take(byteLength)
                .ToArray();

        public byte[] Read(string addressHex, int byteLength)
        {
            var address = (UIntPtr)Convert.ToInt32(addressHex, 16);
            return Read(address, byteLength);
        }

        public byte[] Read(UIntPtr address, int byteLength)
            => Read((int)address, byteLength);

        public void Write(int address, byte[] data)
            => data.CopyTo(_bytes, address);

        public void Write(string addressHex, byte[] data)
        {
            var address = (UIntPtr)Convert.ToInt32(addressHex, 16);
            Write(address, data);
        }

        public void Write(UIntPtr address, byte[] data)
        {
            Write((int)address, data);
        }
        public int PageLength { get; set; } = 20;
        private int _currentAddress { get; set; } = 0x00000000;

        public void NextAddress()
        {
            _currentAddress += 0x04;
        }

        public void LastAddress()
        {
            _currentAddress -= 0x04;
        }

        public Dictionary<int, byte[]> NextPage()
        {
            var page = new Dictionary<int, byte[]>();

            for (var i = 0; i < PageLength; i++)
            {
                NextAddress();
                page.Add(_currentAddress, Read(_currentAddress, 0x4));
            }
            return page;
        }
        public Dictionary<int, byte[]> LastPage()
        {
            var page = new Dictionary<int, byte[]>();

            for (var i = 0; i < PageLength; i++)
            {
                LastAddress();
                page.Add(_currentAddress, Read(_currentAddress, 0x4));
            }

            return page;
        }
        public byte[] SnapShot(int snapShotStartIndex, int snapShotLength)
            => _bytes.Skip(snapShotStartIndex)
            .Take(snapShotLength)
            .ToArray();
    }
}
