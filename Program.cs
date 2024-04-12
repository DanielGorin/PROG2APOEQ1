using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;

namespace PROG2APOEQ1
{
    internal class Recipe
    {
        //------------------------Recipe Properties-------------------------------------
        public string Name { get; set; }
        public int NumberIngredients { get; set; }   
        public string[] IngredientName {  get; set; }
        public float[] IngredientQuant {  get; set; }
        public string[] Ingredientmeasure {  get; set; }
        public int NumberSteps { get; set; }    
        public string[] StepDescriptions { get; set; }
        //------------------------ ENd of Recipe Properties-------------------------------------
        public void CreateRecipe()
        {
            Console.WriteLine("What is the title of your recipe?");
            Name = Console.ReadLine();
            Console.WriteLine("How many ingredients are in your recipe?");
            NumberIngredients = int.Parse(Console.ReadLine());
            IngredientName = new string[NumberIngredients];
            IngredientQuant = new float[NumberIngredients];
            Ingredientmeasure = new string[NumberIngredients];
            for (int i = 0; i < NumberIngredients; i++)
            {
             
            }
        }





    }
    internal class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
