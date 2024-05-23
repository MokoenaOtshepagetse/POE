using Recipe;
using System;
using System.Collections.Generic;

namespace Recipe
{
    public class RecipeHolder
    {
        public void MakeRecipe(List<string> ingredients, List<(double quantity, int unitIndex)> measurements, List<string> steps)
        {
            Console.WriteLine("Ingredients: ");
            for (int i = 0; i < ingredients.Count; i++)
            {
                double quantity = measurements[i].quantity;
                string unit = GetMeasurementUnit(measurements[i].unitIndex);
                Console.WriteLine($"{quantity} {unit} {ingredients[i]}");
            }
            Console.WriteLine("\nSteps: ");
            for (int i = 0; i < steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {steps[i]}");
            }
        }

        private static string GetMeasurementUnit(int unitIndex)
        {
            string[] units =
            {
                "teaspoon", "tablespoon", "cup", "g", "kg", "ml", "l"
            };
            if (unitIndex < 0 || unitIndex >= units.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(unitIndex), "Not a valid unit of measure");
            }
            return units[unitIndex];
        }
    }
}


