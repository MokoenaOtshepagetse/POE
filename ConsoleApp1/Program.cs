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
                "Dairy", "Protein", "Vegetables", "Fruits", "Grains", "Sugars"
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
        public static List<Ingredient> AdjustMeasurements(List<Ingredient> ingredients, double factor)
        {
            return ingredients.Select(ingredient => new Ingredient
            {
                Name = ingredient.Name,
                Quantity = ingredient.Quantity * factor,
                UnitIndex = ingredient.UnitIndex,
                FoodGroupIndex = ingredient.FoodGroupIndex,
                Calories = ingredient.Calories
            }).ToList();
        }
    }

class Program
{
    static void Main()
    {
        List<RecipeHolder> recipes = new List<RecipeHolder>();
        string[] units = { "teaspoon", "tablespoon", "cup", "g", "kg", "ml", "l" };
        string[] foodGroups = { "Dairy", "Protein", "Vegetables", "Fruits", "Grains", "Sugars" };

        while (true)
        {
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1. Add a recipe");
            Console.WriteLine("2. List recipes");
            Console.WriteLine("3. Display a recipe");
            Console.WriteLine("4. Exit");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    RecipeHolder newRecipe = new RecipeHolder();
                    Console.WriteLine("Enter the name of the recipe:");
                    newRecipe.Name = Console.ReadLine();

                    Console.WriteLine("Enter the number of ingredients:");
                    int ingredientCount = int.Parse(Console.ReadLine());

                    for (int i = 0; i < ingredientCount; i++)
                    {
                        Ingredient ingredient = new Ingredient();

                        Console.WriteLine($"Enter the name of ingredient {i + 1}:");
                        ingredient.Name = Console.ReadLine();

                        Console.WriteLine($"Enter the quantity of ingredient {i + 1}:");
                        ingredient.Quantity = double.Parse(Console.ReadLine());

                        Console.WriteLine("Select the unit of measurement:");
                        for (int j = 0; j < units.Length; j++)
                        {
                            Console.WriteLine($"{j + 1}. {units[j]}");
                        }
                        ingredient.UnitIndex = int.Parse(Console.ReadLine());

                        Console.WriteLine("Select the food group:");
                        for (int j = 0; j < foodGroups.Length; j++)
                        {
                            Console.WriteLine($"{j}. {foodGroups[j]}");
                        }
                        ingredient.FoodGroupIndex = int.Parse(Console.ReadLine());

                        Console.WriteLine($"Enter the calories for {ingredient.Name}:");
                        ingredient.Calories = double.Parse(Console.ReadLine());

                        newRecipe.Ingredients.Add(ingredient);
                    }

                    Console.WriteLine("Enter the number of steps:");
                    int stepCount = int.Parse(Console.ReadLine());

                    for (int i = 0; i < stepCount; i++)
                    {
                        Console.WriteLine($"Enter step {i + 1}:");
                        newRecipe.Steps.Add(Console.ReadLine());
                    }

                    newRecipe.CalorieExceeded += message => Console.WriteLine(message);
                    recipes.Add(newRecipe);
                    break;

            }
        }
    }
}



