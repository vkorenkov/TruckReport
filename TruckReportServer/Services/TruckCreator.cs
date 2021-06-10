using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TruckReportLibF.Models;

namespace TruckReportServer.Services
{
    /// <summary>
    /// Класс создания тестовых автомобилей
    /// </summary>
    public class TruckCreator
    {
        /// <summary>
        /// Номера тестовых автомобилей
        /// </summary>
        public static readonly string[] truckNumbers = new string[3] { "o001oa178", "o002oo47", "a100aa777" };
        /// <summary>
        /// Список всех автомобилей
        /// </summary>
        public List<Truck> trucks;

        public TruckCreator()
        {
            trucks = new List<Truck>();

            CreateTempTrucks();
        }

        /// <summary>
        /// Создание тестовых автомобилей
        /// </summary>
        private void CreateTempTrucks()
        {
            for(int i = 0; i < truckNumbers.Length; i++)
            {
                trucks.Add(new Truck() { TruckNumber = truckNumbers[i] });
            }

            ChangeParameters();
        }

        /// <summary>
        /// Имитация изменения параметров автомобилей
        /// </summary>
        private async void ChangeParameters()
        {
            Random r = new Random();

            while (true)
            {
                await Task.Delay(1000);

                foreach (var t in trucks)
                {
                    t.MoveTime = r.Next(0, 101);
                    t.StopTime = r.Next(10, 1000);
                    t.CurrentFuelCount = r.Next(0, 101);
                    t.IgnitionCount = r.Next(10, 1000);
                    t.SnockSensor = r.Next(1000, 10000);
                }
            }
        }
    }
}
