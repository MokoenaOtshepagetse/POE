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
    }
}