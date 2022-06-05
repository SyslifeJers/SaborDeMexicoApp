using SaborDeMexico.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SaborDeMexico.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}