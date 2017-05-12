using System.ComponentModel;
using Android.Animation;
using Android.Views;
using App5.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AProgressBar = Android.Widget.ProgressBar;

[assembly: ExportRenderer(typeof(ContentPage), typeof(LoadingPageRenderer))]

namespace App5.Droid
{
    public class LoadingPageRenderer : PageRenderer
    {
        private AProgressBar _progress;

        private ValueAnimator animator;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (_progress == null)
            {
                _progress = new AProgressBar(Context, null, Android.Resource.Attribute.ProgressBarStyleSmall)
                {
                    Indeterminate = true
                };
                AddView(_progress);
                _progress.Visibility = ViewStates.Invisible;
            }

            if (animator == null)
            {
                animator = ValueAnimator.OfFloat(0f, 1f);
                animator.SetDuration(300);
                animator.Update += (s, a) =>
                {
                    var view = GetChildAt(1);
                    var width = view.Width;
                    var height = view.Height;
                    var c = (float) a.Animation.AnimatedValue;
                    view.Left = (int) (width * c);
                    view.Right = view.Left + width;

                    _progress.Alpha = c;
                    System.Diagnostics.Debug.WriteLine(c);
                    ;
                };
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            if (!changed)
                return;
            base.OnLayout(changed, l, t, r, b);
            float z = 0;
            for (var i = 0; i < ChildCount; ++i)
            {
                var view = GetChildAt(i);
                if (view != _progress && z < view.GetZ())
                    z = view.GetZ();
            }

            _progress.SetZ(z + 1);
            var width = r - l;
            var woffset = (width - 100) / 2;

            var hoffset = (b - t) / 10;

            _progress.Layout(l + woffset, t + hoffset, r - woffset, t + hoffset * 4);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == nameof(Element.IsBusy))
            {
                var page = (Page) sender;
                _progress.Visibility = page.IsBusy ? ViewStates.Visible : ViewStates.Invisible;

                for (var i = 0; i < ChildCount; ++i)
                {
                    var view = GetChildAt(i);
                    if (view != _progress)
                    {
                        if (animator.IsStarted)
                            animator.Pause();
                        if (page.IsBusy)
                        {
                            _progress.Alpha = 0;
                            animator.StartDelay = 1000;
                            animator.Start();
                        }
                        else
                        {
                            animator.StartDelay = 0;
                            animator.Reverse();
                        }
                    }
                }
            }
        }
    }
}