using GuardkeyV01.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace GuardkeyV01.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        private Note _record;

        public INavigation Navigation { get; set; }

        public Note Note
        {
            get { return _record; }
            set { _record = value; OnPropertyChanged(); }

        }

        bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                SetProperty(ref isBusy, value);
            }
        }


        protected bool SetProperty<T>(ref T backingRecord, T value, [CallerMemberName] string propertyName = "", Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingRecord, value))
                return false;
            backingRecord = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
