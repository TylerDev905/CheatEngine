using CodeDesigner.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDesigner.Library.Editors
{
    public class SnapShotEditor : BaseEditor
    {
        private byte[] _fileBytes
        {
            get => this._bytes;
            set => value = this._bytes;
        }

        public SnapShotEditor(string filePath)
        {
            _fileBytes = System.IO.File.ReadAllBytes(filePath);
        }
    }
}
