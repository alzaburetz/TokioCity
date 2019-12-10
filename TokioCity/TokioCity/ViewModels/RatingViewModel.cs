using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Linq;

using Xamarin.Forms;

namespace TokioCity.ViewModels
{
    public class SelectableString: INotifyPropertyChanged
    {
        public string member { get; set; }
        private bool _isselected;
        public bool isSelected
        {
            get => _isselected;
            set
            {
                _isselected = value;
                OnPropertyChanged("isSelected");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public SelectableString(string _member, bool isselected = false)
        {
            member = _member;
            _isselected = isselected;
        }
    }
    public class RatingViewModel
    {
        public ObservableCollection<SelectableString> Selectable { get; set; }
        public Command Select { get; set; }

        public RatingViewModel()
        {
            Selectable = new ObservableCollection<SelectableString>()
            {
                new SelectableString("Прием заказа", true),
                new SelectableString("Качество заказа"),
                new SelectableString("Время доставки"),
                new SelectableString("Работа курьера")
            };
            Select = new Command((selected) =>
            {
                var selected_is =
                Selectable.First(x => x.member == selected.ToString()).isSelected;
                Selectable.First(x => x.member == selected.ToString()).isSelected = !selected_is;
            });
        }

    }
}
