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
    /// This code is a full example with the following steps :
    ///   - Make a card insertion.
    ///   - Move the card to contact less station for smart encoding.
    ///   - <The smart encoding is made by your side.>
    ///   - If the smart encoding is ok :
    ///     - We proceed to a magnetic encoding.
    ///     - If the magnetic encoding is ok :
    ///       - The card is printed.
    ///
    /// At the end of the example, you will find a sample code to understand
    /// how to check if the print is ok or not.
    /// </summary>
    class Workflow
    {
        public static bool ConfigureCardInOut(Connection co)
        {
            bool r = true;

            /// Configure card input.
            /// See InputTray enum for accepted values.
            if (r)
                r = co.SetInputTray(InputTray.FEEDER);

            /// Configure card output.
            /// See OutputTray enum for accepted values.
            if (r)
                r = co.SetOutputTray(OutputTray.STANDARD);

            /// Configure card output on error.
            /// See OutputTray enum for accepted values.
            if (r)
                r = co.SetErrorTray(OutputTray.ERROR);

            return r;
        }

        public static void Print(Connection co)
        {
            PrintSession print = new PrintSession(ref co);

            // Set bitmaps to print
            print.SetImage(CardFace.FRONT, "resources/front.bmp");

            ReturnCode r;
            if ((r = print.Print()) == ReturnCode.OK)
            {
                Console.WriteLine("> Print done.");
            }
            else
            {
                Console.WriteLine($"Print error: {r.ToString()}");
                if (r == ReturnCode.PRINT_NEEDACTION)
                {
                    /// One of those flags is on :
                    /// ------------------------
                    ///
                    /// WAR_COVER_OPEN)
                    /// WAR_UNSUPPORTED_RIBBON)
                    /// WAR_RIBBON_ENDED)
                    /// WAR_NO_RIBBON)
                    /// WAR_FEEDER_EMPTY)
                    /// WAR_PRINTER_LOCKED)
                    /// WAR_REMOVE_RIBBON)
                    /// WAR_HOPPER_FULL)
                    /// WAR_REJECT_BOX_FULL))
                    /// if (EX1_CFG_LAMINATOR)
                    ///     EX2_DEF_LAMI_HOPPER_FULL
                    ///     EX2_DEF_LAMI_COVER_OPEN)
                    ///     EX2_DEF_LAMINATE_UNSUPPORTED)
                    ///     EX2_DEF_LAMINATE_END)
                    ///     EX2_DEF_NO_LAMINATE))
                    ///
                    /// Example :
                    /// -------
                    if (co.GetStatus(out Status status))
                    {
                        // Example of WAR_COVER_OPEN flag test.
                        if (status.IsOn(Status.WarFlag.WAR_COVER_OPEN))
                        {
                            Console.WriteLine("> Please close the cover before print.");
                        }
                    }
                }
                else if (r == ReturnCode.PRINT_EMECHANICAL)
                {
                    Console.WriteLine("> Clearing printer errors.");
                    print.ClearMechanicalErrors();
                }
            }
        }

        public static void Run(string[] args)
        {
            string printerName = PrinterName.Get(args);
            Connection co = new Connection(printerName);

            if (!co.IsOpen())
            {
                Console.WriteLine("> Error: can't open co context.");
            }
            else
            {
                /// Getting the state of the printer.
                if (!co.GetState(out State.MajorState majorState, out State.MinorState minorState)
                    || majorState != State.MajorState.READY)
                {
                    Console.WriteLine("> Error: printer is not ready: {0} / {1}", majorState, minorState);
                }
                else if (ConfigureCardInOut(co))
                {
                    bool encodingOk = true;

                    /// Insert the card then move it to SMART station.
                    co.InsertCard();
                    co.SetCardPos(CardPos.CONTACTLESS);
                    /// ... Please do your encoding process here ...

                    if (encodingOk)
                        Print(co);
                }
            }
        }
    }
}
