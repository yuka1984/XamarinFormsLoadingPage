﻿using System.ComponentModel;
using System.Linq;
using App5.iOS;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(LoadingPageRenderer))]

namespace App5.iOS
{
    public class LoadingPageRenderer : PageRenderer
    {
        private UIActivityIndicatorView _indicator;

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
                Indicatorconstraint(_indicator);
            }
            Element.PropertyChanged += OnHandlePropertyChanged;
            UpdateIsBusy();
        }

        protected virtual UIActivityIndicatorView CreateIndicator(UIView view)
        {
            return new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
        }

        protected virtual void Indicatorconstraint(UIActivityIndicatorView indicatorView)
        {
            indicatorView.CenterXAnchor.ConstraintEqualTo(View.CenterXAnchor).Active = true;
            indicatorView.CenterYAnchor.ConstraintEqualTo(View.CenterYAnchor, -30).Active = true;
            indicatorView.TranslatesAutoresizingMaskIntoConstraints = false;
            indicatorView.UserInteractionEnabled = false;
        }

        public override void WillAnimateRotation(UIInterfaceOrientation toInterfaceOrientation, double duration)
        {
            base.WillAnimateRotation(toInterfaceOrientation, duration);
            UpdateIsBusy();
        }

        private void UpdateIsBusy()
        {
            var isbusy = (Element as Page).IsBusy;
            if (!isbusy)
                return;

            var view = View.Subviews.First(x => x != _indicator);
            var distance = view.Frame.Width > view.Frame.Height ? view.Frame.Width : view.Frame.Height;
            view.Frame = new CGRect(view.Frame.X + distance, view.Frame.Y, view.Frame.Width,
                view.Frame.Height);
            view.Alpha = 0;
        }


        private void OnHandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsBusy")
            {
                var isbusy = (Element as Page).IsBusy;
                var view = View.Subviews.First(x => x != _indicator);
                var distance = view.Frame.Width > view.Frame.Height ? view.Frame.Width : view.Frame.Height;
                if (isbusy)
                {
                    UIView.Animate(0.3, 1, UIViewAnimationOptions.CurveEaseIn
                        , () =>
                        {
                            view.Frame = new CGRect(view.Frame.X + distance, view.Frame.Y,
                                view.Frame.Width, view.Frame.Height);
                            view.Alpha = 0;
                        }
                        , () => _indicator?.StartAnimating());
                }
                else
                {
                    _indicator?.StopAnimating();
                    UIView.Animate(0.3, 0, UIViewAnimationOptions.CurveEaseOut
                        , () =>
                        {
                            view.Frame = new CGRect(view.Frame.X - distance, view.Frame.Y,
                                view.Frame.Width, view.Frame.Height);
                            view.Alpha = 1;
                        }
                        , null);
                }
            }
        }
    }
}