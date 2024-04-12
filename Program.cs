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
        public string RecipeName { get; set; }
        public int NumberIngredients { get; set; }   
        public string[] IngredientName {  get; set; }
        public float[] IngredientQuant {  get; set; }
        public float[] OrigINgredientQuant { get; set; }
        public string[] Ingredientmeasure {  get; set; }
        public int NumberSteps { get; set; }    
        public string[] StepDescriptions { get; set; }
        //------------------------ End of Recipe Properties-------------------------------------
        public void CreateRecipe()
        {
        //------------------------ Recipe Constructos -----------------------------------------
            Console.WriteLine("What is the title of your recipe?");
            RecipeName = Console.ReadLine();
            Console.WriteLine("How many ingredients are in your recipe?");
            NumberIngredients = int.Parse(Console.ReadLine());
            IngredientName = new string[NumberIngredients];
            IngredientQuant = new float[NumberIngredients];
            OrigINgredientQuant = new float[NumberIngredients];//the OrigIngredentQuant varible is only used in the ResetQuant Method and is made equal to the INgredientQuant varible in this constructor
            Ingredientmeasure = new string[NumberIngredients];
            for (int i = 0; i < NumberIngredients; i++)
            {
                Console.WriteLine("What is the name of ingredient " + (i+1));
                IngredientName[i] = Console.ReadLine();
                Console.WriteLine("How much (a number) of ingredient " + (i + 1)+" is used");
                IngredientQuant[i] = int.Parse(Console.ReadLine());
                OrigINgredientQuant[i] = IngredientQuant[i];
                Console.WriteLine("What is the measurment used for ingredient " + (i + 1));
                Ingredientmeasure[i] = Console.ReadLine(); 
            }
            Console.WriteLine("how many steps are there in your recipe?");
            NumberSteps = int.Parse(Console.ReadLine());
            StepDescriptions = new string[NumberSteps];
            for (int j = 0;  j < NumberSteps; j++)
            {
                Console.WriteLine("what is the instruction for step " + (j+1));
                StepDescriptions[j] = Console.ReadLine();
            }
            Console.WriteLine("Recipe titled " + RecipeName + " succesfully created.");

        }
        //---------------------------- End of Recipe Constructors -------------------------
        //---------------------------- Recipe Mehthods -------------------------------------

        //Recipe Display Method
        // the display method will print the full recipe in a pleasent format
        public void DisplayRecipe() 
        {
            Console.WriteLine("TEST");
            Console.WriteLine(RecipeName);
            Console.WriteLine("Ingredient List:");
            for (int i = 0;i < NumberIngredients; i++)
            {
                Console.WriteLine((i+1)+". " + IngredientQuant[i]+" " + Ingredientmeasure[i]+" " + IngredientName[i]);
            }
            Console.WriteLine("Recipe instructions:");
            for (int j = 0;j < NumberSteps; j++)
            {
                Console.WriteLine((j+1)+". " + StepDescriptions[j]);
            }
            
        }
        // End of Recipe DIsplay Method

        //---------------------------- End of Recipe Methods ------------------------------



    }
    internal class Program
    {
        static void Main(string[] args)
        {
            String inpt=" "; //the input string will hold the text instructions the user inputs
            Recipe recipe = new Recipe();
            recipe.CreateRecipe();
            while (inpt != "Exit")
            {
                Console.WriteLine();
                Console.WriteLine("Please use any of the following comands:");
                Console.WriteLine("Help, Display, Scale Quant, Reset Quant, Clear or Exit");
                inpt = Console.ReadLine();
                if (inpt == "Help")
                {
                    //The help comand describes what each of the other functions does

                }else if (inpt == "Display")
                {
                    //The display command calls up the dislay funciton
                    recipe.DisplayRecipe();

                }else if(inpt == "Scale Quant")
                {
                    //The Scale Quant funciton prompts the user as to how much they want to scale the quantity of the ingredient and then calls up the scale function

                }else if( inpt =="Reset Quant")
                {
                    //The Reset Quant Comand calls up the rest funciton

                }else if(inpt == "Clear")
                {
                    //The Clear Comand calls up the clear function

                }else if (inpt == "Exit")
                {
                    //the Exit comand Exits the porgram
                }
                else
                {
                    //if the given comand is not recognized the user will be prompted to input a recognized comand
                    Console.WriteLine(inpt+" is not a recognized comand.");
                }
            }
            
        }
    }
}
