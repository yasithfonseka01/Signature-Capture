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
    /// This example shows how to print with a YMCKO ribbon.
    ///
    /// As you will see below, we also use some functions to configure
    /// how the card is inserted and ejected.
    /// </summary>
    class PrintYMCKO
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

                // Set card insertion mode :
                co.SetInputTray(InputTray.FEEDER);

                // Set card ejection mode :
                co.SetOutputTray(OutputTray.STANDARD);

                // Set card rejection mode (error cases) :
                co.SetErrorTray(OutputTray.ERROR);

                // If you don't want to eject the card post print.
                //ps.SetAutoEject(False);

                // Set front and back faces :
                if (!ps.SetImage(CardFace.FRONT, "resources/front.bmp"))
                {
                    Console.WriteLine("> Error: can't load file resources/front.bmp");
                    return;
                }
                if (!ps.SetImage(CardFace.BACK, "resources/back.bmp"))
                {
                    Console.WriteLine("> Error: can't load file resources/back.bmp");
                    return;
                }

                /// Print :
                Console.WriteLine("> Start printing...");
                ReturnCode r = ps.Print();
                Console.WriteLine("> Print result: " + r);

                // Trigger card ejection if "ps.SetAutoEject(False)" was used
                //co.EjectCard();
            }
        }
    }
}
