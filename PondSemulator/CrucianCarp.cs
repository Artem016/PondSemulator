using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    internal class CrucianCarp : Fish
    {
        public CrucianCarp(int daysWithutFoodMax, double weight, int ageMax, Diet diet, Pond pond)
        {
            this.daysWithutFoodMax = daysWithutFoodMax;
            this.weight = weight;
            this.ageMax = ageMax;
            this.diet = diet;
            this.pond = pond;
        }

        internal override void Eat()
        {
            if (!isDead)
            {
                double needFeed = diet.feedQuantity * weight;
                if (pond.feedMassNow >= needFeed)
                {
                    pond.FetchFeed(needFeed);
                    weight += needFeed;
                    pond.BiomassModification(needFeed);
                    daysWithutFoodNow = 0;
                }
                else
                {
                    daysWithutFoodNow++;
                    if (!isDead && daysWithutFoodNow >= daysWithutFoodMax)
                    {
                        Dead();
                    }
                }
            }    
        }

        internal override bool TryHunt()
        {
            Console.WriteLine("Данная рыба не умеет охотиться");
            return false;
        }

        internal override void Dead()
        {
            if (!isDead)
            {
                base.Dead();
                pond.ReductionFishType(1, Pond.FishType.CrucianCarp);
            }
        }
    }
}
