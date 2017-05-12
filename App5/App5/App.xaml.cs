using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App5
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            var tabbed = new TabbedPage();
            tabbed.Children.Add(new MainPage(){Title = "page1"});
            tabbed.Children.Add(new MainPage(){Title = "page2"});
			MainPage = tabbed;
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
