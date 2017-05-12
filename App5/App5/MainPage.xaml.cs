using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Threading;

namespace App5
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
            Button.Clicked += ButtonOnClicked;
            PropertyChanged += (sender, e) => {

                if(e.PropertyName == "IsBusy"){
                    Status.Text = IsBusy.ToString();
                }
            };
		}

        private CancellationTokenSource cancellTokenSource;
	    private void ButtonOnClicked(object sender, EventArgs eventArgs)
	    {
            if(IsBusy)
            {
                IsBusy = false;
                cancellTokenSource?.Cancel();
            }
            else
            {
                cancellTokenSource = new CancellationTokenSource();
				IsBusy = true;
				Task.Delay(2500, cancellTokenSource.Token).ContinueWith(task =>
				{
                    if(task.IsCompleted)
                    {
                        Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
	                    {
	                        this.IsBusy = false;
	                    });
                    }
				});
			}

	    }
	}
}
