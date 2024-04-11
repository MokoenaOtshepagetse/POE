using System;
using System.Runtime.CompilerServices;

namespace Recipe
{
    public class RecipeHolder
    {
        public void Makerecipe(string[] ingredients, double[,] measurements, string[] steps)
        {
            //testing the function by itself
            Console.WriteLine("Ingredients: ");
                        for (int i = 0; i < ingredients.Length; i++)
            {
                double quantity = measurements[i, 0];
                string unit = GetUnit((int)measurements[i, 1]);

                Console.WriteLine($"{quantity}{unit} {ingredients[i]}");
            }
            Console.WriteLine("\nSteps: ");
                        for (int i = 0;i < steps.Length; i++)
            {
                Console.WriteLine($"{i+1}.{steps[i]}");
            }
        }

        private static string GetUnit(int unitIndex)
        {
            string[] units =
            {
                        "teaspoon", "tablespoon", "cup", "g", "kg", "ml", "l"
                    };
            return
        units[unitIndex];
        }

    }
}