﻿// https://github.com/mrxten/XamEffects/blob/master/src/XamEffects.Droid/CommandsPlatform.cs
//
// 4502b59  on 26 Dec 2017
// @mrxten mrxten Updated for xf 2.5
// This will exclude this file from stylecop analysis
// <auto-generated/>

using System;
using System.Threading.Tasks;

using Android.Widget;

using Sharpnado.Tabs.Droid;
using Sharpnado.Tabs.Effects;
using Sharpnado.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

using View = Android.Views.View;

[assembly: ExportEffect(typeof(AndroidTapCommandEffect), nameof(TapCommandEffect))]
namespace Sharpnado.Tabs.Droid
{
    [Preserve]
    public class AndroidTapCommandEffect : PlatformEffect
    {
        private FrameLayout _clickOverlay;

        protected override void OnAttached()
        {
            if (Container == null)
            {
                return;
            }

            _clickOverlay = ViewOverlayCollector.Add(Container, this);
            _clickOverlay.Click += ViewOnClick;
            _clickOverlay.LongClick += ViewOnLongClick;
        }

        protected override void OnDetached()
        {
            var renderer = Container as IVisualElementRenderer;
            if (renderer?.Element != null) // Check disposed
            {
                _clickOverlay.Click -= ViewOnClick;
                _clickOverlay.LongClick -= ViewOnLongClick;

                ViewOverlayCollector.Delete(Container, this);
            }
        }

        private void ViewOnClick(object sender, EventArgs eventArgs)
        {
            TaskMonitor.Create(
                async () =>
                {
                    await Task.Delay(50);
                    TapCommandEffect.GetTap(Element)?.Execute(TapCommandEffect.GetTapParameter(Element)); 
                });
        }

        private void ViewOnLongClick(object sender, View.LongClickEventArgs longClickEventArgs)
        {
            var cmd = TapCommandEffect.GetLongTap(Element);

            if (cmd == null)
            {
                longClickEventArgs.Handled = false;
                return;
            }

            TaskMonitor.Create(
                async () =>
                {
                    await Task.Delay(50);
                    cmd.Execute(TapCommandEffect.GetLongTapParameter(Element));
                    longClickEventArgs.Handled = true;
                });
        }
    }
}