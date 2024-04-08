﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    internal class Pike : Fish
    {
        Random rnd = new Random();

        public Pike(int daysWithutFoodMax, double weight, int ageMax, Diet diet, Pond pond)
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
                if (TryHunt())
                {
                    foreach (var fish in pond.fishes)
                    {
                        if (!fish.isDead && fish.weight <= weight * diet.extractionSize)
                        {
                            weight += fish.weight;
                            fish.Dead();
                            daysWithutFoodNow = 0;
                            return;
                        }
                    }
                }
                daysWithutFoodNow++;
                if (!isDead && daysWithutFoodNow >= daysWithutFoodMax)
                {
                    Dead();
                }
            }
            
        }

        internal override bool TryHunt()
        {
            if (rnd.Next(0, 2) == 0)
                return true;

            return false;
        }

        internal override void Dead()
        {
            if (!isDead)
            {
                base.Dead();
                pond.quantityPike--;
                Console.WriteLine("Умерла щука");
            }

        }
    }
}
