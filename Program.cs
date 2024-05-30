using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Xml.Linq;

namespace PROG2APOEQ1
{
    internal class IngredientDetails//this class will store all the non Key values for the ingredients (everything but the name)
    {
        //------------------------Ingredient Properties--------------------------------
        public float Quantity { get; set; }
        public float origQuantity { get; set; }
        public string Measurement {  get; set; }
        public int Calories { get; set; }
        public string foodGroup { get; set; }
        //------------------------ End of INgredient Properties -------------------------
        //------------------------ INgredient COnstructor -------------------------------
        public void CreateIngredient(float quant, float origquant, string meas, int cal, string group)
        {
            Quantity = quant;
            origQuantity = origquant;
            Measurement = meas;
            Calories = cal;
            foodGroup = group;
        }
    }
    internal class Recipe
    {
        //------------------------Recipe Properties-------------------------------------
        public string RecipeName { get; set; }
        public int NumberIngredients { get; set; }
        public string[] IngredientName { get; set; }
        public float[] IngredientQuant { get; set; }
        public float[] OrigIngredientQuant { get; set; }
        public string[] IngredientMeasure { get; set; }
        public int[] IngredientCal { get; set; }
        public int CalTotal { get; set; }
        public string[] IngredientFoodGroup { get; set; }
        public int NumberSteps { get; set; }
        public string[] Steps { get; set; }
        //------------------------ End of Recipe Properties-------------------------------------
        public void CreateRecipe(string name, int numIng, string[] ingName, float[] ingQuant,float[] origingQuant, string[] ingMeas, int[] ingCal, int calTot, string[] ingFood, int numSteps, string[] stps)
        {
            //------------------------ Recipe Constructos -----------------------------------------
            RecipeName = name;
            NumberIngredients = numIng;
            IngredientName = ingName;
            IngredientQuant = ingQuant;
            IngredientMeasure = ingMeas;
            IngredientCal = ingCal;
            CalTotal = calTot;
            IngredientFoodGroup = ingFood;
            NumberSteps = numSteps;
            Steps = stps;
        }
        //---------------------------- End of Recipe Constructors -------------------------
        //---------------------------- Recipe Mehthods -------------------------------------
        //DisplayRecipe Method
        //Will print the full recipe in a pleasent format
        public void DisplayRecipe()
        {
            Console.WriteLine();
            Console.WriteLine(RecipeName);
            Console.WriteLine("Ingredient List:");
            for (int i = 0; i < NumberIngredients; i++)
            {
                Console.WriteLine((i + 1) + ". " + IngredientQuant[i] + " " + IngredientMeasure[i] + " " + IngredientName[i]);
            }
            Console.WriteLine("Recipe instructions:");
            for (int j = 0; j < NumberSteps; j++)
            {
                Console.WriteLine((j + 1) + ". " + Steps[j]);
            }
        }
        // End of DisplayRecipe Method
        //ScaleQuant method
        //sclaes the quantity of the ingredients by the desired factor
        public void ScaleQuant(float scale)
        {
            for (int i = 0; i < NumberIngredients; i++)
            {
                //seekingspace and settingspace are for units such as cups or teaspoons Eg 6 cups of suger
                string seekingspace = ((IngredientQuant[i]).ToString() + " " + IngredientMeasure[i] + " of " + IngredientName[i]);
                string settingspace = ((IngredientQuant[i] * scale).ToString() + " " + IngredientMeasure[i] + " of " + IngredientName[i]);
                //seekingnospace and settingnospace is for units such as kg or ml Eg 6Kg of cheese
                string seekingnospace = ((IngredientQuant[i]).ToString() + "" + IngredientMeasure[i] + " of " + IngredientName[i]);
                string settingnospace = ((IngredientQuant[i] * scale).ToString() + "" + IngredientMeasure[i] + " of " + IngredientName[i]);
                for (int j = 0; j < NumberSteps; j++)
                {
                    Steps[j] = Steps[j].Replace(seekingspace, settingspace);
                    Steps[j] = Steps[j].Replace(seekingnospace, settingnospace);
                }
                IngredientQuant[i] *= scale;
            }
            Console.WriteLine("Ingredient quantities scaled succesfully");
        }
        //end of the ScaleQuant method
        //ResetQuant method
        //Sets the QUantity values to the inital ones input (relevant after the use of the scale QUant method)
        public void ResetQuant()
        {
            for (int i = 0; i < NumberIngredients; i++)
            {
                //seekingspace and settingspace are for units such as cups or teaspoons Eg 6 cups of suger
                string seekingspace = ((IngredientQuant[i]).ToString() + " " + Ingredientmeasure[i] + " of " + IngredientName[i]);
                string settingspace = ((OrigIngredientQuant[i]).ToString() + " " + Ingredientmeasure[i] + " of " + IngredientName[i]);
                //seekingnospace and settingnospace is for units such as kg or ml Eg 6Kg of cheese
                string seekingnospace = ((IngredientQuant[i]).ToString() + "" + Ingredientmeasure[i] + " of " + IngredientName[i]);
                string settingnospace = ((OrigIngredientQuant[i]).ToString() + "" + Ingredientmeasure[i] + " of " + IngredientName[i]);

                for (int j = 0; j < NumberSteps; j++)
                {
                    Steps[j] = Steps[j].Replace(seekingspace, settingspace);
                    Steps[j] = Steps[j].Replace(seekingnospace, settingnospace);
                }
            }
            IngredientQuant = OrigIngredientQuant;
            Console.WriteLine("Quantities have been reset");
        }
        //End of ResetQuant method
        //---------------------------- End of Recipe Methods ------------------------------
    }
    internal class Program
    {
        //-------------------------------- Start of inputRecipe Method ------------------------
        //This method interacts with the user via the console to put together a Recipe
        public void userRecipe()
        {
            //variables used in the input process
            bool err = true;
            string quant;
            //variables used to hold input data
            string nam;
            int numIng = 0;
            // name input
            Console.WriteLine("What is the title of your recipe?");
            nam = Console.ReadLine();
            //number of ingredients input
            err = true;
            while (err) {
                try
                {
                    err = false;
                    //prompting text:
                    Console.WriteLine("How many ingredients are in your recipe?");
                    //potential error input:
                    numIng = int.Parse(Console.ReadLine());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR - " + ex.Message);
                    err = true;
                }
            }
            //creating the ingredients list
            // Each ingredient will be formed of the folloeing
            // name - quantity - origianl quantity - measurment - calories - foodgroup
            Dictionary<string, float, float, string, int, string> = new Dictionary<string, float, float, string, int, string>()

            //setting size of arrays:
            string[] ingNam = new string[numIng];
            float[] ingQuant = new float[numIng];
            float[] origingQuant = new float[numIng];//the OrigIngredentQuant varible is only used in the ResetQuant Method and is made equal to the INgredientQuant varible in this constructor
            string[] ingMeas = new string[numIng];
            int[] ingCal = new int[numIng];
            string[] ingFodd = new string[numIng];
            //number of ingredients input
            err = true;
            while (err)
            {
                try
                {
                    err = false;
                    //prompting text:

                    //potential error input:

                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERROR - " + ex.Message);
                    err = true;
                }
            }
            //loops through every ingredient in the recipe filling in the various
                for (int i = 0; i < numIng; i++)
                {
                    Console.WriteLine("What is the name of ingredient " + (i+1));
                    try
                    {
                        IngredientName[i] = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR - " + ex.Message);
                    }
                    Console.WriteLine("How much (a number) of ingredient " + (i + 1)+" is used.");
                    try
                    {
                        quant = Console.ReadLine().Replace(",",".");
                        IngredientQuant[i] = float.Parse(quant, CultureInfo.InvariantCulture.NumberFormat);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR - " + ex.Message);
                    }
                    OrigIngredientQuant[i] = IngredientQuant[i];
                    Console.WriteLine("What is the unit of measurment used for ingredient " + (i + 1));
                    try
                    {
                        Ingredientmeasure[i] = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR - " + ex.Message);
                    }
                }
                Console.WriteLine("how many steps are there in your recipe?");
                NumberSteps = int.Parse(Console.ReadLine());
                Steps = new string[NumberSteps];
                for (int j = 0;  j < NumberSteps; j++)
                {
                    Console.WriteLine("what is the instruction for step " + (j+1)+ " (use a comma in decimals Eg 5,55)");
                    Steps[j] = Console.ReadLine();
                }
                Console.WriteLine("Recipe titled " + RecipeName + " succesfully created.");
                
            Recipe HoldRecipe = new Recipe();
            HoldRecipe.CreateRecipe(nam, );
        }
        //--------------------------------------- End of User Recipe Mehtod ------------------------
        static void Main(string[] args)
        {
            String inpt=""; //the input string will hold the text instructions the user inputs
            Recipe recipe = new Recipe();
            recipe.CreateRecipe();
            while (inpt.ToLower() != "exit")// This will loop until the user types the Exit Command at which point the program will end
            {
                Console.WriteLine();
                Console.WriteLine("Please use any of the following comands:");
                Console.WriteLine("Help, Display, Scale, Reset Quant, Clear or Exit");
                inpt = Console.ReadLine();
                if (inpt.ToLower() == "help")
                {
                    //The help comand describes what each of the other functions does
                    Console.WriteLine("Display -> Will output your recipe in a structured format");
                    Console.WriteLine("Scale -> will prompt you what factor yu would like to scale by the ingredient quantities will then be scaled by the selected factor and the instructions will be searched and updated to match");
                    Console.WriteLine("Rese Quant -> will reset the ingredeint quantities and the instructions back to their original values");
                    Console.WriteLine("Cear -> Will wipe your recipe and prompt you to create a new one");
                    Console.WriteLine("Exit -> Will Shutdown the program closing the comand line interface");
                }else if (inpt.ToLower() == "display")
                {
                    //The display command calls up the dislay funciton
                    recipe.DisplayRecipe();
                }else if(inpt.ToLower() == "scale")
                {
                    //The Scale Quant funciton prompts the user as to how much they want to scale the quantity of the ingredient and then calls up the scale function
                    Console.WriteLine("What factor would you like the quantity of ingredients to be scaled by (enter Half, Double or Triple)");
                    string txtscl = Console.ReadLine();
                    if ((txtscl.ToLower() == "half"))
                    {
                        recipe.ScaleQuant(0.5F);
                    }
                    else if (txtscl.ToLower() == "double")
                    {
                        recipe.ScaleQuant(2);
                    }
                    else if (txtscl.ToLower() == "triple")
                    {
                        recipe.ScaleQuant(3);
                    }else
                    {
                        Console.WriteLine("That is not a valid scaling factor");
                    }
                }else if( inpt.ToLower() == "reset quant")
                {
                    //The Reset Quant Comand calls up the rest funciton
                    recipe.ResetQuant();

                }else if(inpt.ToLower() == "clear")
                {
                    //The Clear Comand calls up the clear function
                    

                }else if (inpt.ToLower() == "exit")
                {
                    //the Exit comand Exits the porgram
                    Console.WriteLine("Shutting down");
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
