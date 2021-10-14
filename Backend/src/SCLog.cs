using System;
using Essential.Diagnostics;
using System.Diagnostics;

namespace Backend
{
    public static class SCLog
    {
        static ColoredConsoleTraceListener coloredConsole = new ColoredConsoleTraceListener();

        static SCLog()
        {
            coloredConsole.Template = "{DateTime:HH':'mm':'ssZ} [{EventType}]: {Message}{Data}";
            coloredConsole.SetConsoleColor(TraceEventType.Information, ConsoleColor.Gray);
            coloredConsole.SetConsoleColor(TraceEventType.Warning, ConsoleColor.Yellow);
            coloredConsole.SetConsoleColor(TraceEventType.Error, ConsoleColor.Magenta);
            coloredConsole.SetConsoleColor(TraceEventType.Critical, ConsoleColor.Red);
        }

        [Conditional("DEBUG")]
        public static void LOG(int severityLevel, string message = "")
        {
            switch(severityLevel)
            {
                case 0:
                    {
                        INFO(message);
                        break;
                    }
                case 1:
                    {
                        WARN(message);
                        break;
                    }
                case 2:
                    {
                        ERROR(message);
                        break;
                    }
                case 3:
                    {
                        CRITICAL(message);
                        break;
                    }
            }
        }

        [Conditional("DEBUG")]
        public static void INFO(string message = "", params object[] args)
        {
            coloredConsole.TraceEvent(null, "", TraceEventType.Information, 0, message, args);
        }

        [Conditional("DEBUG")]
        public static void WARN(string message = "", params object[] args)
        {
            Console.WriteLine("{0}");
            coloredConsole.TraceEvent(null, "", TraceEventType.Warning, 0, message, args);
        }

        [Conditional("DEBUG")]
        public static void ERROR(string message = "", params object[] args)
        {
            coloredConsole.TraceEvent(null, "", TraceEventType.Error, 0, message, args);
        }

        [Conditional("DEBUG")]
        public static void CRITICAL(string message = "", params object[] args)
        {
            coloredConsole.TraceEvent(null, "", TraceEventType.Critical, 0, message, args);
        }
    }
}
