using System;
using UIKit;

using UIAnim.AnimLibrary;
using CoreGraphics;

namespace UIAnim
{
    public partial class ViewController : UIViewController
    {
        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.

            CustomShapeLayer.DrawCircleShape(CircleView, (nfloat)(CircleView.Frame.Width / 2.0));
            CustomShapeLayer.DrawCircleShape(LabelView, (nfloat)(LabelView.Frame.Width / 2.0));

            var xPanAnimation = new CustomPanAnimation(CircleView);
            var xPanAnimation1 = new CustomPanAnimation(LabelView);
            var ySliderAnimation = new CustomSlideAnimation(ImageView, Axis.Y);
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
