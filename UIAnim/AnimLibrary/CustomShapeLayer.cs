using System;
using UIKit;
using Foundation;
using CoreAnimation;
using CoreGraphics;

namespace UIAnim.AnimLibrary
{
    public static class CustomShapeLayer
    {
        public static void DrawCircleShape(UIView view, nfloat radius)
        {
            var circlePath = new UIBezierPath();
            CGPoint center = new CGPoint(view.Frame.Width / 2.0, view.Frame.Height / 2.0);
            circlePath.AddArc(center, radius, 0, (nfloat)(2 * Math.PI), true);

            var shapeLayer = new CAShapeLayer();
            shapeLayer.Path = circlePath.CGPath;
            shapeLayer.FillColor = UIColor.Red.CGColor;

            view.Layer.Mask = shapeLayer;
        }
    }
}
