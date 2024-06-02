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
    /// This example shows how to print on a rewritable card.
    ///
    /// Uncomment FHalftoning setting line and lines configuring rw areas to see
    /// what is changing on the print.
    /// </summary>
    class PrintRW
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
                PrintSession ps = new PrintSession(ref co, RwCardType.MBLACK);

                // Image processing algorithm can be customized with FHalftoning setting.
                ps.SetSetting(SettingsKey.FHalftoning, "FLOYD");

                // Set the image to print to the card:
                if (!ps.SetImage(CardFace.FRONT, "resources/back.bmp"))
                {
                    Console.WriteLine("> Error: can't load file resources/back.bmp");
                    return;
                }

                // In addition to the image configured above, you can, optionally, restrict
                // the printed areas with a monochrome image:
                //if (!ps.SetRwAreas(CardFace.FRONT, "resources/black-overlay.bmp"))
                //{
                //    Console.WriteLine("> Error: can't load file resources/black-overlay.bmp");
                //    return;
                //}

                /// Print :
                Console.WriteLine("> Start printing...");
                ReturnCode r = ps.Print();
                Console.WriteLine("> Print result: " + r);
            }
        }
    }
}
