using SaborDeMexico.Models;
using SaborDeMexico.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace SaborDeMexico.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        bool verOcultar = false;
        public bool VerOcultar
        {
            get { return verOcultar; }
            set { SetProperty(ref verOcultar, value); }
        }
        bool verMostrar = true;
        public bool VerMostrar
        {
            get { return verMostrar; }
            set { SetProperty(ref verMostrar, value); }
        }

        public void Ver()
        {
            if (VerOcultar)
            {
                VerMostrar = true;
                VerOcultar = false;

            }
            else
            {
                VerMostrar = false;
                VerOcultar = true;
            }
        }
        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #endregion
    }
}
