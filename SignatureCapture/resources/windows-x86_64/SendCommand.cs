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

namespace Examples
{
    /// <summary>
    /// In some particular case it could be usefull to send a raw command to the
    /// printer.We also call those commands "escape commands".
    ///
    /// Here is an example of how to send the command and get the printer reply.
    /// </summary>
    class SendCommand
    {
        public static void Run(string[] args)
        {
            string printerName = PrinterName.Get(args);
            Connection co = new Connection(printerName);

            if (!co.IsOpen())
            {
                Console.WriteLine("> Error: can't open printer context.");
            }
            else
            {
                // Send command to printer (Rfv to read firmware version):
                //
                // Sending a command to the printer could fail if printer is reserved
                // by someone else (during print for example).
                //
                // Printers can process only one command at a time.
                var reply = co.SendCommand("Rfv");

                if (reply != null)
                {
                    Console.WriteLine("> Rfv => {0}", reply);
                }
                else
                {
                    Console.WriteLine("> Error: {0}", co.GetLastError());
                }
            }
        }
    }
}