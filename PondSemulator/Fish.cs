using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    abstract internal class Fish
    {
        public bool isDead { get; protected set; }
        private int age = 0; //в днях
        protected int ageMax { get; set; }
        public double weight { get; protected set; } //в килограммах
        protected int daysWithutFoodNow = 0;
        protected int daysWithutFoodMax { get; set; }

        public Pond pond { get; protected set; }

        public Diet diet { get; protected set; }

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
            if (!isDead)
            {
                isDead = true;
                pond.AddDeadFish(this);
                pond.BiomassModification(-weight); //уменьшение биомассы на весь мертвой рыбы
                pond.FishQuentityReduction(1); //уменьшение количесва рыб на одну
            }

        }

        public void WeightModification(double weightModificator)
        {
            weight += weightModificator;
        }

        public void WithoutFoodReset()
        {
            daysWithutFoodNow = 0; 
        }
    }
}
