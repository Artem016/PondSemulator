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



        public int fishQuantity { get; private set; }
        public int quantityCrucianCarp { get; private set; }
        public int quantityPerch { get; private set; }
        public int quantityPike { get; private set; }
        public int deadFishQuentityToday { get; private set; }
        public double fishBiomassNow { get; private set; } //в килограммах
        public double fishBiomassMax { get; private set; } //в килограммах
        internal double feedMassNow { get;private set; } //в килограммах
        internal double feedMassMax { get; private set; } //в килограммах
        internal double feedMassGain { get; private set; } //в килограммах
        public List<Fish> fishes = new List<Fish>();
        private List<Fish> deadFish = new List<Fish>();



        public Pond(double feedMassNow, double feedMassMax, double feedMassGain, double fishBiomassMax)
        {
            this.feedMassNow = feedMassNow;
            this.feedMassMax = feedMassMax;
            this.feedMassGain = feedMassGain;
            this.fishBiomassMax = fishBiomassMax;
        }

        public void AddFry(FishType fishType, uint quentity = 1)
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

        public void BiomassModification(double modificateMass)
        {
            fishBiomassNow += modificateMass;
            if (fishBiomassNow < 0.00000001)
            {
                fishBiomassNow = 0;
            }
        }

        public void FishQuentityReduction(int reductionQuentity)
        {
            fishQuantity -= reductionQuentity;
        }

        public Fish VictimFinder(Fish predator)
        {
            foreach (var fish in fishes)
            {
                if (!fish.isDead && fish.weight <= predator.weight * predator.diet.extractionSize)
                {
                    predator.WeightModification(fish.weight);
                    BiomassModification(fish.weight);
                    fish.Dead();
                    predator.WithoutFoodReset();
                    return fish;
                }
            }
            return null;
        }

        Random rnd = new Random();

        public void Fishing() //отлов рыб рыбаками
        {
            Console.WriteLine("Сегодня была рыбалка");
            foreach (var fish in fishes)
            {
                if (rnd.Next(0, 6) == 1)
                {
                    fish.Dead();
                }
            }
        }

        public void FetchFeed(double feedQuentity)
        {
            feedMassNow -= feedQuentity;
        }

        public void AddDeadFish(Fish fish)
        {
            deadFish.Add(fish);
        }

        public void ReductionFishType(int quentity, FishType fishType)
        {
            switch (fishType)
            {
                case FishType.Pike:
                    quantityPike -= quentity;
                    break;
                case FishType.CrucianCarp:
                    quantityCrucianCarp -= quentity;
                    break;
                case FishType.Perch:
                    quantityPerch -= quentity;
                    break;
            }
        }
    }
}
