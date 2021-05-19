using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EW1_0
{

    class MouseEvents
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x800;
        public static void DoMouseClick(uint X , uint Y)
        {
            //Call the imported function with the cursor's current position            
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
        }
        public static void DoMouseUp(uint X, uint Y)
        {
            //Call the imported function with the cursor's current position            
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        public static void DoMouseDoubleClick(uint X, uint Y)
        {
            //Call the imported function with the cursor's current position            
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        public static void DoMouseRightClick(uint X, uint Y)
        {
            //Call the imported function with the cursor's current position            
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }
        public static void DoMouseWheel(uint X, uint Y)
        {
            //Call the imported function with the cursor's current position            
            mouse_event(MOUSEEVENTF_WHEEL, X, Y, 0, 0);
        }
    }
}
