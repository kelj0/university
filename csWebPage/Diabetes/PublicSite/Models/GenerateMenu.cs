using PublicSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PublicSite.Models
{
    public class ViewModels
    {
        public string ErrorMessage { get; set; }
        public int NumberOfMealsInTale;
        public List<Namirnica> Carbs { get; set; }
        public List<Namirnica> Fats { get; set; }
        public List<Namirnica> Proteins { get; set; }
        public List<NazivObroka> MealNames { get; set; }
        public List<Jedinica> MeasuringUnits { get; set; }
        public List<TipNamirnice> FoodTypes { get; set; }
        public int NumberOfMeals;
    }


    public class GenerateMenu
    {
        public Namirnica Namirnica { get; set; }
        public TipNamirnice TipNamirnice{ get; set; }
        public Jedinica MeasuringUnit { get; set; }
        public KombinacijaDetalji KombinacijaDetalji { get; set; }
        public List<KombinacijaDetalji> Kombinacijas { get; set; }
        public Kombinacija MealCombination { get; set; }
        public Obrok Meal { get; set; }
        public Meni Menu { get; set; }
        public string MealName { get; set; }
        public ViewModels ViewModels { get; set; }
    }
}