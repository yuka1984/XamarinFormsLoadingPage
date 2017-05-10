using System;
using App5.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using UIKit;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Timers;

[assembly: ExportRenderer(typeof(ContentPage), typeof(LoadingPageRenderer))]
namespace App5.iOS
{
	public class LoadingPageRenderer : PageRenderer
	{
		UIActivityIndicatorView _indicator;

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			Element.PropertyChanged -= OnHandlePropertyChanged;
			_indicator.RemoveFromSuperview();
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			if (_indicator == null)
			{
				_indicator = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
				View.AddSubview(_indicator);
				_indicator.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
				_indicator.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor, -100).Active = true;
				_indicator.TranslatesAutoresizingMaskIntoConstraints = false;
				_indicator.UserInteractionEnabled = false;
			}
			Element.PropertyChanged += OnHandlePropertyChanged;
		}

		void OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "IsBusy")
			{
				if ((Element as Page).IsBusy)
				{
					_indicator?.StartAnimating();
				}
				else
				{
					_indicator?.StopAnimating();
				}
			}
		}
}
}
