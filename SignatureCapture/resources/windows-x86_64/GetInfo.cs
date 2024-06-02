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
    /// This example shows how to get :
    ///   - Printer information : model, serial number, ...
    ///   - Ribbon information : type, capacity, ...
    ///   - Cleaning information : remaining card before next cleaning, ...
    /// </summary>
    class GetInfo
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
                if (co.GetInfo(out PrinterInfo pi))
                {
                    Console.WriteLine("> Printer info:");
                    Console.WriteLine("-   Name: {0}", pi.name);
                    Console.WriteLine("-   Model: {0}", pi.model);
                    Console.WriteLine("-   Serial number: {0}", pi.serialNumber);
                    Console.WriteLine("-   Duplex printer: {0}", (pi.hasFlip) ? "yes" : "no");
                    Console.WriteLine("-   ...");
                }

                if (co.GetRibbonInfo(out RibbonInfo ri))
                {
                    Console.WriteLine("> Ribbon info:");
                    Console.WriteLine("-   Name: {0}", ri.description);
                    Console.WriteLine("-   Type: {0}", ri.type);
                    Console.WriteLine("-   Remaining capacity: {0} over {0}",
                        ri.remaining, ri.capacity);
                    Console.WriteLine("-   ...");
                }

                if (co.GetCleaningInfo(out CleaningInfo ci))
                {
                    Console.WriteLine("> Cleaning info:");
                    Console.WriteLine("-   Number of card before next cleaning: {0}", ci.cardCountBeforeWarning);
                    Console.WriteLine("-   Print head under warranty: {0}", (ci.printHeadUnderWarranty) ? "yes" : "no");
                    Console.WriteLine("-   Number of regular cleaning: {0}", ci.regularCleaningCount);
                    Console.WriteLine("-   Number of advanced cleaning: {0}", ci.advancedCleaningCount);
                    Console.WriteLine("-   ...");
                }
            }
        }
    }
}
