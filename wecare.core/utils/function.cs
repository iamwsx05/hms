using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using weCare.Core.Entity;

namespace weCare.Core.Utils
{
    /// <summary>
    /// 公共函数/方法
    /// </summary>
    public static class Function
    {
        #region API
        public enum DMDO
        {
            DEFAULT = 0,
            D90 = 1,
            D180 = 2,
            D270 = 3
        }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct DEVMODE
        {
            public const int DM_DISPLAYFREQUENCY = 0x400000;
            public const int DM_PELSWIDTH = 0x80000;
            public const int DM_PELSHEIGHT = 0x100000;
            private const int CCHDEVICENAME = 32;
            private const int CCHFORMNAME = 32;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public DMDO dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int ChangeDisplaySettings([In] ref DEVMODE lpDevMode, int dwFlags);

        //声明一些API函数      
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        private const int IME_CMODE_FULLSHAPE = 0x8;
        private const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;

        [DllImport("user32")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);
        private const int WM_SETREDRAW = 0xB;

        #region 启用/禁用.控件重绘
        /// <summary>
        /// 禁用重绘
        /// </summary>
        /// <param name="hwnd"></param>
        public static void SuspendLayout(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_SETREDRAW, 0, IntPtr.Zero);
        }
        /// <summary>
        /// 启用重绘
        /// </summary>
        /// <param name="hwnd"></param>
        public static void ResumeLayout(IntPtr hwnd)
        {
            SendMessage(hwnd, WM_SETREDRAW, 1, IntPtr.Zero);
        }
        #endregion

        #region 输入法全/半角
        /// <summary>
        /// 安装HOOK
        /// </summary>
        /// <returns></returns>
        [DllImport("ImeHook.dll")]
        public static extern int InstallHook();
        /// <summary>
        /// 卸载HOOK
        /// </summary>
        /// <returns></returns>
        [DllImport("ImeHook.dll")]
        public static extern bool RemoveHook();
        #endregion

        #region API.弹出窗体移动.
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        private static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        const int WM_SYSCOMMAND = 0x0112;
        const int SC_MOVE = 0xF010;
        const int HTCAPTION = 0x0002;

        [DllImport("user32")]
        private static extern bool AnimateWindow(IntPtr hwnd, int dwTime, int dwFlags);
        const int AW_CENTER = 0x0010;
        const int AW_ACTIVATE = 0x20000;

        public static void AnimateWindow(IntPtr hwnd)
        {
            AnimateWindow(hwnd, 100, AW_CENTER | AW_ACTIVATE);
        }

        public static void SendMessage(IntPtr hwnd)
        {
            ReleaseCapture();
            SendMessage(hwnd, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }
        #endregion

        #endregion

        #region config.xml

        #region 变量
        /// <summary>
        /// 本地配置文件
        /// </summary>
        public static string AppConfigFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\app.xml";

        #endregion

        #region 从本地配置文件读取参数值
        /// <summary>
        /// 从本地配置文件读取参数值
        /// </summary>
        /// <param name="p_strNode"></param>
        /// <param name="p_strKey"></param>
        /// <returns></returns>
        public static string ReadLocalSettingValue(string p_strNode, string p_strKey)
        {
            string strValue = string.Empty;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                System.Xml.XmlElement element = null;
                doc.Load(AppConfigFile);

                int n = -1;
                string[] strNodeArr = p_strNode.Split('|');
                switch (strNodeArr.Length)
                {
                    case 1:
                        element = doc[strNodeArr[++n]];
                        break;
                    case 2:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 3:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 4:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 5:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 6:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 7:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 8:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 9:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 10:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    default:
                        return string.Empty;
                }

                if (element != null)
                {
                    strValue = element.Attributes[p_strKey].Value.Trim();
                }
            }
            catch
            {
                strValue = string.Empty;
            }
            finally
            {
                doc = null;
            }
            return strValue;
        }
        #endregion

        #region 从本地配置文件读取参数值
        /// <summary>
        /// 从本地配置文件读取参数值
        /// </summary>
        /// <param name="p_strNode"></param>
        /// <param name="p_strKey"></param>
        /// <returns></returns>
        public static string ReadLocalSettingValue(string p_strNode)
        {
            string strValue = string.Empty;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                System.Xml.XmlElement element = null;
                doc.Load(AppConfigFile);

                int n = -1;
                string[] strNodeArr = p_strNode.Split('|');
                switch (strNodeArr.Length)
                {
                    case 1:
                        element = doc[strNodeArr[++n]];
                        break;
                    case 2:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 3:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 4:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 5:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 6:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 7:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 8:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 9:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 10:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    default:
                        return string.Empty;
                }

                if (element != null)
                {
                    strValue = element.InnerText.Trim();
                }
            }
            catch
            {
            }
            finally
            {
                doc = null;
            }
            return strValue;
        }
        #endregion

        #region 修改本地配置文件参数值
        /// <summary>
        /// 修改本地配置文件参数值
        /// </summary>
        /// <param name="p_strNode"></param>
        /// <param name="p_strKey"></param>
        /// <param name="p_strValue"></param>
        /// <returns></returns>
        public static bool SetLocalSettingValue(string p_strNode, string p_strKey, string p_strValue)
        {
            bool blnRet = false;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                System.Xml.XmlElement element = null;
                doc.Load(AppConfigFile);

                int n = -1;
                string[] strNodeArr = p_strNode.Split('|');
                switch (strNodeArr.Length)
                {
                    case 1:
                        element = doc[strNodeArr[++n]];
                        break;
                    case 2:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 3:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 4:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 5:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 6:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 7:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 8:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 9:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 10:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    default:
                        return false;
                }

                if (element != null)
                {
                    element.Attributes[p_strKey].Value = p_strValue;
                    doc.Save(AppConfigFile);
                    blnRet = true;
                }
            }
            catch
            {
            }
            finally
            {
                doc = null;
            }
            return blnRet;
        }
        #endregion

        #region 修改本地配置文件参数值
        /// <summary>
        /// 修改本地配置文件参数值
        /// </summary>
        /// <param name="p_strNode"></param>
        /// <param name="p_strKey"></param>
        /// <param name="p_strValue"></param>
        /// <returns></returns>
        public static bool SetLocalSettingValue(string p_strNode, string p_strValue)
        {
            bool blnRet = false;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                System.Xml.XmlElement element = null;
                doc.Load(AppConfigFile);

                int n = -1;
                string[] strNodeArr = p_strNode.Split('|');
                switch (strNodeArr.Length)
                {
                    case 1:
                        element = doc[strNodeArr[++n]];
                        break;
                    case 2:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 3:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 4:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 5:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 6:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 7:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 8:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 9:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    case 10:
                        element = doc[strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]][strNodeArr[++n]];
                        break;
                    default:
                        return false;
                }

                if (element != null)
                {
                    element.InnerText = p_strValue;
                    doc.Save(AppConfigFile);
                    blnRet = true;
                }
            }
            catch
            {
            }
            finally
            {
                doc = null;
            }
            return blnRet;
        }
        #endregion

        #region 本地参数Value
        /// <summary>
        /// 本地参数Value
        /// </summary>
        /// <param name="node"></param>
        /// <param name="module"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string LocalSettingValue(string node, string module, string name)
        {
            string value = string.Empty;

            if (GlobalAppConfig.AppConfig != null)
            {
                if (GlobalAppConfig.AppConfig.Exists(t => t.Node.ToLower() == node.ToLower() && t.Module.ToLower() == module.ToLower() && t.Name.ToLower() == name.ToLower()))
                {
                    return (GlobalAppConfig.AppConfig.FirstOrDefault(t => t.Node.ToLower() == node.ToLower() && t.Module.ToLower() == module.ToLower() && t.Name.ToLower() == name.ToLower())).Value;
                }
            }

            return value;
        }
        #endregion

        #region 获取UPDATE.XML信息
        /// <summary>
        /// 获取UPDATE.XML信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetUpdateXmlValue(string key)
        {
            string strValue = string.Empty;
            string strFile = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\updateconfig.xml";

            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            doc.Load(strFile);
            System.Xml.XmlElement element = doc["Main"][key];
            if (element != null)
            {
                strValue = element.Attributes["value"].Value.Trim();
            }

            doc = null;
            element = null;
            return strValue;
        }
        #endregion

        #endregion

        #region 屏幕光标.键盘光标(绝对坐标)
        /// <summary>
        /// 屏幕光标(绝对坐标)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("User32.dll")]
        private static extern int GetCursorPos(out Point point);
        public static Point ScreenCursorPosition()
        {
            Point showPoint = new Point();
            GetCursorPos(out showPoint);
            return showPoint;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern bool GetCaretPos(out Point lpPoint);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetFocus();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr AttachThreadInput(IntPtr idAttach, IntPtr idAttachTo, int fAttach);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThreadId();
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern void ClientToScreen(IntPtr hWnd, ref Point p);

        /// <summary>
        /// 键盘光标(绝对坐标)
        /// </summary>
        /// <returns></returns>
        public static Point ScreenCaretPosition()
        {
            IntPtr ptr = GetForegroundWindow();
            Point p = new Point();

            //得到Caret在屏幕上的位置     
            if (ptr.ToInt32() != 0)
            {
                //IntPtr targetThreadID = GetWindowThreadProcessId(ptr, IntPtr.Zero);
                //IntPtr localThreadID = GetCurrentThreadId();

                //if (localThreadID != targetThreadID)
                //{
                //    AttachThreadInput(localThreadID, targetThreadID, 1);
                ptr = GetFocus();
                if (ptr.ToInt32() != 0)
                {
                    GetCaretPos(out p);
                    ClientToScreen(ptr, ref p);
                }
                //    AttachThreadInput(localThreadID, targetThreadID, 0);
                //}
            }
            return p;
        }

        #endregion

        #region 类型转换

        /// <summary>
        /// XDocument->XmlDocument
        /// </summary>
        /// <param name="xDocument"></param>
        /// <returns></returns>
        public static XmlDocument ConvertToXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        /// <summary>
        /// XmlDocument->XDocument
        /// </summary>
        /// <param name="xmlDocument"></param>
        /// <returns></returns>
        public static XDocument ConvertToXDocument(XmlDocument xmlDocument)
        {
            using (var nodeReader = new XmlNodeReader(xmlDocument))
            {
                nodeReader.MoveToContent();
                return XDocument.Load(nodeReader);
            }
        }

        public static short Short(string str)
        {
            short s = 0;
            short.TryParse(str, out s);
            return s;
        }
        public static short Short(object obj)
        {
            if (obj == null)
                return 0;
            else
                return Short(obj.ToString());
        }

        public static int Int(string str)
        {
            int i = 0;
            int.TryParse(str, out i);
            return i;
        }
        public static int Int(object obj)
        {
            if (obj == null)
                return 0;
            else
                return Int(obj.ToString());
        }

        public static short? Shortnull(string str)
        {
            try
            {
                short s = short.Parse(str);
                return s;
            }
            catch
            {

            }
            return null;
        }
        public static short? Shortnull(object obj)
        {
            if (obj == null) return null;
            return Shortnull(obj.ToString());
        }

        public static int? Intnull(string str)
        {
            try
            {
                int i = int.Parse(str);
                return i;
            }
            catch
            {

            }
            return null;
        }
        public static int? Intnull(object obj)
        {
            if (obj == null) return null;
            return Intnull(obj.ToString());
        }

        public static decimal Dec(string str)
        {
            decimal d = 0;
            decimal.TryParse(str, out d);
            return d;
        }
        public static decimal Dec(object obj)
        {
            if (obj == null)
                return 0;
            else
                return Dec(obj.ToString());
        }

        public static decimal? Decnull(string str)
        {
            try
            {
                decimal dec = decimal.Parse(str);
                return dec;
            }
            catch
            {

            }
            return null;
        }
        public static decimal? Decnull(object obj)
        {
            if (obj == null) return null;
            return Decnull(obj.ToString());
        }

        public static Double Double(string str)
        {
            double d = 0;
            double.TryParse(str, out d);
            return d;
        }
        public static double Double(object obj)
        {
            double d = 0;
            try
            {
                d = Convert.ToDouble(obj);
            }
            catch
            {
                d = 0;
            }
            return d;
        }

        public static double? Doublenull(string str)
        {
            try
            {
                double d = double.Parse(str);
                return d;
            }
            catch
            {

            }
            return null;
        }
        public static double? Doublenull(object obj)
        {
            if (obj == null) return null;
            return Doublenull(obj.ToString());
        }

        public static DateTime Datetime(string str)
        {
            DateTime date = DateTime.Now;
            DateTime.TryParse(str, out date);
            return date;
        }
        public static DateTime Datetime(object obj)
        {
            DateTime date = DateTime.Now;
            try
            {
                date = Convert.ToDateTime(obj);
            }
            catch
            {
                date = Convert.ToDateTime("2000-01-01 00:00:00");
            }
            return date;
        }

        public static DateTime? Datetimenull(string str)
        {
            try
            {
                DateTime date = Convert.ToDateTime(str);
                return date;
            }
            catch
            {
            }
            return null;
        }
        public static DateTime? Datetimenull(object obj)
        {
            if (obj == null) return null;
            return Datetimenull(obj.ToString());
        }

        #region ASCII码转字符
        /// <summary>
        /// ASCII码转字符
        /// </summary>
        /// <param name="p_intValue"></param>
        /// <returns></returns>
        public static string AsciiToStr(int asciiCode)
        {
            System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
            byte[] byteArr = new byte[] { (byte)asciiCode };
            return asciiEncoding.GetString(byteArr);
        }
        #endregion

        #endregion

        #region 利用序列化<->反序列化复制对象
        /// <summary>
        /// 利用序列化<->反序列化复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static T CloneByXmlSerializer<T>(T RealObject)
        {
            using (System.IO.Stream stream = new System.IO.MemoryStream())
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// 利用序列化<->反序列化复制对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="RealObject"></param>
        /// <returns></returns>
        public static List<T> CloneByXmlSerializer<T>(List<T> RealObject)
        {
            using (System.IO.Stream stream = new System.IO.MemoryStream())
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(List<T>));
                serializer.Serialize(stream, RealObject);
                stream.Seek(0, System.IO.SeekOrigin.Begin);
                return (List<T>)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// Clone
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object CloneByBinary(object obj)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            memoryStream.Position = 0;
            return formatter.Deserialize(memoryStream);
        }
        #endregion

        #region 获取本地硬件信息：CPU序列号、硬盘序列号、HostName、IP、MAC地址...
        /// <summary>
        /// CPU序列号
        /// </summary>
        /// <returns></returns>
        public static string CpuId()
        {
            string strCpuId = string.Empty;
            ManagementClass cimobject = new ManagementClass("Win32_Processor");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                strCpuId += mo.Properties["ProcessorId"].Value.ToString();
            }
            return strCpuId;
        }

        /// <summary>
        /// 硬盘序列号
        /// </summary>
        /// <returns></returns>
        public static string HardDiskId()
        {
            string strHDid = string.Empty;
            ManagementClass cimobject = new ManagementClass("Win32_DiskDrive");
            ManagementObjectCollection moc = cimobject.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                strHDid += mo.Properties["Model"].Value.ToString();
            }
            return strHDid;
        }

        /// <summary>
        /// 本地HostName
        /// </summary>
        /// <returns></returns>
        public static string LocalHostName()
        {
            return Dns.GetHostName();
        }
        /// <summary>
        /// 本地IP
        /// </summary>
        /// <returns></returns>
        public static string LocalIP()
        {
            string strHostName = Dns.GetHostName();
            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);
            string strHostIP = ipEntry.AddressList[0].ToString();
            for (int i = 0; i < ipEntry.AddressList.Length; i++)
            {
                strHostIP = ipEntry.AddressList[i].ToString();
                if (strHostIP.Length <= 15)
                {
                    if ((strHostIP.Split('.')).Length == 4) break;
                }
            }
            return strHostIP;
        }
        /// <summary>
        /// 本地Mac
        /// </summary>
        /// <returns></returns>
        public static string LocalMac()
        {
            // 20151106 博爱 暂时屏蔽
            return string.Empty;
            string strMac = null;
            ManagementObjectSearcher query = new ManagementObjectSearcher("select * from win32_networkadapterconfiguration");
            ManagementObjectCollection queryCollection = query.Get();
            foreach (ManagementObject mo in queryCollection)
            {
                if (mo["IPEnabled"].ToString() == "True")
                {
                    strMac = mo["MacAddress"].ToString();
                    break;
                }
            }
            return strMac;
        }

        /// <summary>
        /// 总物理内存数
        /// </summary>
        /// <returns></returns>        
        public static decimal GetMemInfo()
        {
            ManagementClass cimobject1 = new ManagementClass("Win32_PhysicalMemory");
            ManagementObjectCollection moc1 = cimobject1.GetInstances();
            double capacity = 0;
            foreach (ManagementObject mo1 in moc1)
            {
                capacity += ((Math.Round(Int64.Parse(mo1.Properties["Capacity"].Value.ToString()) / 1024 / 1024 / 1024.0, 1)));
            }
            moc1.Dispose();
            cimobject1.Dispose();
            return Function.Dec(capacity);
        }

        public static string GetMainBoard()
        {
            string serial = string.Empty;
            ManagementObjectSearcher my = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            foreach (ManagementObject share in my.Get())
            {
                serial = share["Product"].ToString() + ":" + share["SerialNumber"].ToString();
                break;
            }
            return serial;
        }

        /// <summary>
        /// 取默认网卡的配置
        /// 目的：保证读取网卡地址数据一致性
        /// </summary>
        public static NetworkAdapter GetDefaultMacIp()
        {
            NetworkAdapter result = new NetworkAdapter();
            List<NetworkAdapter> netadp = GetNetworkAdapter2();

            if (netadp.Count > 0) result = netadp[0];

            if (netadp.Count > 1)
            {
                foreach (NetworkAdapter net in netadp)
                {
                    if ((net.Name == "内网" || net.Name == "in") && (net.IPAddress.Length > 0))
                    {
                        result = net;
                        break;
                    }
                }
            }
            result.MACAddress = result.MACAddress.Replace(":", string.Empty).Trim();
            return result;
        }

        /// <summary>
        /// 取网卡属性
        /// </summary>
        /// <returns>当前所有网卡数组</returns>
        public static List<NetworkAdapter> GetNetworkAdapter2()
        {
            List<NetworkAdapter> stuAdp = new List<NetworkAdapter>();
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in adapters)
            {
                if (adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet && adapter.OperationalStatus == OperationalStatus.Up)
                {
                    NetworkAdapter p = new NetworkAdapter();
                    p.Description = adapter.Description;
                    p.Name = adapter.Name;
                    PhysicalAddress pa = adapter.GetPhysicalAddress();
                    byte[] bytes = pa.GetAddressBytes();
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb.Append(bytes[i].ToString("X2")); // 以十六进制格式化
                        if (i != bytes.Length - 1)
                            sb.Append(":");
                    }
                    p.MACAddress = sb.ToString();
                    List<string> locaip = new List<string>();
                    IPInterfaceProperties property = adapter.GetIPProperties();
                    foreach (UnicastIPAddressInformation ip in property.UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            locaip.Add(ip.Address.ToString());
                        }
                    }
                    if (locaip.Count > 0)
                        p.IPAddress = locaip.ToArray();
                    else
                        p.IPAddress = new string[] { };

                    List<string> gateway = new List<string>();
                    foreach (GatewayIPAddressInformation ip in property.GatewayAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            gateway.Add(ip.Address.ToString());
                        }
                    }
                    if (gateway.Count > 0)
                        p.DefaultIPGateway = gateway.ToArray();
                    else
                        p.DefaultIPGateway = new string[] { };
                    stuAdp.Add(p);
                }
            }
            return stuAdp;
        }

        /// <summary>
        /// 网卡配置
        /// </summary>
        public struct NetworkAdapter
        {
            public string Description; //设备名
            public string[] IPAddress;  //ip地址
            public string[] IPSubnet;   //子网
            public string MACAddress;   //物理地址
            public string[] DefaultIPGateway;    //网关       
            public string Name;
        }

        /// <summary>
        /// 设置网卡属性IP.Gateway
        /// </summary>
        /// <param name="p_adp">要设置的属性</param>
        public static void SetNetworkAdapter(NetworkAdapter p_adp)
        {
            ManagementBaseObject inPar = null;
            ManagementBaseObject outPar = null;
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if ((bool)mo["IPEnabled"]
                    && (mo.Properties["Description"].Value != null)
                    && mo.Properties["Description"].Value.ToString() == p_adp.Description)
                {
                    //设置ip地址和子网掩码 
                    inPar = mo.GetMethodParameters("EnableStatic");
                    inPar["IPAddress"] = p_adp.IPAddress.ToArray();
                    inPar["SubnetMask"] = p_adp.IPSubnet.ToArray();
                    outPar = mo.InvokeMethod("EnableStatic", inPar, null);

                    //设置网关地址
                    inPar = mo.GetMethodParameters("SetGateways");
                    inPar["DefaultIPGateway"] = p_adp.DefaultIPGateway.ToArray();
                    outPar = mo.InvokeMethod("SetGateways", inPar, null);

                    break;
                }
            }
        }
        #endregion

        #region UnitySection
        /// <summary>
        /// UnitySection
        /// </summary>
        /// <param name="configXml"></param>
        /// <param name="unityName"></param>
        /// <param name="containerName"></param>
        /// <returns></returns>
        public static IUnityContainer UnitySection(string configXml, string unityName, string containerName)
        {
            try
            {
                configXml = System.Windows.Forms.Application.StartupPath + "\\" + configXml;
                IUnityContainer container = new UnityContainer();
                ExeConfigurationFileMap configMap = new ExeConfigurationFileMap { ExeConfigFilename = configXml };
                UnityConfigurationSection configuration = (UnityConfigurationSection)ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None).GetSection(unityName);
                return configuration.Configure(container, containerName);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
            return null;
        }
        #endregion

        #region 读取XML片段
        /// <summary>
        /// 读取XML片段
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXML(string xml)
        {
            Dictionary<string, string> objDict = new Dictionary<string, string>();
            XmlTextReader xr = new XmlTextReader(xml, XmlNodeType.Element, null);
            while (xr.Read())
            {
                if (xr.NodeType == XmlNodeType.Element)
                {
                    objDict.Add(xr.Name.Trim(), xr.ReadString().Trim());
                }
            }
            xr = null;
            return objDict;
        }
        /// <summary>
        /// 读取XML片段
        /// </summary>
        /// <param name="p_strXML"></param>
        /// <param name="p_strNodeName"></param>
        /// <returns></returns>
        public static XmlNodeList ReadXML(string p_strXML, string p_strNodeName)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(p_strXML);
                XmlElement element = document["Main"][p_strNodeName];
                document = null;

                if (element == null)
                    return null;
                else
                    return element.ChildNodes;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 读取XML片段
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static Dictionary<string, string> ReadXmlNodes(string xml, string nodeName)
        {
            Dictionary<string, string> dicVal = new Dictionary<string, string>();
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(xml);
                XmlElement element = document[nodeName];
                document = null;
                if (element != null)
                {
                    foreach (XmlNode node in element.ChildNodes)
                    {
                        if (!dicVal.ContainsKey(node.Name))
                        {
                            dicVal.Add(node.Name, node.InnerText);
                        }
                    }
                }
            }
            catch { }
            return dicVal;
        }

        /// <summary>
        /// 返回dataSet
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static DataSet ReadXml(string xml)
        {
            try
            {
                List<string> lstEncoding = new List<string>() { "encoding=\"gb2312\"", "encoding='gb2312'", "encoding=\"GBK\"", "encoding='GBK'" };
                foreach (string enc in lstEncoding)
                {
                    if (xml.IndexOf(enc) >= 0)
                    {
                        xml = xml.Replace(enc, "encoding=\"utf-8\"");
                    }
                }
                XmlReader reader = XmlReader.Create(new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(xml)));
                DataSet ds = new DataSet();
                ds.ReadXml(reader);
                return ds;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region XML.Document转String
        /// <summary>
        /// XML.Document转String
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns></returns>
        public static string ConvertXmlToString(XmlDocument xmlDoc)
        {
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, null);
            writer.Formatting = Formatting.Indented;
            xmlDoc.Save(writer);
            System.IO.StreamReader sr = new System.IO.StreamReader(stream, System.Text.Encoding.UTF8);
            stream.Position = 0;
            string xmlString = sr.ReadToEnd();
            sr.Close();
            stream.Close();
            return xmlString;
        }
        #endregion

        #region 婚姻状况转换
        /// <summary>
        /// 婚姻状况转换
        /// </summary>
        /// <param name="type">1 ID->名称；2 名称-ID</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MarriageCovert(int type, string value)
        {
            string strValue = string.Empty;
            if (type == 1)
            {
                switch (value)
                {
                    case "1":
                        strValue = "未婚";
                        break;
                    case "2":
                        strValue = "已婚";
                        break;
                    case "3":
                        strValue = "离婚";
                        break;
                    case "4":
                        strValue = "丧偶";
                        break;
                    case "5":
                        strValue = "再婚";
                        break;
                    default:
                        strValue = "不详";
                        break;
                }
            }
            else if (type == 2)
            {
                switch (value)
                {
                    case "未婚":
                        strValue = "1";
                        break;
                    case "已婚":
                        strValue = "2";
                        break;
                    case "离婚":
                        strValue = "3";
                        break;
                    case "丧偶":
                        strValue = "4";
                        break;
                    case "再婚":
                        strValue = "5";
                        break;
                    default:
                        strValue = "0";
                        break;
                }
            }
            return strValue;
        }
        #endregion

        #region 将BYTE转换为IMAGE
        /// <summary>
        /// 将BYTE转换为IMAGE
        /// </summary>
        /// <param name="p_bytArr"></param>
        /// <returns></returns>
        public static Image ImgConvertByteToImage(byte[] bytArr)
        {
            if (bytArr != null)
            {
                System.IO.MemoryStream objTempStream = new System.IO.MemoryStream(bytArr);
                return System.Drawing.Image.FromStream(objTempStream);
            }
            return null;
        }
        #endregion

        #region 四舍五入
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static decimal Round(decimal d, int decimals)
        {
            decimal value = 0;

            if (d == 0) return value;

            string strMal = string.Empty;
            for (int i = 0; i < decimals; i++)
            {
                strMal += "0";
            }

            if (strMal != string.Empty)
            {
                strMal = "0." + strMal;
            }

            string s = d.ToString(strMal);

            decimal.TryParse(s, out value);

            return value;
        }

        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="d"></param>
        /// <param name="decimals"></param>
        /// <returns></returns>
        public static double Round(double d, int decimals)
        {
            double value = 0;

            if (d == 0) return value;

            string strMal = string.Empty;
            for (int i = 0; i < decimals; i++)
            {
                strMal += "0";
            }

            if (strMal != string.Empty)
            {
                strMal = "0." + strMal;
            }

            string s = d.ToString(strMal);

            double.TryParse(s, out value);

            return value;
        }

        #endregion

        #region 小写金额转大写
        /// <summary>
        /// 小写金额转大写
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string Currency(string money)
        {
            double d = 0;
            bool b = double.TryParse(money, out d);
            if (b)
            {
                return Currency(d);
            }
            return money;
        }

        /// <summary>
        /// 小写金额转大写
        /// </summary>
        /// <param name="money"></param>
        /// <returns></returns>
        public static string Currency(double money)
        {
            string[] BigNumArr = { "零", "壹", "贰", "叁", "肆", "伍", "陆", "柒", "捌", "玖" };
            string[] UnitArr = { "分", "角", "圆", "拾", "佰", "仟", "万", "拾", "佰", "仟", "亿", "拾", "佰", "仟" };

            money = Math.Abs(money);

            string Money = money.ToString("0.00").Replace(".", "");

            int len = Money.Length;
            string s = "";
            string Result = "";

            for (int i = 1; i <= len; i++)
            {
                s = Money.Substring(len - i, 1);
                Result = string.Concat(BigNumArr[Int32.Parse(s)] + UnitArr[i - 1], Result);
            }

            Result = Result.Replace("拾零", "拾");
            Result = Result.Replace("零拾", "零");
            Result = Result.Replace("零佰", "零");
            Result = Result.Replace("零仟", "零");
            Result = Result.Replace("零万", "万");
            for (int i = 1; i <= 6; i++)
            {
                Result = Result.Replace("零零", "零");
            }
            Result = Result.Replace("零万", "零");
            Result = Result.Replace("零亿", "億");
            Result = Result.Replace("零零", "零");
            Result = Result.Replace("零角零分", "");
            Result = Result.Replace("零分", "");
            Result += "整";
            Result = Result.Replace("分整", "分");

            return Result;
        }
        #endregion

        #region Decimal去小数位数转换
        /// <summary>
        /// Decimal去小数位数转换
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static decimal Dectruncate0(decimal dec)
        {
            return Function.Dec(dec.ToString("0.########"));
        }
        /// <summary>
        /// Decimal去小数位数转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal Dectruncate0(object obj)
        {
            return Dectruncate0(Function.Dec(obj));
        }
        /// <summary>
        /// Decimal去小数位数转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static decimal Dectruncate0(string str)
        {
            return Dectruncate0(Function.Dec(str));
        }

        #endregion

        #region Decimal去小数位数转换
        /// <summary>
        /// Decimal去小数位数转换
        /// </summary>
        /// <param name="dec"></param>
        /// <returns></returns>
        public static double Doubletruncate0(decimal dec)
        {
            return Function.Double(dec.ToString("0.########"));
        }
        /// <summary>
        /// Decimal去小数位数转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double Doubletruncate0(object obj)
        {
            return Doubletruncate0(Function.Double(obj));
        }
        /// <summary>
        /// Decimal去小数位数转换
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static double Doubletruncate0(string str)
        {
            return Doubletruncate0(Function.Double(str));
        }

        public static string StringTruncate0(string str)
        {
            if (str == null) return string.Empty;
            if (str.IndexOf(".") <= 0) return str;
            str = Stringtruncate(str.Trim());
            if (str.EndsWith(".")) str = str.TrimEnd('.');
            return str;
        }

        static string Stringtruncate(string str)
        {
            if (str.EndsWith("0"))
            {
                str = str.TrimEnd('0');
                return Stringtruncate(str);
            }
            else
            {
                return str;
            }
        }

        #endregion

        #region 格式化日期、时间格式(.)

        public static string FormatDate(DateTime dtmNow)
        {
            return dtmNow.ToString("yyyy.MM.dd");
        }

        public static string FormatTime(DateTime dtmNow)
        {
            return dtmNow.ToString("yyyy.MM.dd HH:mm:ss");
        }

        #endregion

        #region 将对象转换为IMAGE
        /// <summary>
        /// 将对象转换为IMAGE
        /// </summary>
        /// <param name="p_obj"></param>
        /// <returns></returns>
        public static Image ConvertObjectToImage(object p_obj)
        {
            byte[] data = (byte[])p_obj;
            if (data != null)
            {
                System.IO.MemoryStream objTempStream = new System.IO.MemoryStream(data);
                return System.Drawing.Image.FromStream(objTempStream); // new System.Drawing.Bitmap(objTempStream);
            }

            return null;
        }
        #endregion

        #region 截取图像的矩形区域--图像分割
        /// <summary>
        /// 截取图像的矩形区域
        /// </summary>
        /// <param name="source">源图像对应picturebox1</param>
        /// <param name="rect">矩形区域，如上初始化的rect</param>
        /// <returns>矩形区域的图像</returns>
        public static Image AcquireRectangleImage(Image source, Rectangle rect)
        {
            if (source == null || rect.IsEmpty) return null;
            Bitmap bmSmall = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb); //.Format32bppRgb); // 改变像素可能会提高打印清晰度
            using (Graphics grSmall = Graphics.FromImage(bmSmall))
            {
                grSmall.DrawImage(source,
                                  new System.Drawing.Rectangle(0, 0, bmSmall.Width, bmSmall.Height),
                                  rect,
                                  GraphicsUnit.Pixel);
                grSmall.Dispose();
            }
            return bmSmall;
        }
        #endregion

        #region 字符串是否数值(IsNumber)
        /// <summary>
        /// 字符串是否数值(IsNumber)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumber(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            for (int i = 0; i < str.Length; i++)
            {
                if (str.Substring(i, 1) != "." && !Char.IsNumber(str, i))
                    return false;
            }
            return true;
        }
        #endregion

        #region 图像转为BYTE[]
        /// <summary>
        /// 图像转为BYTE[]
        /// </summary>
        /// <param name="p_img">Image</param>
        /// <param name="p_Type">转换类型：0 Bmp 1 Gif 2 Icon 3 Jpeg 4 Png 5 Tiff 6 Wmf </param>
        /// <returns></returns>
        public static byte[] ConvertImageToByte(Image p_img, int p_Type)
        {
            if (p_img == null)
            {
                return null;
            }

            System.IO.MemoryStream objTempStream = new System.IO.MemoryStream();

            if (p_Type == 0)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            else if (p_Type == 1)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Gif);
            }
            else if (p_Type == 2)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Icon);
            }
            else if (p_Type == 3)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (p_Type == 4)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Png);
            }
            else if (p_Type == 5)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Tiff);
            }
            else if (p_Type == 6)
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Wmf);
            }
            else
            {
                p_img.Save(objTempStream, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            return objTempStream.ToArray();
        }
        #endregion

        #region 将BYTE转换为IMAGE
        /// <summary>
        /// 将BYTE转换为IMAGE
        /// </summary>
        /// <param name="p_bytArr"></param>
        /// <returns></returns>
        public static Image ConvertByteToImage(byte[] p_bytArr)
        {
            if (p_bytArr != null)
            {
                System.IO.MemoryStream objTempStream = new System.IO.MemoryStream(p_bytArr);
                return System.Drawing.Image.FromStream(objTempStream); // new System.Drawing.Bitmap(objTempStream);
            }
            return null;
        }
        #endregion

        #region 对象转换为BYTE[]
        /// <summary>
        /// 对象转换为BYTE[]
        /// </summary>
        /// <param name="p_obj"></param>
        /// <returns></returns>
        public static byte[] ConvertObjectToByte(object p_obj)
        {
            if (p_obj == null) return null;
            System.IO.MemoryStream objStream = new System.IO.MemoryStream();
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            objFormatter.Serialize(objStream, p_obj);
            objStream.Position = 0;
            byte[] bytArr = new byte[objStream.Length];
            objStream.Read(bytArr, 0, bytArr.Length);
            objStream.Close();
            objStream.Dispose();
            return bytArr;
        }
        #endregion

        #region 将字节转字符串
        /// <summary>
        /// 将字节转字符串
        /// </summary>
        /// <param name="p_bytArr"></param>
        /// <returns></returns>
        public static string ConvertByteToString(byte[] p_bytArr)
        {
            return System.Text.UnicodeEncoding.Default.GetString(p_bytArr).Trim();
        }
        #endregion

        #region 将字节反序列化为相应对象
        /// <summary>
        ///  将字节反序列化为相应对象
        /// </summary>
        /// <param name="p_bytArr"></param>
        /// <returns></returns>
        public static object ConvertByteToObject(byte[] p_bytArr)
        {
            object obj = null;
            if (p_bytArr == null) return obj;
            System.IO.MemoryStream objStream = new System.IO.MemoryStream(p_bytArr);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter objFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            objStream.Position = 0;
            obj = objFormatter.Deserialize(objStream);
            objStream.Close();
            objStream.Dispose();

            return obj;
        }
        #endregion

        #region 获取Rtf信息
        /// <summary>
        /// 获取Rtf信息
        /// </summary>
        /// <param name="p_bytRtfArr"></param>
        /// <returns></returns>
        public static string GetRtf(byte[] p_bytRtfArr)
        {
            string strRtf = Function.ConvertByteToString(p_bytRtfArr);
            int pos = strRtf.IndexOf(@"{\rtf1");
            if (pos >= 0)
                return strRtf.Substring(pos);
            else
                return string.Empty;
        }
        /// <summary>
        /// 获取Rtf信息
        /// </summary>
        /// <param name="p_objRtf"></param>
        /// <returns></returns>
        public static string GetRtf(object p_objRtf)
        {
            string strRtf = p_objRtf.ToString();
            int pos = strRtf.IndexOf(@"{\rtf1");
            if (pos >= 0)
                return strRtf.Substring(pos);
            else
                return string.Empty;
        }
        #endregion

        #region 根据日期得到星期几

        /// <summary>
        /// 根据日期得到星期几
        /// </summary>
        /// <param name="strNow"></param>
        /// <returns></returns>
        public static string CaculateWeekDay(string strNow)
        {
            return CaculateWeekDay(Function.Datetime(strNow));
        }

        /// <summary>
        /// 根据日期得到星期几
        /// </summary>
        /// <param name="dtmNow"></param>
        /// <returns></returns>
        public static string CaculateWeekDay(DateTime dtmNow)
        {
            return CaculateWeekDay(dtmNow.Year, dtmNow.Month, dtmNow.Day);
        }

        /// 基姆拉尔森计算公式计算日期
        /// </summary>
        /// <param name="y">年</param>
        /// <param name="m">月</param>
        /// <param name="d">日</param>
        /// <returns>星期几</returns>
        static string CaculateWeekDay(int y, int m, int d)
        {
            if (m == 1 || m == 2)
            {
                m += 12;
                y--;         //把一月和二月看成是上一年的十三月和十四月，例：如果是2004-1-10则换算成：2003-13-10来代入公式计算。
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7;
            return GetWeekName(week);
        }

        public static string GetWeekName(int weekId)
        {
            string weekstr = "";
            switch (weekId)
            {
                case 0: weekstr = "星期一"; break;
                case 1: weekstr = "星期二"; break;
                case 2: weekstr = "星期三"; break;
                case 3: weekstr = "星期四"; break;
                case 4: weekstr = "星期五"; break;
                case 5: weekstr = "星期六"; break;
                case 6: weekstr = "星期日"; break;
            }
            return weekstr;
        }

        public static int GetWeekIndex(string weekName)
        {
            int weekIdx = 0;
            switch (weekName)
            {
                case "星期一": weekIdx = 0; break;
                case "星期二": weekIdx = 1; break;
                case "星期三": weekIdx = 2; break;
                case "星期四": weekIdx = 3; break;
                case "星期五": weekIdx = 4; break;
                case "星期六": weekIdx = 5; break;
                case "星期日": weekIdx = 6; break;
            }
            return weekIdx;
        }

        #endregion

        #region 清空指定的文件夹，但不删除文件夹
        /// <summary>
        /// 清空指定的文件夹，但不删除文件夹
        /// </summary>
        /// <param name="dir"></param>
        public static void DeleteFolder(string dir)
        {
            foreach (string d in Directory.GetFileSystemEntries(dir))
            {
                if (File.Exists(d))
                {
                    FileInfo fi = new FileInfo(d);
                    if (fi.Attributes.ToString().IndexOf("ReadOnly") != -1)
                        fi.Attributes = FileAttributes.Normal;
                    File.Delete(d);//直接删除其中的文件  
                }
                else
                {
                    DirectoryInfo d1 = new DirectoryInfo(d);
                    if (d1.GetFiles().Length != 0)
                    {
                        DeleteFolder(d1.FullName);////递归删除子文件夹
                    }
                    Directory.Delete(d);
                }
            }
        }
        #endregion

        #region GetParm
        /// <summary>
        /// GetParm
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static EntityParm GetParm(string key, string value)
        {
            EntityParm parm = new EntityParm();
            parm.key = key;
            parm.value = value;
            return parm;
        }
        #endregion

        #region 从本地配置文件读取参数值
        /// <summary>
        /// 从本地配置文件读取参数值
        /// </summary>
        /// <param name="p_strNode"></param>
        /// <param name="p_strKey"></param>
        /// <returns></returns>
        public static string ReadConfigXml(string node)
        {
            string strValue = string.Empty;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                string strFile = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName + @"\config.xml";
                if (!System.IO.File.Exists(strFile))
                {
                    try
                    {
                        strFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\config.xml";
                        if (!System.IO.File.Exists(strFile))
                        {
                            strFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config.xml";
                        }
                    }
                    catch
                    {

                        strFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config.xml";
                    }
                }
                if (!System.IO.File.Exists(strFile)) return string.Empty;
                System.Xml.XmlElement element = null;
                doc.Load(strFile);

                element = doc["configuration"]["Client"][node];
                if (element != null)
                {
                    strValue = element.InnerText.Trim();
                }
            }
            catch (Exception ex)
            {
                strValue = string.Empty;
            }
            finally
            {
                doc = null;
            }
            return strValue;
        }
        #endregion

        #region 将参数值写入本地配置文件
        /// <summary>
        /// 将参数值写入本地配置文件
        /// </summary>
        /// <param name="node"></param>
        /// <param name="value"></param>
        public static bool SaveConfigXml(string node, string value)
        {
            string strValue = string.Empty;
            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
            try
            {
                string strFile = Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName + @"\config.xml";
                if (!System.IO.File.Exists(strFile))
                {
                    try
                    {
                        strFile = System.AppDomain.CurrentDomain.BaseDirectory + @"\config.xml";
                        if (!System.IO.File.Exists(strFile))
                        {
                            strFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config.xml";
                        }
                    }
                    catch
                    {
                        strFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\config.xml";
                    }
                }
                System.Xml.XmlElement element = null;
                doc.Load(strFile);

                element = doc["configuration"]["Client"][node];
                if (element != null)
                {
                    element.InnerText = value;
                    doc.Save(strFile);
                    return true;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                doc = null;
            }
            return false;
        }
        #endregion

        #region ConvertIListToList
        /// <summary>
        /// ConvertIListToList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static List<T> ConvertIListToList<T>(IList<T> iList) where T : weCare.Core.Entity.BaseDataContract
        {
            if (iList != null && iList.Count >= 1)
            {
                List<T> list = new List<T>();
                for (int i = 0; i < iList.Count; i++)
                {
                    T temp = iList[i] as T;
                    if (temp != null)
                        list.Add(temp);
                }
                return list;
            }
            return new List<T>();
            //return null;
        }
        #endregion

        #region 制表符
        /// <summary>
        /// 制表符
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string Tab(int num)
        {
            string tab = string.Empty;
            for (int i = 0; i < num; i++)
            {
                tab += "\t";
            }
            return tab;
        }
        #endregion

        #region 获取列名
        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<string> GetTableColumnName(DataTable dt)
        {
            List<string> lstCols = new List<string>();
            foreach (DataColumn col in dt.Columns)
            {
                lstCols.Add(col.ColumnName);
            }
            return lstCols;
        }
        #endregion

        #region DataSet2DataTableMerge
        /// <summary>
        /// DataSet2DataTableMerge
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static DataTable DataSet2DataTableMerge(DataSet ds)
        {
            DataTable dtMerge = new DataTable();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dtMerge.Columns.IndexOf(dc.ColumnName) < 0)
                        dtMerge.Columns.Add(dc.ColumnName, typeof(string));
                }
            }
            dtMerge.AcceptChanges();
            DataRow drMerge = dtMerge.NewRow();
            foreach (DataTable dt in ds.Tables)
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    drMerge[dc.ColumnName] = dt.Rows[dt.Rows.Count - 1][dc.ColumnName];
                }
            }
            dtMerge.LoadDataRow(drMerge.ItemArray, true);
            return dtMerge;
        }
        #endregion

        #region 图片转64位字符串
        /// <summary>
        /// 图片转64位字符串
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static string ConvertImageToBase64String(System.Drawing.Image img)
        {
            // 暂时只用jpg格式
            return Convert.ToBase64String(ConvertImageToByte(img, 3));

            //BinaryFormatter binFormatter = new BinaryFormatter();
            //MemoryStream memStream = new MemoryStream();
            //binFormatter.Serialize(memStream, img);
            //return Convert.ToBase64String(memStream.GetBuffer());
        }
        #endregion

        #region 64位字符串转图片
        /// <summary>
        /// 64位字符串转图片
        /// </summary>
        /// <param name="str64"></param>
        /// <returns></returns>
        public static System.Drawing.Image ConvertBase64StringToImage(string str64)
        {
            byte[] bytes = Convert.FromBase64String(str64);
            return ConvertByteToImage(bytes);

            //MemoryStream memStream = new MemoryStream(bytes);
            //BinaryFormatter binFormatter = new BinaryFormatter();
            //return (System.Drawing.Image)binFormatter.Deserialize(memStream);
        }
        #endregion

        #region GetWeekFirstDay
        /// <summary>
        /// 得到本周第一天(以星期一为第一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekFirstDay(DateTime dtmNow)
        {
            //星期一为第一天
            int weeknow = Convert.ToInt32(dtmNow.DayOfWeek);

            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天
            string FirstDay = dtmNow.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        #endregion

        #region GetWeekLastDay
        /// <summary>
        /// 得到本周最后一天(以星期天为最后一天)
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetWeekLastDay(DateTime dtmNow)
        {
            //星期天为最后一天
            int weeknow = Convert.ToInt32(dtmNow.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天
            string LastDay = dtmNow.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }
        #endregion

        #region GetMonthFirstDay
        /// <summary>
        /// GetMonthFirstDay
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetMonthFirstDay(DateTime dtmNow)
        {
            return new DateTime(dtmNow.Year, dtmNow.Month, 1);
        }
        #endregion

        #region GetMonthLastDay
        /// <summary>
        /// GetMonthLastDay
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime GetMonthLastDay(DateTime dtmNow)
        {
            return GetMonthFirstDay(dtmNow).AddMonths(1).AddDays(-1);
        }
        #endregion

        #region 缩略图
        /// <summary>
        /// 缩略图
        /// </summary>
        /// <param name="oriImage"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image Thumbnail(Image oriImage, int width, int height)
        {
            if (oriImage.Width <= width && oriImage.Height <= height)
            {
                return oriImage;
            }
            //按模版大小生成最终图片
            System.Drawing.Image outImage = new System.Drawing.Bitmap(width, height);
            System.Drawing.Graphics templateG = System.Drawing.Graphics.FromImage(outImage);
            templateG.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
            templateG.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            templateG.Clear(Color.White);
            templateG.DrawImage(oriImage, new System.Drawing.Rectangle(0, 0, width, height), new System.Drawing.Rectangle(0, 0, oriImage.Width, oriImage.Height), System.Drawing.GraphicsUnit.Pixel);
            return outImage;
        }
        #endregion

        #region PictureProcess
        /// <summary>
        /// PictureProcess
        /// </summary>
        /// <param name="sourceImage"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <returns></returns>
        public static Image PictureProcess(Image sourceImage, int targetWidth, int targetHeight)
        {
            int width;//图片最终的宽
            int height;//图片最终的高
            try
            {
                System.Drawing.Imaging.ImageFormat format = sourceImage.RawFormat;
                Bitmap targetPicture = new Bitmap(targetWidth, targetHeight);
                Graphics g = Graphics.FromImage(targetPicture);
                g.Clear(Color.Transparent);

                //计算缩放图片的大小
                if (sourceImage.Width > targetWidth && sourceImage.Height <= targetHeight)
                {
                    width = targetWidth;
                    height = (width * sourceImage.Height) / sourceImage.Width;
                }
                else if (sourceImage.Width <= targetWidth && sourceImage.Height > targetHeight)
                {
                    height = targetHeight;
                    width = (height * sourceImage.Width) / sourceImage.Height;
                }
                else if (sourceImage.Width <= targetWidth && sourceImage.Height <= targetHeight)
                {
                    width = sourceImage.Width;
                    height = sourceImage.Height;
                }
                else
                {
                    width = targetWidth;
                    height = (width * sourceImage.Height) / sourceImage.Width;
                    if (height > targetHeight)
                    {
                        height = targetHeight;
                        width = (height * sourceImage.Width) / sourceImage.Height;
                    }
                }
                g.DrawImage(sourceImage, (targetWidth - width) / 2, (targetHeight - height) / 2, width, height);
                sourceImage.Dispose();

                return targetPicture;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        #endregion

        #region 将DataSet转换为xml对象字符串
        /// <summary>
        /// 将DataSet转换为xml对象字符串
        /// </summary>
        /// <param name="xmlDS"></param>
        /// <returns></returns>
        public static string ConvertDataSetToXML(DataSet xmlDS)
        {
            StringWriter writer = null;
            try
            {
                writer = new StringWriter();
                // 用WriteXml方法写入文件.
                xmlDS.WriteXml(writer);
                return writer.GetStringBuilder().ToString();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }
        #endregion

        #region GetLogCaption
        /// <summary>
        /// GetLogCaption
        /// </summary>
        /// <param name="clsName"></param>
        /// <param name="methodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string GetLogCaption(string clsName, string methodName, string msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("类 名 称: " + clsName);
            sb.AppendLine("方法名称: " + methodName);
            sb.AppendLine("信息文本: " + msg);
            return sb.ToString();
        }
        #endregion

        #region GetExceptionCaption
        /// <summary>
        /// GetExceptionCaption
        /// </summary>
        /// <param name="clsName"></param>
        /// <param name="methodName"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static string GetExceptionCaption(string clsName, string methodName, string msg)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("类 名 称: " + clsName);
            sb.AppendLine("方法名称: " + methodName);
            sb.AppendLine("异常文本: " + msg);
            return sb.ToString();
        }
        #endregion

        #region 出生日期计算年龄
        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static string CalcAge(DateTime? date)
        {
            if (date == null)
                return string.Empty;

            DateTime beginDateTime = Function.Datetime(date);
            DateTime endDateTime = DateTime.Now;
            if (date > DateTime.Now)
            {
                return "";
            }

            /*计算出生日期到当前日期总月数*/
            int months = endDateTime.Month - beginDateTime.Month + 12 * (endDateTime.Year - beginDateTime.Year);
            /*出生日期加总月数后，如果大于当前日期则减一个月*/
            int totalMonth = (beginDateTime.AddMonths(months) > endDateTime) ? months - 1 : months;
            if (totalMonth >= 12)
            {
                /*计算整年*/
                int fullYear = totalMonth / 12;
                return fullYear + "岁";
            }
            else if (totalMonth < 12)
                return totalMonth + "月";
            return "";
        }
        #endregion

        #region 反射实现两个类的对象之间相同属性的值的复制
        /// <summary>
        /// 反射实现两个类的对象之间相同属性的值的复制
        /// 适用于没有新建实体之间
        /// </summary>
        /// <typeparam name="D">返回的实体</typeparam>
        /// <typeparam name="S">数据源实体</typeparam>
        /// <param name="d">返回的实体</param>
        /// <param name="s">数据源实体</param>
        /// <returns></returns>
        public static D MapperToModel<D, S>(D d, S s, Dictionary<string, string> mapping)
        {
            try
            {
                var Types = s.GetType();//获得类型  
                var Typed = typeof(D);
               
                foreach (PropertyInfo sp in Types.GetProperties())//获得类型的属性字段  
                {
                    
                    foreach (PropertyInfo dp in Typed.GetProperties())
                    {
                        if ((mapping == null || !mapping.ContainsKey(sp.Name)) && Typed.GetProperties().Any(x => x.Name == sp.Name))
                        {
                            if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                            {
                                dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                            }
                        }
                        else if(mapping != null && mapping.ContainsKey(sp.Name) && Typed.GetProperties().Any(x => x.Name == mapping[sp.Name]))
                        {
                            string spName = Typed.GetProperties().FirstOrDefault(x => x.Name == mapping[sp.Name]).Name;

                            if (dp.Name == sp.Name && dp.PropertyType == sp.PropertyType && dp.Name != "Error" && dp.Name != "Item")//判断属性名是否相同  
                            {
                                dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return d;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="D"></typeparam>
        /// <typeparam name="S"></typeparam>
        /// <param name="d"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static D MapperToModel<D, S>(D d, S s)
        {
            return MapperToModel(d,s,null);
        }
        #endregion
    }

    #region COPYDATASTRUCT
    /// <summary>
    /// COPYDATASTRUCT
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }
    #endregion

}
