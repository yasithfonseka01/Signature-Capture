/*
 * Evolis C# Wrapper
 *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
 * ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
 * THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
 * PARTICULAR PURPOSE.
 */

using Evolis;
using System;
using System.IO;

namespace Examples
{
    /// <summary>
    /// This example shows how to use the scan API (<see cref="ScanSession"/>) to acquire
    /// images from the scanner (if option available in your printer).
    ///
    /// If the printer is not supervised by the Evolis Premium Suite, you will have
    /// to load the WiseCube library by yourself. WiseCube is the provider of the
    /// scanners embedded in our printers.
    ///
    /// Scan API only supported on Windows systems.
    /// </summary>
    class Scan
    {
        public static void Run(string[] args)
        {
            string printerName = PrinterName.Get(args);
            Connection co = new Connection(printerName, true);

            if (!co.IsOpen())
            {
                Console.WriteLine("> Error: can't open printer context.");
            }
            else
            {
                ScanSession scan = new ScanSession(ref co);

                // If printer is not supervised by Evolis service (evoservice), you
                // need to configure the WiseCube SDK by defining DLL's path :
                //ScanSession.SetLibraryPath("<PATH-TO-wsdef.dll>");

                string reply = scan.SendCommand("#GV", out ReturnCode rc);
                Console.WriteLine("> Send command \"#GV\"");
                Console.WriteLine("    => rc={0}, reply={1}", rc, reply);

                Console.WriteLine("> Scan image");
                if (scan.Acquire())
                {
                    Console.WriteLine("    => Ok");
                    scan.SaveImage(ScanImage.TOP, "_top.bmp");
                    scan.SaveImage(ScanImage.TOP_IR, "_top_ir.bmp");
                    scan.SaveImage(ScanImage.BOTTOM, "_bottom.bmp");
                    scan.SaveImage(ScanImage.BOTTOM_IR, "_bottom_ir.bmp");
                }
                else
                {
                    Console.WriteLine("    => {0}", scan.GetLastError());
                }
            }
        }
    }
}
