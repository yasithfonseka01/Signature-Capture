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
    class GetDevices
    {
        public static void Run(string[] args)
        {
            var devices = Evolis.Evolis.GetDevices();

            Console.WriteLine("Number of detected devices: {0}", devices.Count);
            for (int i = 0; i < devices.Count; ++i)
            {
                Device d = devices[i];
                Console.WriteLine("> Device[" + i + "]");
                Console.WriteLine("-   id: " + d.id);
                Console.WriteLine("-   name: " + d.name);
                Console.WriteLine("-   displayName: " + d.displayName);
                Console.WriteLine("-   uri: " + d.uri);
                Console.WriteLine("-   mark: " + d.mark);
                Console.WriteLine("-   model: " + d.model);
                Console.WriteLine("-   isSupervised: " + d.isSupervised);
                Console.WriteLine("-   isOnline: " + d.isOnline);

                Connection connection = new Connection(d);
                Console.WriteLine("-   PrinterInfo");
                if (connection.GetInfo(out PrinterInfo pi))
                {
                    Console.WriteLine("-      name: {0}", pi.name);
                    Console.WriteLine("-      model: {0}", pi.modelName);
                    Console.WriteLine("-      serial number: {0}", pi.serialNumber);
                }
                else
                {
                    Console.WriteLine("-      error: {0}", connection.GetLastError());
                }
                Console.WriteLine();
            }
        }
    }
}
