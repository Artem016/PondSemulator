using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    internal class DaysSemulator
    {
        int dayNumber;

        public void StartSettings(Pond pond)
        {
            Console.WriteLine("Добро пожаловать в семулятор пруда!");

            int pikeQentity = 0;
            bool validInput = false;
            do
            {
                Console.Write("Введите количество щук, которые будут запущены в водоем: ");
                try
                {
                    pikeQentity = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: Неверный формат ввода. Пожалуйста, введите целое число.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: Введенное число слишком большое. Пожалуйста, введите другое число.");
                }
            } while (!validInput);

            pond.AddFry(Pond.FishType.Pike, pikeQentity);

            int crucianCarpQentity = 0;
            validInput = false;
            do
            {
                Console.Write("Введите количество карасей, которые будут запущены в водоем: ");
                try
                {
                    crucianCarpQentity = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: Неверный формат ввода. Пожалуйста, введите целое число.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: Введенное число слишком большое. Пожалуйста, введите другое число.");
                }
            } while (!validInput);

            pond.AddFry(Pond.FishType.CrucianCarp, crucianCarpQentity);

            int parchQentity = 0;
            validInput = false;
            do
            {
                Console.Write("Введите количество окуней, которые будут запущены в водоем: ");
                try
                {
                    parchQentity = Convert.ToInt32(Console.ReadLine());
                    validInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка: Неверный формат ввода. Пожалуйста, введите целое число.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Ошибка: Введенное число слишком большое. Пожалуйста, введите другое число.");
                }
            } while (!validInput);

            pond.AddFry(Pond.FishType.Perch, parchQentity);
        }

        public void SemulateNextDay(Pond pond)
        
        {
            dayNumber++;
            pond.GainFeed();
            foreach (var fish in pond.fishes)
            {
                fish.GrowOld(1);
                fish.Eat();
            }
            pond.DestructionDeadFish();
            Console.WriteLine(GetStatistic(pond));
        }

        public void SemulateTargetDay(Pond pond, int targetDay)
        {
            int tempDayNumber = dayNumber;
            if(targetDay > dayNumber)
            {
                for (int i = 1; i <= targetDay - tempDayNumber; i++)
                {
                    SemulateNextDay(pond);
                }
            }       
        }

        public string GetStatistic(Pond pond)
        {
            return $"День {dayNumber} \n" +
                $"Количство рыб в пруду: {pond.fishQuantity}\n" +
                $"Общая биомасса рыб в пруду: {pond.fishBiomassNow} кг\n" +
                $"Умерло рыб в пруду: {pond.deadFishQuentityToday}\n" +
                $"Количество корма в пруду: {pond.feedMassNow} кг\n" +
                $"Количество щук в пруду: {pond.quantityPike}\n" +
                $"Количество карасей в пруду: {pond.quantityCrucianCarp}\n" +
                $"Количество окуней в пруду: {pond.quantityPerch}";
        }
    }
}
