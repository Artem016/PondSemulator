using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PondSemulator
{
    internal class Diet
    {
        public enum TypeMeal
        {
            Predator,
            Herbivore,
            Mixed
        }

        bool isHerbivore { get; }
        bool isPredator { get; }

        public double extractionSize { get; } //диапазон от нуля в процентах от массы хищника

        public double feedQuantity { get; }// необходимого для трапезы количества еды в процентах от собственной массы

        public Diet(TypeMeal typeMeal, double feedQuantity, double extractionSize)
        {
            switch (typeMeal)
            {
                case TypeMeal.Predator:
                    isHerbivore = false;
                    isPredator = true;
                    break;

                case TypeMeal.Herbivore:
                    isHerbivore = true;
                    isPredator = false;
                    break;
                case TypeMeal.Mixed:
                    isHerbivore = true;
                    isPredator = true;
                    break;
            }
            this.extractionSize = extractionSize;
            this.feedQuantity = feedQuantity;
        }
    }
}
