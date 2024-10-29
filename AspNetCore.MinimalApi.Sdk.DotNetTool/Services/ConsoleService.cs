using Extensions.Pack;

namespace AspNetCore.MinimalApi.Sdk.DotNetTool
{
    internal static class AddConsoleServiceExtension
    {
        internal static void AddConsoleService(this IServiceCollection services)
        {
            services.AddSingletonIfNotExists<ConsoleService>();
        }
    }

    internal sealed class ConsoleService
    {
        internal void WriteLine()
        {
            Console.WriteLine();
        }

        internal void WriteInfo(string value)
        {
            WriteLine(value);
        }

        internal void WriteInput(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal void WriteSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal string ReadLine()
        {
            var result = Console.ReadLine();
            Console.WriteLine();
            return result ?? string.Empty;
        }

        internal void WriteSample(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(value);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        internal void WriteError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}
