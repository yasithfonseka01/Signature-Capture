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
    /// This example shows how to use the magnetic encoding API.
    /// As you will see by reading the code below, you can encode 1, 2 or 3 tracks
    /// at the same time.
    /// </summary>
    class MagEncoding
    {
        private static readonly string[] TRACKS = {
            "0123456789AZERTYUIOPMLKJHGFDSQWXCVBN12345678912345123456789012345ABKIYTRLM",
            "1234567891234567891234567891234567891",
            "1234567891234567891234567891234567891"
        };

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
                MagSession ms = new MagSession(ref co);
                ReturnCode rc;

                // Write magnetic tracks:
                ms.SetCoercivity(MagCoercivity.AUTO);
                ms.SetTrack(0, MagFormat.ISO1, TRACKS[0]);
                ms.SetTrack(1, MagFormat.ISO2, TRACKS[1]);
                ms.SetTrack(2, MagFormat.ISO3, TRACKS[2]);
                if ((rc = ms.Write()) == ReturnCode.OK)
                {
                    Console.WriteLine("> Write done.");
                }
                else
                {
                    Console.WriteLine("> Write error: {0}.", rc);
                }

                // Read magnetic tracks:
                if ((rc = ms.Read()) == ReturnCode.OK)
                {
                    Console.WriteLine("> Read:");
                    Console.WriteLine("-  Coercivity: " + ms.GetCoercivity());
                    Console.WriteLine("-  Track 0: {0} / {1}", ms.GetFormat(0), ms.GetData(0));
                    Console.WriteLine("-  Track 1: {0} / {1}", ms.GetFormat(1), ms.GetData(1));
                    Console.WriteLine("-  Track 2: {0} / {1}", ms.GetFormat(2), ms.GetData(2));
                }
                else
                {
                    Console.WriteLine("> Read error: " + rc + ".");
                }

                /// To write only one track at a time please simply use
                // `MagSession#SetTrack()`  once.
                ///
                /// Example of how to write first track only :
                ms.Init(); // MagSession#Init() is used to reinitialize MagSession object.
                ms.SetTrack(0, MagFormat.ISO1, "HELLO");
                if ((rc = ms.Write()) == ReturnCode.OK)
                {
                    Console.WriteLine("> Write track done.");
                }
                else
                {
                    Console.WriteLine("> Write track error: {0}.", rc);
                }
            }
        }
    }
}
