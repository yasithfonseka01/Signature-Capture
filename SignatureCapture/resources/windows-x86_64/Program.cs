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
using System.Collections.Generic;

namespace Examples
{
    class Program
    {
        static readonly Dictionary<string, Action<string[]>> kExamples = new Dictionary<string, Action<string[]>>
        {
            {"Bezel", Bezel.Run},
            {"GetDevices", GetDevices.Run},
            {"GetInfo", GetInfo.Run},
            {"GetStatus", GetStatus.Run},
            {"KCMax", KCMax.Run},
            {"MagEncoding", MagEncoding.Run},
            {"PrintKO", PrintKO.Run},
            {"PrintRW", PrintRW.Run},
            {"PrintYMCKO", PrintYMCKO.Run},
            {"Scan", Scan.Run},
            {"SendCommand", SendCommand.Run},
            {"Workflow", Workflow.Run},
        };

        static void Main(string[] args)
        {
            /// Need to be called to load evolis.dll. An optional path can
            /// given as argument.
            /// 
            /// If not called, evolis.dll, will be load from PATH env variable.
            Evolis.Evolis.LoadEvolisDll();

            if (args.Length < 1 || !kExamples.ContainsKey(args[0])) // at least EXAMPLE is mandatory
                Usage();
            else
                kExamples[args[0]](args);
        }

        static void Usage()
        {
            var program = System.AppDomain.CurrentDomain.FriendlyName;

            Console.WriteLine("usage: {0} EXAMPLE [PRINTER-NAME]", program);
            Console.WriteLine();
            Console.WriteLine("  Examples:");
            foreach (var key in kExamples.Keys)
                Console.WriteLine("    {0}", key);
            Console.WriteLine();
        }
    }
}
