#region Liscence
//  Copyright (c) 2020 Uzumaki Boruto

//  MSIColorSDK.cs is a part of project MSI_TemperatureC

//  Permission is hereby granted, free of charge, to any person obtaining a copy
//  of this software and associated documentation files (the "Software"), to deal
//  in the Software without restriction, including without limitation the rights
//  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//  copies of the Software, and to permit persons to whom the Software is
//  furnished to do so, subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all
//  copies or substantial portions of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//  OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE
//  OR OTHER DEALINGS IN THE SOFTWARE.
#endregion

using MSI_TemperatureC.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace MSI_TemperatureC
{
    public class MSIColorSDK
    {
        /// <summary>
        /// Request is completed.
        /// </summary>
        public const int MLAPI_OK = 0;
        /// <summary>
        /// Generic error.
        /// </summary>
        public const int MLAPI_ERROR = -1;
        /// <summary>
        /// Request is timeout.
        /// </summary>
        public const int MLAPI_TIMEOUT = -2;
        /// <summary>
        /// MSI application not found or installed version not supported.
        /// </summary>
        public const int MLAPI_NO_IMPLEMENTED = -3;
        /// <summary>
        /// MLAPI_Initialize has not been called successful.
        /// </summary>
        public const int MLAPI_NOT_INITIALIZED = -4;
        /// <summary>
        ///  The parameter value is not valid.
        /// </summary>
        public const int MLAPI_INVALID_ARGUMENT = -101;
        /// <summary>
        /// The device is not found.
        /// </summary>
        public const int MLAPI_DEVICE_NOT_FOUND = -102;
        /// <summary>
        /// Requested feature is not supported in the selected LED
        /// </summary>
        public const int MLAPI_NOT_SUPPORTED = -103;

        private const string DllPath = "\\ExternalLibrary\\MysticLight_SDK_x64.dll";

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MLAPI_Initialize();

        static MSIColorSDK()
        {
            var item = MLAPI_Initialize();
            MLAPI_GetErrorMessage(item, out string description);
            Console.WriteLine(description);
        }

        #region Dll Function
        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MLAPI_GetDeviceInfo(
            [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] devTypes,
            [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] ledCounts
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetDeviceName(
           [In, MarshalAs(UnmanagedType.BStr)] string type,
           [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] devName
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetDeviceNameEx(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.BStr)] out string deviceName
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetLedInfo(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.BStr)] out string name,
            [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] ledStyles
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl)]
        private static extern int MLAPI_GetLedName(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [Out, MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] out string[] deviceName
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetLedColor(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.U4)] out uint R,
            [Out, MarshalAs(UnmanagedType.U4)] out uint G,
            [Out, MarshalAs(UnmanagedType.U4)] out uint B
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetLedStyle(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.BStr)] out string style
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetLedMaxBright(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.U4)] out uint maxLevel
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern int MLAPI_GetLedBright(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.U4)] out uint currentLevel
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetLedMaxSpeed(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.U4)] out uint maxLevel
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetLedSpeed(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [Out, MarshalAs(UnmanagedType.U4)] out uint currentLevel
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedColor(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [In, MarshalAs(UnmanagedType.U4)] uint R,
            [In, MarshalAs(UnmanagedType.U4)] uint G,
            [In, MarshalAs(UnmanagedType.U4)] uint B
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedColors(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_BSTR)] ref String[] ledName,
            [MarshalAs(UnmanagedType.U4)] ref uint R,
            [MarshalAs(UnmanagedType.U4)] ref uint G,
            [MarshalAs(UnmanagedType.U4)] ref uint B
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedColorEx(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [In, MarshalAs(UnmanagedType.BStr)] string ledName,
            [In, MarshalAs(UnmanagedType.U4)] uint R,
            [In, MarshalAs(UnmanagedType.U4)] uint G,
            [In, MarshalAs(UnmanagedType.U4)] uint B,
            [In, MarshalAs(UnmanagedType.U4)] uint Sync
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedColorSync(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [In, MarshalAs(UnmanagedType.BStr)] string ledName,
            [In, MarshalAs(UnmanagedType.U4)] uint R,
            [In, MarshalAs(UnmanagedType.U4)] uint G,
            [In, MarshalAs(UnmanagedType.U4)] uint B,
            [In, MarshalAs(UnmanagedType.U4)] uint Sync
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedStyle(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [In, MarshalAs(UnmanagedType.BStr)] string style
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedBright(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [In, MarshalAs(UnmanagedType.U4)] uint level
        );

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_SetLedSpeed(
            [In, MarshalAs(UnmanagedType.BStr)] string type,
            [In, MarshalAs(UnmanagedType.U4)] uint index,
            [In, MarshalAs(UnmanagedType.U4)] uint level
        );


        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private static extern int MLAPI_GetErrorMessage(
            int errorCode,
            [Out, MarshalAs(UnmanagedType.BStr)] out string description
        );
        #endregion

        public IEnumerable<DeviceInfo> GetDeviceInfo()
        {
            MLAPI_GetDeviceInfo(out string[] deviceType, out string[] ledCounts);
            if (deviceType == null || ledCounts == null)
                return Enumerable.Empty<DeviceInfo>();
            return deviceType.Zip(ledCounts, (device, ledCount) => new DeviceInfo(device, int.Parse(ledCount)));
        }
        public string[] GetDeviceName(string type)
        {
            MLAPI_GetDeviceName(type, out string[] deviceName);
            return deviceName;
        }
        public (string Name, string[] LedStyles) GetLedInfo(string type, uint index)
        {
            MLAPI_GetLedInfo(type, index, out string name, out string[] ledStyles);
            return (name, ledStyles);
        }
        public string[] GetLedName(string type)
        {
            MLAPI_GetLedName(type, out string[] names);
            return names;
        }
        public void SetLedColor(string type, uint index, Color color)
        {
            MLAPI_GetLedStyle(type, index, out string style);
            if (style != "Steady")
            {
                MLAPI_SetLedStyle(type, index, "Steady");
            }  
            MLAPI_GetLedColor(type, index, out uint r, out uint g, out uint b);
            if (Color.FromArgb((int)r, (int)g, (int)b).ToArgb() != color.ToArgb())
            {
                var setLedresponse = MLAPI_SetLedColor(type, index, color.R, color.G, color.B);
                MLAPI_GetErrorMessage(setLedresponse, out string description);
                Console.WriteLine(description);
            }
        }
    }
}
