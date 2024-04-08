using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    abstract internal class Fish
    {
        public bool isDead = false;
        public int age = 0; //в днях
        public int ageMax { get; set; }
        public double weight { get; set; } //в килограммах
        protected int daysWithutFoodNow = 0;
        protected int daysWithutFoodMax { get; set; }

        public Pond pond;

        protected Diet diet { get; set; }

        /// <summary>
        /// Увеличение возраста рыбы на входящий параметр
        /// </summary>
        /// <param name="day">количество дней на которое следует увеличить возраст рыбы</param>
        /// <returns>Ogbcfybt возвращаемого значения</returns>
        public void GrowOld(int day)  
        {
            age += day;
            if (!isDead && age >= ageMax)
                Dead();
        }

        abstract internal void Eat();

        abstract internal bool TryHunt();

        internal virtual void Dead()
        {
            isDead = true;
            pond.deadFish.Add(this);
            pond.fishBiomassNow -= weight;
            pond.fishQuantity--;
            if(pond.fishBiomassNow < 0)
            {
                pond.fishBiomassNow = 0;
            }
        }
    }
}
