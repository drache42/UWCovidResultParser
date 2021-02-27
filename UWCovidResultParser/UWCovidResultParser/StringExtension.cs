using System;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static void Dump(this string str)
        {
            Console.WriteLine(str);
        }
    }
}