using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using TokioCity.Models;

namespace TokioCity.Views.Components
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonBar : ContentView
    {
        public ObservableCollection<CategoryButton> buttons { get; set; }

        public ButtonBar()
        {
            buttons = new ObservableCollection<CategoryButton>();
            buttons.Add(new CategoryButton("Избранное", "favorite.png"));
            buttons.Add(new CategoryButton("Ланчи", "lunch.png"));
            buttons.Add(new CategoryButton("Паста", "pasta.png"));
            buttons.Add(new CategoryButton("Суши", "sushi.png"));
            buttons.Add(new CategoryButton("Пицца", "pizza.png"));
            buttons.Add(new CategoryButton("Роллы", "roll.png"));
            buttons.Add(new CategoryButton("Wok", "wok.png"));
            buttons.Add(new CategoryButton("Бургеры", "burger.png"));
            buttons.Add(new CategoryButton("Пироги", "pies.png"));
            buttons.Add(new CategoryButton("Напитки", "drink.png"));
            buttons.Add(new CategoryButton("Наборы", "set.png"));
            buttons.Add(new CategoryButton("Детское", "kids.png"));
            buttons.Add(new CategoryButton("Пиццета", "pizzetta.png"));
            buttons.Add(new CategoryButton("Десерты", "dessert.png"));
            buttons.Add(new CategoryButton("Соусы", "sauce.png"));
            buttons.Add(new CategoryButton("Салаты", "salad.png"));
            buttons.Add(new CategoryButton("Горячее", "salad.png"));
            InitializeComponent();
        }
    }
}