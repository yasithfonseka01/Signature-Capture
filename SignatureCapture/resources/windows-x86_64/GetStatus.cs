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
    /// This examples shows how you can get data about the printer state.
    /// There are two types of data "hardware status" and "state" :
    ///
    ///   - Hardware status (aka printer flags) are a set of flags split in 4
    /// categories : CONFIG, INFO, WARNING and ERROR.
    /// CONFIG flags will describe your printer configuration(if you have magnetic
    /// encoder or not, dual side printer or not, etc...).
    /// INFO flags contain information about what the printer is doing.
    /// For example, you will find flags to know if the printer is printing or not,
    /// what is the card position in the printer and so on.
    /// WARNING flags indicate potential issues with the printer
    /// For example if the cover is open, or the feeder is empty.
    /// ERROR flags will be raised during a print job when something wrong happens.
    /// Here you will find flags like ERR_MECHANICAL that will be raised if a
    /// mechanical error occurred during print or ERR_HOPPER_FULL if the output tray
    /// is full.
    ///
    ///   - The state is split in two substates named MAJOR and MINOR.
    /// We have 4 MAJOR states : OFF, READY, WARNING and ERROR.The first one means
    /// that your printer is offline and you should power it on to use it. The
    /// READY state means that everything is fine : you can print.The WARNING state
    /// means that your printer is ok but you have to fix something before printing.
    /// The ERROR state means that something went wrong : The printer will remain
    /// blocked until the problem is resolved.
    /// The MINOR state gives a little more detail on the MAJOR state.
    ///
    /// Hardware status is fine-grained data about your printer while state is more
    /// an overview of the whole printer state.
    /// </summary>
    class GetStatus
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
                // Get printer flags:
                //
                // Flags can be retrieved at every time even when printer is reserved
                // by someone else, processing a command or during print.
                if (co.GetStatus(out Status status))
                {
                    // Check, for example, if there is cards in the feeder:
                    if (status.IsOn(Status.WarFlag.WAR_FEEDER_EMPTY))
                        Console.WriteLine("> No cards in the feeder.");

                    // Check if printer cover is open:
                    if (status.IsOn(Status.WarFlag.WAR_COVER_OPEN))
                        Console.WriteLine("> Printer cover is open, please close it.");

                    GetStatus.ShowActiveFlags(status);
                }

                // Get printer logical state:
                //
                // The printer state allows you to know if the printer state is ready or
                // not and gives you hint on the issue.
                if (co.GetState(out State.MajorState mas, out State.MinorState mis))
                {
                    if (mas == State.MajorState.READY)
                        Console.WriteLine("> Printer is READY.");
                    else
                        Console.WriteLine("> Printer state is {0}:{1}", mas, mis);
                }
            }
        }

        public static void ShowActiveFlags(Status status)
        {
            Console.WriteLine("> Flags on:");
            foreach (Evolis.Status.CfgFlag v in Enum.GetValues(typeof(Evolis.Status.CfgFlag)))
            {
                if (status.IsOn(v))
                    Console.WriteLine("  {0}", v);
            }
            foreach (Evolis.Status.InfFlag v in Enum.GetValues(typeof(Evolis.Status.InfFlag)))
            {
                if (status.IsOn(v))
                    Console.WriteLine("  {0}", v);
            }
            foreach (Evolis.Status.WarFlag v in Enum.GetValues(typeof(Evolis.Status.WarFlag)))
            {
                if (status.IsOn(v))
                    Console.WriteLine("  {0}", v);
            }
            foreach (Evolis.Status.ErrFlag v in Enum.GetValues(typeof(Evolis.Status.ErrFlag)))
            {
                if (status.IsOn(v))
                    Console.WriteLine("  {0}", v);
            }
            foreach (Evolis.Status.Ex1Flag v in Enum.GetValues(typeof(Evolis.Status.Ex1Flag)))
            {
                if (status.IsOn(v))
                    Console.WriteLine("  {0}", v);
            }
            foreach (Evolis.Status.Ex2Flag v in Enum.GetValues(typeof(Evolis.Status.Ex2Flag)))
            {
                if (status.IsOn(v))
                    Console.WriteLine("  {0}", v);
            }
        }
    }
}
