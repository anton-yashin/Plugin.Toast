﻿#nullable enable
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.UWP;
using XDataTemplate = Xamarin.Forms.DataTemplate;
using XThickness = Xamarin.Forms.Thickness;
using XVisualStateManager = Xamarin.Forms.VisualStateManager;
using WThickness = Windows.UI.Xaml.Thickness;
using WSize = Windows.Foundation.Size;

namespace ManualTests.UWP
{
	/// <summary>
	/// temporaly solution for https://github.com/xamarin/Xamarin.Forms/issues/13588
	/// </summary>
	public sealed class ItemContentControl : ContentControl
	{
		VisualElement? _visualElement;
		IVisualElementRenderer? _renderer;
		XDataTemplate? _currentTemplate;

		public ItemContentControl()
		{
			DefaultStyleKey = typeof(ItemContentControl);
		}

		public static readonly DependencyProperty FormsDataTemplateProperty = DependencyProperty.Register(
			nameof(FormsDataTemplate), typeof(XDataTemplate), typeof(ItemContentControl),
			new PropertyMetadata(default(XDataTemplate), FormsDataTemplateChanged));

		static void FormsDataTemplateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			if (e.NewValue == null)
			{
				return;
			}

			var itemContentControl = (ItemContentControl)d;
			itemContentControl.Realize();
		}

		public XDataTemplate FormsDataTemplate
		{
			get => (XDataTemplate)GetValue(FormsDataTemplateProperty);
			set => SetValue(FormsDataTemplateProperty, value);
		}

		public static readonly DependencyProperty FormsDataContextProperty = DependencyProperty.Register(
			nameof(FormsDataContext), typeof(object), typeof(ItemContentControl),
			new PropertyMetadata(default(object), FormsDataContextChanged));

		static void FormsDataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var formsContentControl = (ItemContentControl)d;
			formsContentControl.Realize();
		}

		public object FormsDataContext
		{
			get => GetValue(FormsDataContextProperty);
			set => SetValue(FormsDataContextProperty, value);
		}

		public static readonly DependencyProperty FormsContainerProperty = DependencyProperty.Register(
			nameof(FormsContainer), typeof(BindableObject), typeof(ItemContentControl),
			new PropertyMetadata(default(BindableObject), FormsContainerChanged));

		static void FormsContainerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var formsContentControl = (ItemContentControl)d;
			formsContentControl.Realize();
		}

		public BindableObject FormsContainer
		{
			get => (BindableObject)GetValue(FormsContainerProperty);
			set => SetValue(FormsContainerProperty, value);
		}

		public static readonly DependencyProperty ItemHeightProperty = DependencyProperty.Register(
			nameof(ItemHeight), typeof(double), typeof(ItemContentControl),
			new PropertyMetadata(default(double)));

		public double ItemHeight
		{
			get => (double)GetValue(ItemHeightProperty);
			set => SetValue(ItemHeightProperty, value);
		}

		public static readonly DependencyProperty ItemWidthProperty = DependencyProperty.Register(
			nameof(ItemWidth), typeof(double), typeof(ItemContentControl),
			new PropertyMetadata(default(double)));

		public double ItemWidth
		{
			get => (double)GetValue(ItemWidthProperty);
			set => SetValue(ItemWidthProperty, value);
		}

		public static readonly DependencyProperty ItemSpacingProperty = DependencyProperty.Register(
			nameof(ItemSpacing), typeof(XThickness), typeof(ItemContentControl),
			new PropertyMetadata(default(XThickness)));

		public XThickness ItemSpacing
		{
			get => (XThickness)GetValue(ItemSpacingProperty);
			set => SetValue(ItemSpacingProperty, value);
		}

		protected override void OnContentChanged(object oldContent, object newContent)
		{
			base.OnContentChanged(oldContent, newContent);

			if (oldContent != null && _visualElement != null)
				_visualElement.MeasureInvalidated -= OnViewMeasureInvalidated;

			if (newContent != null && _visualElement != null)
				_visualElement.MeasureInvalidated += OnViewMeasureInvalidated;
		}

		internal void Realize()
		{
			var dataContext = FormsDataContext;
			var formsTemplate = FormsDataTemplate;
			var container = FormsContainer;

			var itemsView = container as ItemsView;

			if (itemsView != null && _renderer?.Element != null)
			{
				itemsView.RemoveLogicalChild(_renderer.Element);
			}

			if (dataContext == null || formsTemplate == null || container == null)
			{
				return;
			}

			if (_renderer?.ContainerElement == null || _currentTemplate != formsTemplate)
			{
				// If the content has never been realized (i.e., this is a new instance), 
				// or if we need to switch DataTemplates (because this instance is being recycled)
				// then we'll need to create the content from the template 
				_visualElement = (VisualElement)formsTemplate.CreateContent(dataContext, container);
				_visualElement.BindingContext = dataContext;
				_renderer = Platform.CreateRenderer(_visualElement);
				Platform.SetRenderer(_visualElement, _renderer);

				// We need to set IsNativeStateConsistent explicitly; otherwise, it won't be set until the renderer's Loaded 
				// event. If the CollectionView is in a Layout, the Layout won't measure or layout the CollectionView until
				// every visible descendant has `IsNativeStateConsistent == true`. And the problem that Layout is trying
				// to avoid by skipping layout for controls with not-yet-loaded children does not apply to CollectionView
				// items. If we don't set this, the CollectionView just won't get layout at all, and will be invisible until
				// the window is resized. 
				SetNativeStateConsistent(_visualElement);

				// Keep track of the template in case this instance gets reused later
				_currentTemplate = formsTemplate;
			}
			else
			{
				// We are reusing this ItemContentControl and we can reuse the Element
				_visualElement = _renderer.Element;
				_visualElement.BindingContext = dataContext;
			}

			Content = _renderer.ContainerElement;
			itemsView?.AddLogicalChild(_visualElement);
		}

		void SetNativeStateConsistent(VisualElement visualElement)
		{
			if (visualElement is Xamarin.Forms.Button)
				return;

			visualElement.IsNativeStateConsistent = true;

			foreach (var child in visualElement.LogicalChildren)
			{
				if (!(child is VisualElement ve))
				{
					continue;
				}

				SetNativeStateConsistent(ve);
			}
		}

		internal void UpdateIsSelected(bool isSelected)
		{
			var formsElement = _renderer?.Element;

			if (formsElement == null)
				return;

			XVisualStateManager.GoToState(formsElement, isSelected
				? XVisualStateManager.CommonStates.Selected
				: XVisualStateManager.CommonStates.Normal);
		}

		void OnViewMeasureInvalidated(object sender, EventArgs e)
		{
			InvalidateMeasure();
		}

		protected override WSize MeasureOverride(WSize availableSize)
		{
			if (_renderer == null)
			{
				return base.MeasureOverride(availableSize);
			}

			var frameworkElement = (FrameworkElement)Content;

			var formsElement = _renderer.Element;
			if (ItemHeight != default || ItemWidth != default)
			{
				formsElement.Layout(new Rectangle(0, 0, ItemWidth, ItemHeight));

				var wsize = new WSize(ItemWidth, ItemHeight);

				frameworkElement.Margin = new WThickness(ItemSpacing.Left, ItemSpacing.Top, ItemSpacing.Right, ItemSpacing.Bottom);

				frameworkElement.Measure(wsize);

				return base.MeasureOverride(wsize);
			}
			else
			{
				var (width, height) = formsElement.Measure(availableSize.Width, availableSize.Height,
					MeasureFlags.IncludeMargins).Request;

				width = Max(width, availableSize.Width);
				height = Max(height, availableSize.Height);

				formsElement.Layout(new Rectangle(0, 0, width, height));

				var wsize = new WSize(width, height);

				frameworkElement.Measure(wsize);

				return base.MeasureOverride(wsize);
			}
		}

		double Max(double requested, double available)
		{
			return Math.Max(requested, ClampInfinity(available));
		}

		double ClampInfinity(double value)
		{
			return double.IsInfinity(value) ? 0 : value;
		}
	}
}
