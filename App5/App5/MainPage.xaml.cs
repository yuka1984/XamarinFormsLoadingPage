using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App5
{
    public partial class MainPage : ContentPage
    {
        private CancellationTokenSource cancellTokenSource;

        public MainPage()
        {
            InitializeComponent();
            Button.Clicked += ButtonOnClicked;
            PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsBusy")
                    Status.Text = IsBusy.ToString();
            };
        }

        private void ButtonOnClicked(object sender, EventArgs eventArgs)
        {
            if (IsBusy)
            {
                IsBusy = false;
                cancellTokenSource?.Cancel();
            }
            else
            {
                cancellTokenSource = new CancellationTokenSource();
                IsBusy = true;
                Task.Delay(2500, cancellTokenSource.Token)
                    .ContinueWith(task =>
                    {
                        if (task.IsCompleted)
                            Device.BeginInvokeOnMainThread(() => { IsBusy = false; });
                    });
            }
        }
    }
}