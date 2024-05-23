using System.Collections.Generic;
using Xunit;

namespace RecipeTests
{
    public class RecipeHolderTests
    {
        [Fact]
        public void CalculateTotalCalories_ShouldReturnCorrectTotal()
        {
            // Arrange
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Sugar", Quantity = 100, UnitIndex = 3, FoodGroupIndex = 4, Calories = 4 },
                new Ingredient { Name = "Butter", Quantity = 50, UnitIndex = 3, FoodGroupIndex = 1, Calories = 7 }
            };
            var recipe = new RecipeHolder { Ingredients = ingredients };

            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.Equal(850, totalCalories);
        }

        [Fact]
        public void CalculateTotalCalories_ShouldReturnZeroForNoIngredients()
        {
            // Arrange
            var ingredients = new List<Ingredient>();
            var recipe = new RecipeHolder { Ingredients = ingredients };

            // Act
            double totalCalories = recipe.CalculateTotalCalories();

            // Assert
            Assert.Equal(0, totalCalories);
        }
    }

    public class Ingredient
    {
        public string Name { get; set; }
        public double Quantity { get; set; }
        public int UnitIndex { get; set; }
        public int FoodGroupIndex { get; set; }
        public double Calories { get; set; }
    }

    public class RecipeHolder
    {
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(ingredient => ingredient.Calories * ingredient.Quantity);
        }
    }
}