using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;

using TokioCity.Models;
using TokioCity.Services;
using Xamarin.Forms;
using LiteDB;

namespace TokioCity.ViewModels.RestrauntViewModels
{
    public class RestrauntListViewModel:BaseViewModel
    {
        public ObservableCollection<Restraunt> Restraunts { get; set; }

        public Command LoadRestraunts { get; set; }
        public Command LoadrestrauntsFromDb { get; set; }

        public RestrauntListViewModel()
        {
            Restraunts = new ObservableCollection<Restraunt>();

            LoadrestrauntsFromDb = new Command(() =>
            {
                var restraunts = DataBase.GetAllStream<Restraunt>("Restraunts");
                var ie = restraunts.GetEnumerator();
                while (ie.MoveNext())
                {
                    Restraunts.Add(ie.Current);
                }
            });
        }

        public RestrauntListViewModel(System.Net.Http.HttpClient client): this()
        {

            
            LoadRestraunts = new Command(async () =>
            {
                Restraunts.Clear();
                if (DataBase.GetRecordCount<Restraunt>("Restraunts") == 0)
                {
                    var restraunts = await RequestHelper.GetData<List<Restraunt>>(client, "data/app_addresses.php?version=");
                    foreach (var rest in restraunts)
                    {
                        var restraunt = MapBuilder.CalculateRestrauntDistance(rest);
                        Restraunts.Add(restraunt);
                    }
                    DataBase.WriteAll<Restraunt>("Restraunts", restraunts);
                }
                else
                {
                    LoadrestrauntsFromDb.Execute(null);
                }
            });
        }
    }
}
