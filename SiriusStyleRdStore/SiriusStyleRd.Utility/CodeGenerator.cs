using System;

namespace SiriusStyleRd.Utility
{
    public static class CodeGenerator
    {
        public static string Generate()
        {
            var code = new Random().Next(999999999).ToString();

            var zerosCount = 9 - code.Length;

            for (var i = 0; i < zerosCount; i++) code = code.Insert(0, "0");

            return code;
        }
    }
}