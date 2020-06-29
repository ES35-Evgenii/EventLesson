using System;

namespace EventLesson
{
    public class Car
    {
        int speed = 0; //поле
        public event TooFast TooFastDriving;//event - надстройка над делегатом, дает краткость и безопасность
        public delegate void TooFast(int currentSpeed); //делегат "Слишком быстро", передаем int "текущую скорость"

        public void Start()
        {
            speed = 10;
        }

        public void Accelerate()
        {
            speed += 10; //при каждом вызове метода скорость будет увеличиваться на 10 единиц

            if (speed > 80)
            {
                if (TooFastDriving != null)//при использовании event необходимо обязательно проверять на null
                {
                    TooFastDriving(speed); //при скорость более 80 будет вызван делегат в который передается текущая скорость
                }
            }
        }

        public void Stop()
        {
            speed = 0;
        }

        public int ShowSpeed()
        {
            return speed;
        }
    }
    class Program
    {
        static Car car;

        static void Main(string[] args)
        {
            car = new Car();
            car.TooFastDriving += HandleOnTooFast;//два одинаковых события, которые нужны
            car.TooFastDriving += HandleOnTooFast;//чтобы ниже отписаться от одного из них

            car.TooFastDriving -= HandleOnTooFast;//отписываемся, остается только один

            car.Start();

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Скорость = {car.ShowSpeed()}");
                car.Accelerate();
            }


            Console.ReadLine();
        }
        private static void HandleOnTooFast(int speed)
        {
            Console.WriteLine($"О, я понял, текущая скорость = {speed}! Вызываю метод остановки!");
            car.Stop();
        }
    }
}
