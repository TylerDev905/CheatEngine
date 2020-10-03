using CodeDesigner.Library.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodeDesigner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public enum MemoryDisplayMode
        {
            Snapshot,
            Pcsx2
        }
        public MemoryDisplayMode CurrentMemoryDisplay { get; set; }

        public abstract class MemoryDisplayItem
        {
            public string MemoryRegionDisplay { get; set; }
            public string AddressDisplay { get; set; }
            public string AddressDataDisplay { get; set; }
            public string OperationDisplay { get; set; }
            public string CommentsDisplay { get; set; }
        }
        private int _address { get; set; } = 0x00000000;
        private int NextAddress { get; set; } = 0x04;
        public List<MemoryDisplayItem> LoadDisplayPage()
        {
            if(CurrentMemoryDisplay is MemoryDisplayMode.Pcsx2)
            {
                var pcsx2Editor = new Pcsx2Editor();

                pcsx2Editor.ReadOperation(_address);

                return new List<MemoryDisplayItem>();
            }
            else if(CurrentMemoryDisplay is MemoryDisplayMode.Snapshot)
            {
                return new List<MemoryDisplayItem>();
            }
            else
            {
                return new List<MemoryDisplayItem>();
            }
        }

    }
}
