using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsDesktop.Controller
{
    public class Clipboard : Control
    {
        private volatile static Clipboard _instance = null;
        public static Clipboard Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new Clipboard();
                }
                return _instance;
            }
        }

        public event EventHandler<ClipboardChangedEventArgs> Changed;


        private IntPtr _clipboardViewerNext;


        public Clipboard()
        {

            RegisterClipboardViewer();
        }

        ~Clipboard()
        {
            Dispose(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposing)
            {
                UnregisterClipboardViewer();
            }
        }

        protected override void WndProc(ref Message m)
        {
            switch ((RAD.Windows.WIN32API.Msgs)(m.Msg))
            {
                case RAD.Windows.WIN32API.Msgs.WM_DRAWCLIPBOARD:
                    System.Diagnostics.Debug.WriteLine("WindowProc DRAWCLIPBOARD: " + m.Msg, "WndProc");
                    OnChanged();
                    RAD.Windows.WIN32API.User32.SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;

                case RAD.Windows.WIN32API.Msgs.WM_CHANGECBCHAIN:
                    System.Diagnostics.Debug.WriteLine("WM_CHANGECBCHAIN: lParam: " + m.LParam, "WndProc");
                    if (m.WParam == _clipboardViewerNext)
                        _clipboardViewerNext = m.LParam;
                    else
                        RAD.Windows.WIN32API.User32.SendMessage(_clipboardViewerNext, m.Msg, m.WParam, m.LParam);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        private void OnChanged()
        {
            try
            {
                IDataObject iData = System.Windows.Forms.Clipboard.GetDataObject();
                Changed?.Invoke(this, new ClipboardChangedEventArgs(iData));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void RegisterClipboardViewer()
        {
            _clipboardViewerNext = RAD.Windows.WIN32API.User32.SetClipboardViewer(this.Handle);
        }
        private void UnregisterClipboardViewer()
        {
            RAD.Windows.WIN32API.User32.ChangeClipboardChain(this.Handle, _clipboardViewerNext);
        }



        public class ClipboardChangedEventArgs : EventArgs
        {
            public readonly IDataObject DataObject;

            public ClipboardChangedEventArgs(IDataObject dataObject)
            {
                DataObject = dataObject;
            }
        }
    }

}
