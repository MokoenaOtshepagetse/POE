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

                case 2:
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("No recipes available.");
                    }
                    else
                    {
                        Console.WriteLine("Recipes:");
                        foreach (var recipe in recipes.OrderBy(r => r.Name))
                        {
                            Console.WriteLine(recipe.Name);
                        }
                    }
                    break;

                case 3:
                    if (recipes.Count == 0)
                    {
                        Console.WriteLine("No recipes available.");
                    }
                    else
                    {
                        Console.WriteLine("Enter the name of the recipe to display:");
                        string recipeName = Console.ReadLine();
                        var selectedRecipe = recipes.FirstOrDefault(r => r.Name.Equals(recipeName, StringComparison.OrdinalIgnoreCase));

                        if (selectedRecipe == null)
                        {
                            Console.WriteLine("Recipe not found.");
                        }
                        else
                        {
                            selectedRecipe.MakeRecipe();
                            bool continueRecipe = true;

                            while (continueRecipe)
                            {
                                Console.WriteLine("\nChoose an option:");
                                Console.WriteLine("1. Multiply recipe measurements");
                                Console.WriteLine("2. Reset recipe measurements");
                                Console.WriteLine("3. Back to main menu");
                                int recipeChoice = int.Parse(Console.ReadLine());

                                switch (recipeChoice)
                                {
                                    case 1:
                                        Console.WriteLine("Enter the multiplication factor (0.5, 2, 3):");
                                        double factor = double.Parse(Console.ReadLine());
                                        var adjustedMeasurements = MeasurementAdjuster.AdjustMeasurements(selectedRecipe.Ingredients, factor);
                                        Console.WriteLine($"\nRecipe multiplied by {factor}:");
                                        selectedRecipe.Ingredients = adjustedMeasurements;
                                        selectedRecipe.MakeRecipe();
                                        break;

                                    case 2:
                                        Console.WriteLine("\nOriginal Recipe:");
                                        selectedRecipe.MakeRecipe();
                                        break;

                                    case 3:
                                        continueRecipe = false;
                                        break;

                                    default:
                                        Console.WriteLine("Invalid option. Please try again.");
                                        break;
                                }
                            }
                        }
                    }
                    break;

                case 4:
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;

            }
        }
    }
}



