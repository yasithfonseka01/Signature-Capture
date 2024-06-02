using Evolis;

namespace Examples
{
    class PrinterName
    {
        const string DEFAULT_PRINTER_NAME = "Evolis Primacy 2";

        public static string Get(string[] args)
        {
            if (args.Length > 1)
            {
                return args[1];
            }
            else
            {
                var devices = Evolis.Evolis.GetDevices();
                var selected = -1;

                for (int i = 0; i < devices.Count; ++i)
                {
                    if (selected == -1
                        || (!devices[selected].isOnline && devices[i].isOnline))
                    {
                        selected = i;
                    }
                }
                if (selected >= 0)
                {
                    return devices[selected].name;
                }
            }
            return DEFAULT_PRINTER_NAME;
        }
    }
}
