using System;
using UIKit;
using CoreAnimation;

namespace UIAnim.AnimLibrary
{
    public class CustomPanAnimation
    {
        int direction = 1;
        nfloat progressWhenInterrupted = 0.0f;
        nfloat superViewWidth;
        nfloat xOffset;

        UIViewPropertyAnimator animator = null;

        public CustomPanAnimation(UIView view)
        {
            superViewWidth = view.Superview.Frame.Width;
            xOffset = superViewWidth - view.Frame.Width;

            var recognizer = new UIPanGestureRecognizer((UIPanGestureRecognizer obj) => 
            {
                switch(obj.State)
                {
                    case UIGestureRecognizerState.Began:
                        animator = new UIViewPropertyAnimator(1, UIViewAnimationCurve.EaseOut,() =>  
                        {
                            var velocity = obj.VelocityInView(view);
                            if (velocity.X < 0)
                                direction = -1;
                            else
                                direction = 1;

                            var frame = view.Frame;
                            frame.Offset(direction * xOffset, 0);
                            view.Frame = frame;
                        });
                        animator.PauseAnimation();
                        progressWhenInterrupted = animator.FractionComplete;
                        break;
                    case UIGestureRecognizerState.Changed:
                        var translation = obj.TranslationInView(view);
                        animator.FractionComplete = (direction * translation.X / xOffset) + progressWhenInterrupted;
                        break;
                    case UIGestureRecognizerState.Ended:
                        if (Math.Abs(animator.FractionComplete) < 0.3)
                            animator.Reversed = true;
                        var timing = new UICubicTimingParameters(UIViewAnimationCurve.EaseOut);
                        animator.ContinueAnimation(timing, 0);
                        break;
                }
            });

            view.UserInteractionEnabled = true;
            view.AddGestureRecognizer(recognizer);
        }
    }

    public enum Axis { X, Y };

    public class CustomSlideAnimation
    {
        int direction = 1;
        nfloat progressWhenInterrupted = 0.0f;
        nfloat superViewWidth;
        nfloat superViewHeight;
        nfloat xOffset;
        nfloat yOffset;

        UIViewPropertyAnimator animator = null;

        public CustomSlideAnimation(UIView view, Axis axis)
        {
            superViewWidth = view.Superview.Frame.Width;
            superViewHeight = view.Superview.Frame.Height;

            xOffset = superViewWidth - view.Frame.Width;
            yOffset = superViewHeight - view.Frame.Height - 40;

            var recognizer = new UIPanGestureRecognizer((UIPanGestureRecognizer obj) =>
            {
                switch (obj.State)
                {
                    case UIGestureRecognizerState.Began:
                        animator = new UIViewPropertyAnimator(1, UIViewAnimationCurve.EaseOut, () =>
                        {
                            var velocity = obj.VelocityInView(view);
                            if (axis == Axis.X && velocity.X < 0)
                                direction = -1;
                            else if (axis == Axis.Y && velocity.Y < 0)
                                direction = -1;
                            else
                                direction = 1;

                            var frame = view.Frame;
                            if(axis == Axis.Y)
                                frame.Offset(0, direction * yOffset);
                            else
                                frame.Offset(direction * xOffset, 0);
                            view.Frame = frame;
                        });
                        animator.PauseAnimation();
                        progressWhenInterrupted = animator.FractionComplete;
                        break;
                    case UIGestureRecognizerState.Changed:
                        var translation = obj.TranslationInView(view);
                        if(axis == Axis.Y)
                            animator.FractionComplete = (direction * translation.Y / yOffset) + progressWhenInterrupted;
                        else
                            animator.FractionComplete = (direction * translation.X / xOffset) + progressWhenInterrupted;
                        break;
                    case UIGestureRecognizerState.Ended:
                        animator.PauseAnimation();
                        break;
                }
            });

            view.UserInteractionEnabled = true;
            view.AddGestureRecognizer(recognizer);
        }
    }
}
