﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Test.Utils
{
    public static class Gesture
    {
        public static readonly BindableProperty TappedProperty = 
            BindableProperty.CreateAttached("Tapped", typeof(Command<Point>), typeof(Gesture), null, propertyChanged: CommandChanged);

        public static Command<Point> GetCommand(BindableObject view)
        {
            return (Command<Point>)view.GetValue(TappedProperty);
        }

        public static void SetTapped(BindableObject view, Command<Point> value)
        {
            view.SetValue(TappedProperty, value);
        }

        private static void CommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as View;
            if (view != null)
            {
                var effect = GetOrCreateEffect(view);
            }
        }

        private static GestureEffect GetOrCreateEffect(View view)
        {
            var effect = (GestureEffect)view.Effects.FirstOrDefault(e => e is GestureEffect);
            if (effect == null)
            {
                effect = new GestureEffect();
                view.Effects.Add(effect);
            }
            return effect;
        }

        class GestureEffect : RoutingEffect
        {
            public GestureEffect() : base("Test.TapWithPositionGestureEffect")
            {
            }
        }
    }
}
