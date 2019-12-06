using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;

using Xamarin.Forms;
using LiteDB;

using TokioCity.Models;

namespace TokioCity.ViewModels.RestrauntViewModels
{
    public class RestrauntViewModel: BaseViewModel
    {
        public Restraunt Restraunt { get; set; }
        public Command LoadRestraunt { get; set; }
        private int id;

        public RestrauntViewModel(int id)
        {
            Restraunt = new Restraunt();
            this.id = id;
            LoadRestraunt = new Command((idRest) =>
            {
                this.id = Convert.ToInt32(idRest);
                var test = DataBase.GetItem<Restraunt>("Restraunts", Query.All());
                Restraunt = DataBase.GetItem<Restraunt>("Restraunts", Query.Where("id", x=> x.AsInt32 == this.id));
            });
        }


    }
}
