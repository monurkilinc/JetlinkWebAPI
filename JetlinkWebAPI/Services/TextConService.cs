using JetlinkWebAPI.Application.DTOs;
using JetlinkWebAPI.Application.Interfaces;

namespace JetlinkWebAPI.Application.Services
{
    public class TextConService : ITextConService
    {
        private readonly Dictionary<string, long> _numberWords;

        public TextConService()
        {
            _numberWords = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase)
            {
                {"sıfır", 0}, {"bir", 1}, {"iki", 2}, {"üç", 3}, {"dört", 4},
                {"beş", 5}, {"altı", 6}, {"yedi", 7}, {"sekiz", 8}, {"dokuz", 9},
                {"on", 10}, {"yirmi", 20}, {"otuz", 30}, {"kırk", 40}, {"elli", 50},
                {"altmış", 60}, {"yetmiş", 70}, {"seksen", 80}, {"doksan", 90},
                {"yüz", 100}, {"bin", 1000}, {"milyon", 1000000}, {"milyar", 1000000000},
            };
        }

        public UserTextModel ConvertWordToNumber(UserTextModel input)
        {
            string[] words = input.Text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<string> result = new List<string>();
            long currentNumber = 0;
            long tempNumber = 0;

            foreach (string word in words)
            {
                if (_numberWords.TryGetValue(word, out long value))
                {
                    if (value == 100)
                    {
                        tempNumber = tempNumber == 0 ? 100 : tempNumber * 100;
                    }
                    else if (value == 1000 || value == 1000000 || value == 1000000000)
                    {
                        tempNumber = tempNumber == 0 ? value : tempNumber * value;
                        currentNumber += tempNumber;
                        tempNumber = 0;
                    }
                    else
                    {
                        tempNumber += value;
                    }
                }
                else
                {
                    if (currentNumber > 0 || tempNumber > 0)
                    {
                        currentNumber += tempNumber;
                        result.Add(currentNumber.ToString());
                        currentNumber = 0;
                        tempNumber = 0;
                    }
                    result.Add(word);
                }
            }

            if (currentNumber > 0 || tempNumber > 0)
            {
                currentNumber += tempNumber;
                result.Add(currentNumber.ToString());
            }

            input.Output = string.Join(" ", result);
            return input;
        }
    }
}