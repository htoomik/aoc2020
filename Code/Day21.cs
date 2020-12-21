using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace aoc2020.Code
{
    public class Day21
    {
        public int Solve(List<string> input)
        {
            var foods = input.Select(Parse).ToList();

            var allergenToIngredient = GetAllergenMap(foods);

            return CountSafe(foods, allergenToIngredient);
        }

        public string Solve2(List<string> input)
        {
            var foods = input.Select(Parse).ToList();

            var allergenToIngredient = GetAllergenMap(foods);

            var list = allergenToIngredient.Select(kvp => kvp).OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value);
            var answer = string.Join(",", list);

            return answer;
        }

        private static int CountSafe(List<Food> foods, Dictionary<string, string> allergenToIngredient)
        {
            var allIngredients = foods.SelectMany(d => d.Ingredients).Distinct();
            var safeIngredients = allIngredients.Except(allergenToIngredient.Values);
            var result = foods.Sum(f => f.Ingredients.Count(safeIngredients.Contains));
            return result;
        }

        private static Dictionary<string, string> GetAllergenMap(List<Food> original)
        {
            var foods = original.Select(d => d.Clone()).ToList();
            var allIngredients = original.SelectMany(d => d.Ingredients).Distinct().ToList();
            var allAllergens = original.SelectMany(d => d.Allergens).Distinct().ToList();
            var ingredientToAllergen = new Dictionary<string, string>();
            var allergenToIngredient = new Dictionary<string, string>();

            var didWork = true;
            while (didWork)
            {
                didWork = false;
                foreach (var allergen in allAllergens)
                {
                    var foodsWithAllergen = foods.Where(f => f.Allergens.Contains(allergen));
                    var sharedIngredients = allIngredients.Where(
                        i => foodsWithAllergen.All(f => f.Ingredients.Contains(i)));
                    if (sharedIngredients.Count() == 1)
                    {
                        didWork = true;
                        var ingredient = sharedIngredients.Single();
                        allergenToIngredient[allergen] = ingredient;
                        ingredientToAllergen[ingredient] = allergen;

                        foreach (var food in foods)
                        {
                            food.Allergens.Remove(allergen);
                            food.Ingredients.Remove(ingredient);
                        }
                    }
                }
            }

            return allergenToIngredient;
        }

        private static Food Parse(string input)
        {
            var sections = input.Split(" (contains ");
            var ingredients = sections[0].Split(" ").ToList();
            var allergens = new List<string>();
            if (sections.Length == 2)
            {
                allergens = sections[1].Replace(")", "").Split(", ").ToList();
            }

            return new Food(ingredients, allergens);
        }

        [DebuggerDisplay("{Ing} - {All}")]
        private class Food
        {
            public List<string> Ingredients { get; set; }
            public List<string> Allergens { get; set; }

            public string Ing => string.Join(", ", Ingredients);
            public string All => string.Join(", ", Allergens);

            public Food(List<string> ingredients, List<string> allergens)
            {
                Ingredients = ingredients;
                Allergens = allergens;
            }

            public Food Clone()
            {
                var ingredients = Ingredients.Select(i => i).ToList();
                var allergens = Allergens.Select(a => a).ToList();
                return new Food(ingredients, allergens);
            }
        }
    }
}