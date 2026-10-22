Exception Handling 
السؤال الأول منع الأرقام المكررة
تقرأ قائمة أرقام صحيحة من المستخدم، ولو فيه رقم متكرر ترمي Exception.
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Enter integers separated by spaces:");

            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            List<int> numbers = new List<int>();

            foreach (string part in parts)
            {
                int number = int.Parse(part);

                if (numbers.Contains(number))
                {
                    throw new Exception(
                        "Duplicate number: " + number);
                }

                numbers.Add(number);
            }

            Console.WriteLine("All numbers are unique.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}

try`: بنحط جواه الكود اللي ممكن يحصل فيه خطأ.
Split(' ')`: بتقسّم الأرقام المكتوبة إلى أجزاء حسب المسافات.
int.Parse()`: بتحوّل كل جزء من نص لرقم صحيح.
numbers.Contains(number)`: بتشوف هل الرقم موجود قبل كده.
throw new Exception(...)`: بترمي خطأ لو الرقم متكرر.
catch`: بيمسك الخطأ ويعرض رسالته بدل ما البرنامج يقفل فجأة

السؤال الثاني التاكد من وجود حروف متحركة في النص
تعمل Method تستقبل `string`، وترمي Exception لو النص مفيهوش أي حرف متحرك (Vowel)

using System;

class Program
{
    static void CheckVowels(string text)
    {
        string vowels = "aeiouAEIOU";

        foreach (char letter in text)
        {
            if (vowels.Contains(letter))
                return;
        }

        throw new Exception(
            "The string does not contain any vowels.");
    }

    static void Main()
    {
        try
        {
            Console.Write("Enter a string: ");
            string input = Console.ReadLine();

            CheckVowels(input);

            Console.WriteLine("The string contains vowels.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}


5. في `Main` بنستخدم `try/catch` علشان نتعامل مع النتيجة.
