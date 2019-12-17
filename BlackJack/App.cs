using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
namespace BlackJack
{


    class App
    {
        [DllImport("user32.dll")]
        public static extern bool ShowWindow(System.IntPtr hWnd, int cmdShow);

        public void Config(Boolean clear)
        {
            Process p = Process.GetCurrentProcess();
            ShowWindow(p.MainWindowHandle, 3);
            Console.Title = "BlackJack";
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (clear == true)
                Console.Clear();
        }


    }
}
