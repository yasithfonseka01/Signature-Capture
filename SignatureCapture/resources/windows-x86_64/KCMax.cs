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
    /// KC Max printers can be use like any other EVOLIS printers.
    /// The only difference is the management of the 4 feeders.
    ///
    /// Basically, you can use the same flags that you would have use
    /// on other printers : WAR_FEEDER_EMPTY, EX1_INF_FEEDER_NEAR_EMPTY.
    /// Those flags will give the state of the feeder currently in front of the
    /// printer. So if you want the state of the feeder C, you have to configure
    /// the feeder C as the current one.
    ///
    /// Change the value given to <see cref="Connection.SetFeeder(Feeder)"/> to see what happens
    /// on the printer. Remove cards from one of the feeder to see the status
    /// change.
    /// </summary>
    class KCMax
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
                var ps = new PrintSession(ref co);
                var status = new Status();

                /// Disable/Enable auto ejection at end of print.
                /// Ejection is enabled by default.
                ps.SetAutoEject(false);

                /// Choose feeder to be used.
                co.SetFeeder(Feeder.C);

                if (co.GetStatus(out status)) /// Retrieve printer flags.
                {
                    if (status.IsOn(Status.WarFlag.WAR_FEEDER_EMPTY)) /// Check the state of the feeder.
                    {
                        Console.WriteLine("> Error: the feeder is empty.");
                    }
                    else
                    {
                        Console.WriteLine("> Everything is good. Make your print here.");

                        /// Don't forget to eject card if default behavior was disabled.
                        co.EjectCard();
                    }
                }
            }
        }
    }
}
