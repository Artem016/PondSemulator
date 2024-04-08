using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    internal class Pond
    {
        public enum FishType
        {
            Pike,
            Perch,
            CrucianCarp
        }

        public int fishQuantity;
        public int quantityCrucianCarp;
        public int quantityPerch;
        public int quantityPike;
        public double fishBiomassNow; //в килограммах
        double fishBiomassMax; //в килограммах
        internal double feedMassNow { get; set; } //в килограммах
        internal double feedMassMax; //в килограммах
        internal double feedMassGain; //в килограммах
        public List<Fish> fishes = new List<Fish>();
        public List<Fish> deadFish = new List<Fish>();

        public int deadFishQuentityToday;

        public void FetchFeed(double feedQuentity)
        {
            feedMassNow -= feedQuentity;
        }

        public Pond(double feedMassNow, double feedMassMax, double feedMassGain)
        {
            this.feedMassNow = feedMassNow;
            this.feedMassMax = feedMassMax;
            this.feedMassGain = feedMassGain;
        }

        public void AddFry(FishType fishType, int quentity = 1)
        {
            switch (fishType)
            {
                case FishType.Pike:

                    for (int i = 0; i < quentity; i++)
                    {
                        Diet diet = new Diet(Diet.TypeMeal.Predator, 0, 0.6);
                        Pike pike = new Pike(30,0.09,35, diet, this);
                        fishes.Add(pike);
                        quantityPike++;
                        fishQuantity++;
                        fishBiomassNow += pike.weight;
                    }
                    break;
                case FishType.Perch:
                   
                    for (int i = 0; i < quentity; i++)
                    {
                        Diet diet = new Diet(Diet.TypeMeal.Mixed, 0.5, 0.5);
                        Perch perch = new Perch(30, 0.04, 35, diet, this);
                        fishes.Add(perch);
                        quantityPerch++;
                        fishQuantity++;
                        fishBiomassNow += perch.weight;
                    }
                    break;
                case FishType.CrucianCarp:
                    
                    for (int i = 0; i < quentity; i++)
                    {
                        Diet diet = new Diet(Diet.TypeMeal.Herbivore, 0.4, 0);
                        CrucianCarp crucianCarp = new CrucianCarp(30, 0.02, 35, diet, this);
                        fishes.Add(crucianCarp);
                        quantityCrucianCarp++;
                        fishQuantity++;
                        fishBiomassNow += crucianCarp.weight;
                    }
                    break;
            }

        }

        public void DestructionDeadFish()
        {
            deadFishQuentityToday = 0;
            foreach (var fish in deadFish)
            {
                fishes.Remove(fish);
                deadFishQuentityToday++;
            }
            deadFish.Clear();
        }

        public void GainFeed()
        {
            if(feedMassNow < feedMassMax)
                feedMassNow += feedMassGain;
        }
    }
}
