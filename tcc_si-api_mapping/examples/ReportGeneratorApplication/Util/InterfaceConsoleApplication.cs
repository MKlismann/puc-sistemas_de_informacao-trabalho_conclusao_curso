using System;

namespace ReportGeneratorApplication.Util
{
    public static class InterfaceConsoleApplication
    {
        public static void WriteMessage(string message, bool? includeTime = false, int? tabLevel = 0)
        {
            var formattedMessage = string.Empty;

            if (includeTime.HasValue && includeTime.Value)
                formattedMessage = string.Format($"[{ DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")}] - {message}");
            else
                formattedMessage = message;

            if (tabLevel.HasValue && tabLevel.Value > 0)
            {
                var tabs = "\t";
                for (var i = 1; i < tabLevel; i++)
                {
                    tabs += tabs;
                }

                formattedMessage = tabs + formattedMessage;
            }

            Console.WriteLine(formattedMessage);
        }

        public static void InsertBlankLine()
        {
            Console.WriteLine();
        }
    }
}
