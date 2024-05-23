using Recipe;
using System;
using System.Collections.Generic;

namespace Recipe
{
    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int UnitIndex { get; set; }
        public int FoodGroupIndex { get; set; }
        public double Calories { get; set; }

        public override string ToString()
        {
            return $"{Quantity} {GetMeasurementUnit(UnitIndex)} {Name} ({Calories} calories, {GetFoodGroup(FoodGroupIndex)})";
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

        private static string GetFoodGroup(int foodGroupIndex)
        {
            string[] foodGroups =
            {
                "Dairy", "Protein", "Vegetables", "Fruits", "Grains"
            };
            if (foodGroupIndex < 0 || foodGroupIndex >= foodGroups.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(foodGroupIndex), "Not a valid food group");
            }
            return foodGroups[foodGroupIndex];
        }

    }

        public class RecipeHolder
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<string> Steps { get; set; } = new List<string>();

        public delegate void CalorieExceededHandler(string message);
        public event CalorieExceededHandler CalorieExceeded;

        public void MakeRecipe()
        {
            Console.WriteLine($"\nRecipe: {Name}");
            Console.WriteLine("Ingredients:");
            double totalCalories = 0;
            foreach (var ingredient in Ingredients)
            {
                Console.WriteLine(ingredient);
                totalCalories += ingredient.Calories * ingredient.Quantity;
            }
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }
            Console.WriteLine($"\nTotal Calories: {totalCalories}");

            if (totalCalories > 300)
            {
                CalorieExceeded?.Invoke($"Warning: Total calorie count for {Name} exceeds 300 calories.");
            }
        }
    }

        
    }

    public class MeasurementAdjuster
    {
        public static List<(double quantity, int unitIndex)> AdjustMeasurements(List<(double quantity, int unitIndex)> measurements, double factor)
        {
            List<(double quantity, int unitIndex)> adjustedMeasurements = new List<(double quantity, int unitIndex)>();

            foreach (var measurement in measurements)
            {
                double adjustedQuantity = measurement.quantity * factor;
                adjustedMeasurements.Add((adjustedQuantity, measurement.unitIndex));
            }

            return adjustedMeasurements;
        }
    }

    class Program
    {
        static void Main()
        {
            List<string> ingredients = new List<string>();
            List<(double quantity, int unitIndex)> measurements = new List<(double quantity, int unitIndex)>();
            List<string> steps = new List<string>();

            Console.WriteLine("Enter the number of ingredients:");
            int ingredientCount = int.Parse(Console.ReadLine());

            string[] units = { "teaspoon", "tablespoon", "cup", "g", "kg", "ml", "l" };

            for (int i = 0; i < ingredientCount; i++)
            {
                Console.WriteLine($"Enter the name of ingredient {i + 1}:");
                ingredients.Add(Console.ReadLine());

                Console.WriteLine($"Enter the quantity of ingredient {i + 1}:");
                double quantity = double.Parse(Console.ReadLine());

                Console.WriteLine("Select the unit of measurement:");
                for (int j = 0; j < units.Length; j++)
                {
                    Console.WriteLine($"{j+1}. {units[j]}");
                }
                int unitIndex = int.Parse(Console.ReadLine());

                measurements.Add((quantity, unitIndex));
            }

            Console.WriteLine("Enter the number of steps:");
            int stepCount = int.Parse(Console.ReadLine());

            for (int i = 0; i < stepCount; i++)
            {
                Console.WriteLine($"Enter step {i + 1}:");
                steps.Add(Console.ReadLine());
            }

            RecipeHolder recipeHolder = new RecipeHolder();
            Console.WriteLine("\nOriginal Recipe:");
            recipeHolder.MakeRecipe(ingredients, measurements, steps);

            bool continueProgram = true;

            while (continueProgram)
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Multiply recipe measurements");
                Console.WriteLine("2. Reset recipe measurements");
                Console.WriteLine("3. Exit");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the multiplication factor (0.5, 2, 3):");
                        double factor = double.Parse(Console.ReadLine());
                        List<(double quantity, int unitIndex)> adjustedMeasurements = MeasurementAdjuster.AdjustMeasurements(measurements, factor);
                        Console.WriteLine($"\nRecipe multiplied by {factor}:");
                        recipeHolder.MakeRecipe(ingredients, adjustedMeasurements, steps);
                        break;
                    case 2:
                        Console.WriteLine("\nOriginal Recipe:");
                        recipeHolder.MakeRecipe(ingredients, measurements, steps);
                        break;
                    case 3:
                        continueProgram = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }



