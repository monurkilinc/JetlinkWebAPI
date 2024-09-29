using JetlinkWebAPI.Application.DTOs;
using JetlinkWebAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JetlinkWebAPI.Application.Services
{
    public class TextConService : ITextConService
    {
        private readonly Dictionary<string, long> _keyValuePairs;

        public TextConService()
        {
            //Yeni sozluk ogelerini ekledik
            //StringComparer.OrdinalIgnoreCase:Buyuk kucuk harf duyarlılıgını yok saymak icin kullandik
            _keyValuePairs = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase)
            {
                {"sıfır", 0}, {"bir", 1}, {"iki", 2}, {"üç", 3}, {"dört", 4},
                {"beş", 5}, {"altı", 6}, {"yedi", 7}, {"sekiz", 8}, {"dokuz", 9},
                {"on", 10}, {"yirmi", 20}, {"otuz", 30}, {"kırk", 40}, {"elli", 50},
                {"altmış", 60}, {"yetmiş", 70}, {"seksen", 80}, {"doksan", 90},
            };
        }
        //Sayilari kelimeye cevirme islemi
        public UserTextModel ConvertWordToNumber(UserTextModel input)
        {
            string[] words=input.Text.Split(' ');

            List<string> result=new List<string>();
            double currentNumber = 0;
            double tempNumber = 0;

            foreach (string word in words)
            {
                //Eger kelime bir sayi ise
                if (_keyValuePairs.TryGetValue(word.ToLower(), out long value))
                {
                    if (value == 100)
                    {
                        tempNumber = 100;
                    }
                    else if (value == 1000 || value == 1000000 || value == 1000000000)
                    {
                        //tempNumber 0 ise value degerini ata,degilse tempNumber*value degerini ata
                        tempNumber = tempNumber == 0 ? value : tempNumber * value;
                        currentNumber += tempNumber; //currentNumber degerine tempNumber degerini ekle
                        tempNumber = 0;

                    }
                    //Diger tüm sayıları icin.
                    else
                    {
                        tempNumber += value;
                    }
                }
                else
                {
                    //Kelime sayi degilse,
                    //Eger tempNumber 0 dan buyukse ve currentNumber 0 dan buyuk degilse
                    if (currentNumber > 0 || tempNumber > 0)
                    {
                        currentNumber += tempNumber;
                        result.Add(currentNumber.ToString());
                        currentNumber = 0;
                        tempNumber = 0;

                    }
                    //Sayi degilse kelimeyi ekle
                    result.Add(word);

                }
            }
            //Dongu bittiginde hala bir tempNumber degeri varsa currentNumber degerine ekle
            if (currentNumber > 0 || tempNumber > 0)
            {
                currentNumber += tempNumber;
                result.Add(currentNumber.ToString());
            }

            //Sonucu stringe cevir ve input.Text'e ata  
            input.Text = string.Join(" ", result);
            return input;
        }
    }
}
