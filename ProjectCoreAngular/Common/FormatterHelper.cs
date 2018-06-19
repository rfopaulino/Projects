using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public static class FormatterHelper
    {
        public static string[] Suggestion(List<SuggestionDto> dto)
        {
            var result = new string[dto.Count];
            for (int i = 0; i < dto.Count; i++)
            {
                result[i] = dto[i].View;
            }

            return result;
        }
    }
}
