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
    /// This example shows how to configure the printer BEZEL.
    /// The BEZEL is only available, as an option, for KC/KM printers.
    ///
    /// It can be configured in 3 ways :
    ///   - The behavior : What to do if the card is not taken ?
    /// Re-insert, reject or nothing (the card is kepts in the bezel).
    ///   - The delay : After how many times we consider that the card was not
    /// taken.
    ///   - The offset : How much the card is ejected (in millimeters).
    /// </summary>
    class Bezel
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
                /// After card ejection, re-insert the card if it's not taken.
                bool ok = co.SetBezelBehavior(BezelBehavior.INSERT);

                /// If ok, we set the bezel delay to 5 seconds.
                if (ok)
                {
                    ok = co.SetBezelDelay(5);
                }

                /// If ok, we set the bezel offset to 2cm.
                if (ok)
                {
                    co.SetBezelOffset(20);
                }
            }
        }
    }
}
