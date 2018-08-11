﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Granular.Presentation.Generic
{
    [TemplateVisualState(VisualStates.CommonStates, VisualStates.NormalState)]
    [TemplateVisualState(VisualStates.CommonStates, VisualStates.MouseOverState)]
    [TemplateVisualState(VisualStates.CommonStates, VisualStates.PressedState)]
    [TemplateVisualState(VisualStates.CommonStates, VisualStates.DisabledState)]
    [TemplateVisualState(VisualStates.CheckStates, VisualStates.CheckedState)]
    [TemplateVisualState(VisualStates.CheckStates, VisualStates.UncheckedState)]
    [TemplateVisualState(VisualStates.CheckStates, VisualStates.IndeterminateState)]
    public class BulletChrome : Control
    {
        public static readonly DependencyProperty RenderEnabledProperty = DependencyProperty.RegisterAttached("RenderEnabled", typeof(bool), typeof(BulletChrome), new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.AffectsVisualState));
        public bool RenderEnabled
        {
            get { return (bool)GetValue(RenderEnabledProperty); }
            set { SetValue(RenderEnabledProperty, value); }
        }

        public static readonly DependencyProperty RenderMouseOverProperty = DependencyProperty.RegisterAttached("RenderMouseOver", typeof(bool), typeof(BulletChrome), new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.AffectsVisualState));
        public bool RenderMouseOver
        {
            get { return (bool)GetValue(RenderMouseOverProperty); }
            set { SetValue(RenderMouseOverProperty, value); }
        }

        public static readonly DependencyProperty RenderPressedProperty = DependencyProperty.RegisterAttached("RenderPressed", typeof(bool), typeof(BulletChrome), new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.AffectsVisualState));
        public bool RenderPressed
        {
            get { return (bool)GetValue(RenderPressedProperty); }
            set { SetValue(RenderPressedProperty, value); }
        }

        public static readonly DependencyProperty RenderCheckedProperty = DependencyProperty.RegisterAttached("RenderChecked", typeof(bool?), typeof(BulletChrome), new FrameworkPropertyMetadata(FrameworkPropertyMetadataOptions.AffectsVisualState));
        public bool? RenderChecked
        {
            get { return (bool?)GetValue(RenderCheckedProperty); }
            set { SetValue(RenderCheckedProperty, value); }
        }

        public static readonly DependencyProperty RenderRoundProperty = DependencyProperty.RegisterAttached("RenderRound", typeof(bool), typeof(BulletChrome), new FrameworkPropertyMetadata(true));
        public bool RenderRound
        {
            get { return (bool)GetValue(RenderRoundProperty); }
            set { SetValue(RenderRoundProperty, value); }
        }

        public static readonly DependencyProperty BulletTemplateProperty = DependencyProperty.RegisterAttached("BulletTemplate", typeof(ControlTemplate), typeof(BulletChrome), new FrameworkPropertyMetadata());
        public ControlTemplate BulletTemplate
        {
            get { return (ControlTemplate)GetValue(BulletTemplateProperty); }
            set { SetValue(BulletTemplateProperty, value); }
        }

        public static readonly DependencyProperty IndeterminateBulletTemplateProperty = DependencyProperty.RegisterAttached("IndeterminateBulletTemplate", typeof(ControlTemplate), typeof(BulletChrome), new FrameworkPropertyMetadata());
        public ControlTemplate IndeterminateBulletTemplate
        {
            get { return (ControlTemplate)GetValue(IndeterminateBulletTemplateProperty); }
            set { SetValue(IndeterminateBulletTemplateProperty, value); }
        }

        public static readonly DependencyProperty IsThreeStateProperty = DependencyProperty.RegisterAttached("IsThreeState", typeof(bool), typeof(BulletChrome), new FrameworkPropertyMetadata());
        public bool IsThreeState
        {
            get { return (bool)GetValue(IsThreeStateProperty); }
            set { SetValue(IsThreeStateProperty, value); }
        }

        static BulletChrome()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(BulletChrome), new FrameworkPropertyMetadata(new StyleKey(typeof(BulletChrome))));
        }

        protected override void UpdateVisualState(bool useTransitions)
        {
            VisualStateManager.GoToState(this, GetCommonStateName(), useTransitions);
            VisualStateManager.GoToState(this, GetCheckStateName(), useTransitions);
        }

        private string GetCommonStateName()
        {
            if (!RenderEnabled)
            {
                return VisualStates.DisabledState;
            }

            if (RenderPressed)
            {
                return VisualStates.PressedState;
            }

            if (RenderMouseOver)
            {
                return VisualStates.MouseOverState;
            }

            return VisualStates.NormalState;
        }

        private string GetCheckStateName()
        {
            return GetCheckStateName(RenderPressed ? GetPreviewToggledState(RenderChecked, IsThreeState) : RenderChecked);
        }

        private static bool? GetPreviewToggledState(bool? currentState, bool isThreeState)
        {
            bool? toggledState = ToggleButton.GetToggledState(currentState, isThreeState);
            return toggledState == false ? currentState : toggledState;
        }

        private static string GetCheckStateName(bool? isChecked)
        {
            if (isChecked.HasValue)
            {
                return isChecked.Value ? VisualStates.CheckedState : VisualStates.UncheckedState;
            }

            return VisualStates.IndeterminateState;
        }
    }
}