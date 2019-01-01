using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using HexMapGeneration.ViewModels;

namespace HexMapGeneration
{
	public partial class MapView : Window
	{
		private Point _last;
		private double _maxPanX = -1500.0;
		private double _maxPanY = -1500.0;

		public MapView(MapViewModel viewModel)
		{
			DataContext = viewModel;

			InitializeComponent();

			var transformGroup = new TransformGroup();
			var translateTransform = new TranslateTransform();
			transformGroup.Children.Add(translateTransform);

			TheCanvas.RenderTransform = transformGroup;

			TheCanvas.MouseMove += OnMouseMove;
			TheCanvas.MouseLeftButtonDown += OnMouseLeftDown;
			TheCanvas.MouseLeftButtonUp += OnMouseLeftUp;
		}

		public void SetMaxPanValues(object sender, double[] maxPanValues)
		{
			_maxPanX = -maxPanValues[0];
			_maxPanY = -maxPanValues[1];
		}

		private void OnMouseLeftDown(object sender, MouseButtonEventArgs e)
		{
			_last = e.GetPosition(this);
			TheCanvas.CaptureMouse();
		}

		private void OnMouseLeftUp(object sender, MouseButtonEventArgs e)
		{
			TheCanvas.ReleaseMouseCapture();
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!TheCanvas.IsMouseCaptured)
				return;

			var tt = (TranslateTransform) ((TransformGroup) TheCanvas.RenderTransform).Children.First(tr =>
				tr is TranslateTransform);
			Vector v = _last - e.GetPosition(TheCanvas);

			var deltaX = tt.X - v.X;
			var deltaY = tt.Y - v.Y;
			if (deltaX < 0 && deltaY < 0 && deltaX > _maxPanX && deltaY > _maxPanY)
			{
				tt.X -= v.X;
				tt.Y -= v.Y;
			}

			_last = e.GetPosition(TheCanvas);
		}
	}
}
