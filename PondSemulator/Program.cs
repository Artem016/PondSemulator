namespace PondSemulator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Pond pond = new Pond(10, 100, 1, 50);
            DaysSemulator daysSemulator = new DaysSemulator();
            daysSemulator.StartSettings(pond);

            while (true)
            {
                Console.WriteLine("Для симуляции следующего дня введите + \n" +
                    "Для симуляции до конкретного дня введите номер этого дня \n" +
                    "!!!учтите, что производить симуляцию до предыдущуго дня нельзя.\n");

                string userComand = Console.ReadLine();

                if(userComand == "+")
                {
                    daysSemulator.SemulateNextDay(pond);
                }
                else
                {
                    try
                    {
                        int dayTarget = int.Parse(userComand);
                        daysSemulator.SemulateTargetDay(pond, dayTarget);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Некорректные данные, попробуйте снова... \n");
                    }
                }
            }

        }
    }
}
