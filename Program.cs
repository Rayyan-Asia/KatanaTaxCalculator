using Katana_Tax_Calculator;
using KatanaTaxCalculator;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http.Headers;
using System.Numerics;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Price_Calculator_Kata
{
    public class Program
    {
        public static void Main()
        {

            while (true)
            {
                Console.WriteLine("Please enter the product UPC or q to exit the loop");
                var input = Console.ReadLine().ToLower().Replace(" ", "");
                if(input == "q")
                {
                    Console.WriteLine("Thanks for using our program.");
                    return;
                }
                else
                {
                    try
                    {
                        int upc = int.Parse(input);
                        var productService = Factory.CreateProductService();
                        var calculator = Factory.CreateCalculator();
                        var printer = Factory.CreatePrinter();
                        if (productService.DoesProductExist(upc))
                        {
                            var results = calculator.CalculateAndSaveResults(upc);
                            printer.PrintPriceCalculations(results);
                        }
                        else
                        {
                            Console.WriteLine("Product not found!! Please try again.");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Try entering a number this time");
                    }
                }
            }
        }

    }
}
