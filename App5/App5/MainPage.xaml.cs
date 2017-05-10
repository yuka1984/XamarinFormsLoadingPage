using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Button.Clicked += ButtonOnClicked;
		}

	    private void ButtonOnClicked(object sender, EventArgs eventArgs)
	    {
	        IsBusy = !IsBusy;
	    }
	}
}
