using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace PROG2APOEQ1
{
    delegate void CalDelegate();
    internal class IngredientDetails//this class will store all information for each of the ingredietns
    {
        //------------------------Ingredient Properties--------------------------------
        public string Name { get; set; }   
        public float Quantity { get; set; }
        public float origQuantity { get; set; }
        public string Measurement {  get; set; }
        public int Calories { get; set; }
        public string foodGroup { get; set; }
        //------------------------ End of INgredient Properties -------------------------
        //------------------------ INgredient COnstructor -------------------------------
        public void CreateIngredient(string nam, float quant, float origquant, string meas, int cal, string group)
        {
            Name = nam;
            Quantity = quant;
            origQuantity = origquant;
            Measurement = meas;
            Calories = cal;
            foodGroup = group;
        }
        //----------------------- end of ingredient constructor ----------------------------
        public string display()
        {
            string otpt = (Quantity.ToString() + " " + Measurement + " of " + Name + ". This is "+Calories.ToString()+ " calories in the " + foodGroup+ " food group." );
            return otpt;
        }
    }
    internal class Recipe
    {
        //------------------------Recipe Properties-------------------------------------
        public string recipeName { get; set; }
        public Dictionary<string, IngredientDetails> recipeIngredients { get; set; }
        public int recipeCalTotal { get; set; }
        public List<string> recipeSteps { get; set; }
        //------------------------ End of Recipe Properties-------------------------------------
        public void CreateRecipe(string name, Dictionary<string, IngredientDetails> ings, int calTot, List<string> stps)
        {
            //------------------------ Recipe Constructos -----------------------------------------
            recipeName = name;
            recipeIngredients = ings;
            recipeCalTotal = calTot;
            recipeSteps = stps;
        }
        //---------------------------- End of Recipe Constructors -------------------------
        //---------------------------- Recipe Mehthods -------------------------------------
        //DisplayRecipe Method
        //Will print the full recipe in a pleasent format
        public void DisplayRecipe()
        {

            int count = 0;
            Console.WriteLine();
            Console.WriteLine(recipeName);
            Console.WriteLine("Ingredient List:");
            foreach (var pair in recipeIngredients)
            {
                count ++;
                Console.WriteLine(count.ToString()+". "+pair.Value.display());
            }
            Console.WriteLine("Steps:");
            count = 0;
            foreach (var stp in recipeSteps)
            {
                count ++;
                Console.WriteLine(count.ToString() + ". " + stp);
            }
            Console.WriteLine("Calories:");
            Console.WriteLine(recipeCalTotal.ToString());



            
        }
        // End of DisplayRecipe Method
        //ScaleQuant method
        //sclaes the quantity of the ingredients by the desired factor
        public void ScaleQuant(float scale)
        {
            /*for (int i = 0; i < NumberIngredients; i++)
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
            Console.WriteLine("Ingredient quantities scaled succesfully");*/
        }
        //end of the ScaleQuant method
        //ResetQuant method
        //Sets the QUantity values to the inital ones input (relevant after the use of the scale QUant method)
        public void ResetQuant()
        {/*
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
            Console.WriteLine("Quantities have been reset");*/
        }
        //End of ResetQuant method
        //---------------------------- End of Recipe Methods ------------------------------
    }
    internal class Program
    {
        //high call method
        public static void HighCal()
        {
            Console.WriteLine("this is considered a high calory recipe. Please be aware of your caloric intake.");
        }
        //-------------------------------- Start of inputRecipe Method ------------------------
        //This method interacts with the user via the console to put together a Recipe
        public static Recipe userRecipe()
        {
            Dictionary<string, IngredientDetails> Ingredients = new Dictionary<string, IngredientDetails>();


            //variables used in the input process
            bool err = true;//used for the advanced error handling do identify whether an error occured
            bool cont = true;//used to continue or halt adding new ingredients
            bool legit = true;//ensures users input legitimate replies to queries
            string ans;//stores the answer to queries
            //variables used to hold input data
            string nam = "";
            string ingname = "";
            float ingquant = 0;
            float origingquant = 0;
            string meas = "";
            int cal = 0;
            string fgroup = "";
            int caltot = 0;
            // name input
            Console.WriteLine("What is the title of your recipe?");
            nam = Console.ReadLine().ToLower();
            //creates the instructions collection
            List<string> steps = new List<string>();
            cont = true;
            while (cont) {
                //input name of ingredient
                //prompting text:
                Console.WriteLine("what is the name of the ingredient?");
                //input:
                ingname = Console.ReadLine().ToLower();
                err = true;
                while (err)
                {
                    try
                    {
                        err = false;
                        //prompting text:
                        Console.WriteLine("what is the quantity needed of the ingredient?");
                        //potential error input:
                        ingquant = float.Parse(Console.ReadLine().Replace(",","."));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR - " + ex.Message);
                        err = true;
                    }
                    origingquant = ingquant;
                }
                //input measuremeant for ingredient
                //prompting text:
                Console.WriteLine("what is the measuremeant used for the ingredient");
                //input:
                meas = Console.ReadLine();
                //input calories in the ingredient
                err = true;
                while (err)
                {
                    try
                    {
                        err = false;
                        //prompting text:
                        Console.WriteLine("How many calories are in this ingredient?");
                        //potential error input:
                        cal = int.Parse(Console.ReadLine());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("ERROR - " + ex.Message);
                        err = true;
                    }

                }
                //input what food group the ingredient is in
                Console.WriteLine("What food group is the ingredient in?");
                fgroup = Console.ReadLine();
                //creates an instance of the ingredientdetails class
                IngredientDetails holder = new IngredientDetails();
                holder.CreateIngredient(ingname, ingquant, origingquant, meas, cal, fgroup);
                //Adds to the ingredients collection
                Ingredients.Add(ingname,holder);
                //adds to the calory total
                caltot += cal;
                //Determines wheather there are more ingredients
                Console.WriteLine("Would You like to add another ingredient? (yes/no)");
                legit = false;
                while (legit==false) {
                    ans = Console.ReadLine().ToLower();
                    if (ans == "yes")
                    {
                        cont = true;
                        legit = true;
                    }else if (ans == "no")
                    {
                        cont = false;
                        legit = true;
                    }
                    else
                    {
                        legit = false;
                        Console.WriteLine("That was not a legitimate reponse. Please input yes to continue and no to stop adding ingredients.");
                    }
                }


            }
            //adds the steps to the recipe
            cont = true;
            while (cont)
            {
                Console.WriteLine("Please input the instructions for the next step in the recipe");
                steps.Add(Console.ReadLine());
                Console.WriteLine("That was step "+steps.Count);
                Console.WriteLine("Would You like to add another step? (yes/no)");
                legit = false;
                while (legit == false)
                {
                    ans = Console.ReadLine().ToLower();
                    if (ans == "yes")
                    {
                        cont = true;
                        legit = true;
                    }
                    else if (ans == "no")
                    {
                        cont = false;
                        legit = true;
                    }
                    else
                    {
                        legit = false;
                        Console.WriteLine("That was not a legitimate reponse. Please input yes to continue and no to stop adding steps.");
                    }
                }
            }
            //finalizes the recipe
            Console.WriteLine("Recipe titled " + nam + " succesfully created.");
            Recipe HoldRecipe = new Recipe();
            HoldRecipe.CreateRecipe(nam, Ingredients, caltot,steps );
            return HoldRecipe;
        }
        //--------------------------------------- End of User Recipe Mehtod ------------------------
        static void Main(string[] args)
        {
            //used to print recipes
            Recipe printRecipe = new Recipe();
            //Stores all the recipes
            Dictionary<string, Recipe> Book = new Dictionary<string, Recipe>();
            //Creates some stock recipes to fill the system
            IngredientDetails stockIngredientdetailsone = new IngredientDetails();
            IngredientDetails stockIngredientdetailstwo = new IngredientDetails();
            IngredientDetails stockIngredientdetailsthree = new IngredientDetails();
            IngredientDetails astockIngredientdetailsone = new IngredientDetails();
            IngredientDetails astockIngredientdetailstwo = new IngredientDetails();
            Dictionary<string, IngredientDetails> stockIngredients = new Dictionary<string, IngredientDetails>();
            Dictionary<string, IngredientDetails> astockIngredients = new Dictionary<string, IngredientDetails>();
            List<string> stockSteps = new List<string>();
            List<string> astockSteps = new List<string>();
            Recipe stockRecipe = new Recipe();
            Recipe astockRecipe = new Recipe();
            //Stock Recipe One
            stockIngredientdetailsone.CreateIngredient("apple",100, 100, "g", 52, "fruit");
            stockIngredients.Add(stockIngredientdetailsone.Name, stockIngredientdetailsone);
            stockIngredientdetailstwo.CreateIngredient("banana",150, 150, "g", 134, "fruit");
            stockIngredients.Add(stockIngredientdetailstwo.Name, stockIngredientdetailstwo);
            stockIngredientdetailsthree.CreateIngredient("pineapple",100, 100, "g", 48, "fruit");
            stockIngredients.Add(stockIngredientdetailsthree.Name, stockIngredientdetailsthree);
            stockSteps.Add("Add the apple to a bowl");
            stockSteps.Add("Add the banana to the bowl");
            stockSteps.Add("Add the pineapple to the bowl");
            stockSteps.Add("Mix all the ingredients and serve");
            stockRecipe.CreateRecipe("fruit salad",stockIngredients,234,stockSteps);
            Book.Add("fruit salad",stockRecipe);
            //Stock Recipe two
            astockIngredientdetailsone.CreateIngredient("grated Cheese",2, 2, "cups", 911, "dairy");
            astockIngredients.Add(astockIngredientdetailsone.Name, astockIngredientdetailsone);
            astockIngredientdetailstwo.CreateIngredient("cake",500, 500, "g", 1485, "starch");
            astockIngredients.Add(astockIngredientdetailstwo.Name, astockIngredientdetailstwo);
            astockSteps.Add("Melt the Cheese");
            astockSteps.Add("Pour the melted cheese over the cake");
            astockSteps.Add("Let it cool and serve");
            astockRecipe.CreateRecipe("cheese cake", astockIngredients, 2396, astockSteps);
            Book.Add("cheese cake",astockRecipe);
            //User interaction
            String inpt=""; //the input string will hold the text instructions the user inputs
            while (inpt.ToLower() != "exit")// This will loop until the user types the Exit Command at which point the program will end
            {
                Console.WriteLine();
                Console.WriteLine("Please use any of the following comands:");
                Console.WriteLine("Help, Recipe List, View Recipe, Add Recipe, Scale ('Recipe Name') Delete ('Recipe Name'), Exit");
                inpt = Console.ReadLine();
                if (inpt.ToLower() == "help")
                {
                    //The help comand describes what each of the other functions does
                    Console.WriteLine("Recipe List -> will display an alphabetical list of all the recipes");
                    Console.WriteLine("View Recipe -> you will be prompted to provide a recipe name the selected recipe will then be fully displayed");
                    Console.WriteLine("Add Recipe -> will prompt you to add your own recipe to the system.");
                    Console.WriteLine("Scale Recipe -> you will be prompted to provide a recipe name then you will be prompted with options to scale the quantity of ingredients in the selected recipe");
                    Console.WriteLine("Delete Recipe -> you will be prompted to provide a recipe name the selected recipe will then be removed from the system");
                    Console.WriteLine("Exit -> Will Shutdown the program closing the comand line interface");
                }else if (inpt.ToLower() == "recipe list")
                {
                    //the reipe list command displayes all the names of the recipes in the system
                    var sortesDict = Book.OrderBy(KeyValuePair => KeyValuePair.Key);
                    Console.WriteLine("Recipe List:");
                    foreach(var alph in sortesDict)
                    {
                        Console.WriteLine($"{alph.Key}");
                    }

                }
                else if (inpt.ToLower() == "view recipe")
                {
                    //The view recipe command allows the suer to view a specific recipe
                    CalDelegate caldelegate = new CalDelegate(HighCal);
                    Console.WriteLine("What is the name of the recipe you would like to view");
                    inpt = Console.ReadLine();
                    if (Book.ContainsKey(inpt))
                    {
                        printRecipe = Book[inpt];
                        printRecipe.DisplayRecipe();
                        if (Book[inpt].recipeCalTotal > 300)
                        {
                            caldelegate();//warns user about high caloric intake
                        }
                    }
                    else
                    {
                        Console.WriteLine("that is not a recognized recipe name");
                    }
                    


                }
                else if (inpt.ToLower() == "add recipe")
                {
                    //The add recipe Command allows the user ot add their own recipe to the system
                    stockRecipe = userRecipe();
                    Book.Add(stockRecipe.recipeName,stockRecipe);
                }
                else if(inpt.ToLower() == "scale recipe")
                {
                    //The Scale Quant funciton prompts the user as to how much they want to scale the quantity of the ingredient and then calls up the scale function
                    /*Console.WriteLine("What factor would you like the quantity of ingredients to be scaled by (enter Half, Double or Triple)");
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
                    }*/
                }else if( inpt.ToLower() == "delete recipe")
                {
                    //The delete recipe Comand removes a recipe from the book dictionary
                    Console.WriteLine("What is the name of the recipe you would like to view");
                    inpt = Console.ReadLine();
                    if (Book.ContainsKey(inpt))
                    {
                        Book.Remove(inpt);
                        Console.WriteLine(inpt+" Recipe succesfully Removed.");
                    }
                    else
                    {
                        Console.WriteLine("that is not a recognized recipe name");
                    }


                }
                else if (inpt.ToLower() == "exit")
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
