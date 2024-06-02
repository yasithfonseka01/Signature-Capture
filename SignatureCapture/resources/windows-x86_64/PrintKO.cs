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
    /// This example shows how to print a card with KO ribbon.
    ///
    /// In this example, we use a custom bitmap for the overlay panel.
    /// If we hadn't used a custom bitmap then the overlay panel would have been
    /// fully printed.
    /// </summary>
    class PrintKO
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
                PrintSession ps = new PrintSession(ref co);

                // Set main image :
                if (!ps.SetImage(CardFace.FRONT, "resources/back.bmp"))
                {
                    Console.WriteLine("> Error: can't load file resources/back.bmp");
                    return;
                }

                // Set overlay option :
                if (!ps.SetOverlay(CardFace.FRONT, "resources/overlay.bmp"))
                {
                    Console.WriteLine("> Error: can't load file resources/overlay.bmp");
                    return;
                }

                /// Print :
                Console.WriteLine("> Start printing...");
                ReturnCode r = ps.Print();
                Console.WriteLine("> Print result: " + r);
            }
        }
    }
}
