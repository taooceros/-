using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 挂机点名器
{
    static class Extra
    {

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern IntPtr GetDC(IntPtr ptr);
        [DllImport("user32.dll", EntryPoint = "ReleaseDC")]
        static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);


        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        //模拟鼠标滚轮滚动操作，必须配合dwData参数
        const int MOUSEEVENTF_WHEEL = 0x0800;

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(
                IntPtr hdc, // handle to DC
                int nIndex // index of capability
                );

        [DllImport("user32.dll", EntryPoint = "SetCursorPos")]
        private static extern int SetCursorPos(int x, int y);


        public static bool run = false;
        public static void TestClickMouse()
        {
            Console.WriteLine("模拟鼠标移动5个像素点。");
            //mouse_event(MOUSEEVENTF_ABSOLUTE|MOUSEEVENTF_MOVE, 1920 / 2 * 65536 / 1024, 1080 / 2 * 65536 / 768, 0, 0);//相对当前鼠标位置x轴和y轴分别移动50像素
            int X = 1920 / 2;
            int Y = 1080 / 2;

            mouse_event(MOUSEEVENTF_LEFTDOWN, X * 65536 / 1024, Y * 65536 / 768, 0, 0); mouse_event(MOUSEEVENTF_LEFTUP, X * 65536 / 1024, Y * 65536 / 768, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTDOWN, X * 65536 / 1024, Y * 65536 / 768, 0, 0); mouse_event(MOUSEEVENTF_LEFTUP, X * 65536 / 1024, Y * 65536 / 768, 0, 0);

        }

        public static void Start()
        {
            //Console.WriteLine("输入屏幕分辨率(x,y)");

            int[] pos = { DESKTOP.Width, DESKTOP.Height }; // new int[2];
                                                           //var input = Console.ReadLine().Split(',');
                                                           //for (int i = 0; i < 2; i++)
                                                           //{
                                                           //  pos[i] = int.Parse(input[i]);
                                                           //}
                                                           //Console.WriteLine("输入屏幕缩放比例 (1,1.25...)");
            double zoom = ScaleX;                          //double.Parse(Console.ReadLine());
            

            while (true)
            {
                DateTime dateTime = DateTime.Now;
                


                if (run)
                {
                    SetCursorPos(Convert.ToInt32(pos[0] / 2 / zoom), Convert.ToInt32((pos[1] + 170.0 / 1080 * pos[1]) / 2 / zoom));
                    TestClickMouse();
                    Thread.Sleep(5000);
                }
                else
                {
                    Thread.Sleep(5000);
                }
            }
        }

        public static Size DESKTOP
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                Size size = new Size();
                size.Width = GetDeviceCaps(hdc, 118);
                size.Height = GetDeviceCaps(hdc, 117);
                ReleaseDC(IntPtr.Zero, hdc);
                return size;
            }
        }


        /// <summary>
        /// 获取宽度缩放百分比
        /// </summary>
        public static float ScaleX
        {
            get
            {
                IntPtr hdc = GetDC(IntPtr.Zero);
                int t = GetDeviceCaps(hdc, 118);
                int d = GetDeviceCaps(hdc, 8);
                float ScaleX = (float)GetDeviceCaps(hdc, 118) / (float)GetDeviceCaps(hdc, 8);
                ReleaseDC(IntPtr.Zero, hdc);
                return ScaleX;
            }
        }


        /// <summary>
        /// 获取高度缩放百分比
        /// </summary>
        //public static float ScaleY
        //{
        //    get
        //    {
        //        IntPtr hdc = GetDC(IntPtr.Zero);
        //        float ScaleY = (float)(float)GetDeviceCaps(hdc, DESKTOPVERTRES) / (float)GetDeviceCaps(hdc, VERTRES);
        //        ReleaseDC(IntPtr.Zero, hdc);
        //        return ScaleY;
        //    }
        //}
    }
}
