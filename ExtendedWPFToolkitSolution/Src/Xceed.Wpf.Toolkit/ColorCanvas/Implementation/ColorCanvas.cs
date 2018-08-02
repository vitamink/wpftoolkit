/*************************************************************************************

   Extended WPF Toolkit

   Copyright (C) 2007-2013 Xceed Software Inc.

   This program is provided to you under the terms of the Microsoft Public
   License (Ms-PL) as published at http://wpftoolkit.codeplex.com/license 

   For more features, controls, and fast professional support,
   pick up the Plus Edition at http://xceed.com/wpf_toolkit

   Stay informed: follow @datagrid on Twitter or Like http://facebook.com/datagrids

  ***********************************************************************************/

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit.Core.Utilities;
using Xceed.Wpf.Toolkit.Primitives;
using System.IO;
using System;
using System.Windows.Shapes;

namespace Xceed.Wpf.Toolkit
{
    [TemplatePart(Name = PART_SaturationCanvas, Type = typeof(Canvas))]
    [TemplatePart(Name = PART_SaturationSelector, Type = typeof(Canvas))]
    //[TemplatePart(Name = PART_SpectrumSlider, Type = typeof(ColorSpectrumSlider))]
    [TemplatePart(Name = PART_HexadecimalTextBox, Type = typeof(TextBox))]
    public class ColorCanvas : Control
    {
        private const string PART_SaturationCanvas = "PART_SaturationCanvas";
        private const string PART_SaturationSelector = "PART_SaturationSelector";
        //private const string PART_SpectrumSlider = "PART_SpectrumSlider";
        private const string PART_IntensityCanvas = "PART_IntensityCanvas";
        private const string PART_IntensitySelector = "PART_IntensitySelector";
        private const string PART_SpectrumCanvas = "PART_SpectrumCanvas";
        private const string PART_SpectrumSelector = "PART_SpectrumSelector";
        private const string PART_SpectrumDisplay = "PART_SpectrumDisplay";
        private const string PART_HexadecimalTextBox = "PART_HexadecimalTextBox";

        #region Private Members

        private TranslateTransform _saturationSelectorTransform = new TranslateTransform();
        private Canvas _saturationCanvas;
        private Canvas _saturationSelector;


        private TranslateTransform _intensitySelectorTransform = new TranslateTransform();
        private Canvas _intensityCanvas;
        private Canvas _intensitySelector;

        private TranslateTransform _spectrumSelectorTransform = new TranslateTransform();
        private Canvas _spectrumCanvas;
        private Canvas _spectrumSelector;
        private Rectangle _spectrumDisplay;



        //private ColorSpectrumSlider _spectrumSlider;
        private TextBox _hexadecimalTextBox;
        private Point? _currentSaturationPosition;
        private Point? _currentIntensityPosition;
        private Point? _currentSpectrumPosition;
        private bool _surpressColorPropertyChanged;
        private bool _surpressIntensityPropertyChanged;
        private bool _updateSpectrumSliderValue = true;
        private bool _updateSaturationPosition = false;
        private bool _updateIntensityPosition = false;

        #endregion //Private Members

        #region Properties

        #region SelectedColor

        public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register("SelectedColor", typeof(Color?), typeof(ColorCanvas), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedColorChanged));
        public Color? SelectedColor
        {
            get => (Color?) GetValue(SelectedColorProperty);
            set => SetValue(SelectedColorProperty, value);
        }

        private static void OnSelectedColorChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            colorCanvas?.OnSelectedColorChanged((Color?) e.OldValue, (Color?) e.NewValue);
        }

        protected virtual void OnSelectedColorChanged(Color? oldValue, Color? newValue)
        {
            SetHexadecimalStringProperty(GetFormatedColorString(newValue), false);
            UpdateRGBValues(newValue);
            UpdateHueAndSaturationSelectorPositions(newValue);
            SetSelectedHue(newValue);

            RoutedPropertyChangedEventArgs<Color?> args = new RoutedPropertyChangedEventArgs<Color?>(oldValue, newValue);
            args.RoutedEvent = SelectedColorChangedEvent;
            RaiseEvent(args);
        }

        #endregion //SelectedColor

        #region SelectedHue

        public static readonly DependencyProperty SelectedHueProperty = DependencyProperty.Register("SelectedHue", typeof(Color?), typeof(ColorCanvas), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedHueChanged));
        public Color? SelectedHue
        {
            get => (Color?)GetValue(SelectedHueProperty);
            set => SetValue(SelectedHueProperty, value);
        }

        private static void OnSelectedHueChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            colorCanvas?.OnSelectedHueChanged((Color?)e.OldValue, (Color?)e.NewValue);
        }

        protected virtual void OnSelectedHueChanged(Color? oldValue, Color? newValue)
        {
            
            //SetHexadecimalStringProperty(GetFormatedColorString(newValue), false);
            //UpdateRGBValues(newValue);
            //UpdateHueAndSaturationSelectorPositions(newValue);

            //RoutedPropertyChangedEventArgs<Color?> args = new RoutedPropertyChangedEventArgs<Color?>(oldValue, newValue);
            //args.RoutedEvent = SelectedColorChangedEvent;
            //RaiseEvent(args);
        }

        #endregion //SelectedHue

        #region Intensity

        public static readonly DependencyProperty IntensityProperty = DependencyProperty.Register("Intensity", typeof(byte?), typeof(ColorCanvas), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnIntensityChanged));
        public byte? Intensity
        {
            get => (byte?)GetValue(IntensityProperty);
            set => SetValue(IntensityProperty, value);
        }

        private static void OnIntensityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            colorCanvas?.OnIntensityChanged((byte?)e.OldValue, (byte?)e.NewValue);
        }

        protected virtual void OnIntensityChanged(byte? oldValue, byte? newValue)
        {

            UpdateIntensitySelectorPosition(newValue);
            //SetHexadecimalStringProperty(GetFormatedColorString(newValue), false);
            //UpdateRGBValues(newValue);
            //UpdateColorShadeSelectorPosition(newValue);

            //RoutedPropertyChangedEventArgs<Color?> args = new RoutedPropertyChangedEventArgs<Color?>(oldValue, newValue);
            //args.RoutedEvent = SelectedColorChangedEvent;
            //RaiseEvent(args);
        }

        #endregion //SelectedColor

        #region RGB

        //#region A

        //public static readonly DependencyProperty AProperty = DependencyProperty.Register("A", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)255, OnAChanged));
        //public byte A
        //{
        //    get
        //    {
        //        return (byte)GetValue(AProperty);
        //    }
        //    set
        //    {
        //        SetValue(AProperty, value);
        //    }
        //}

        //private static void OnAChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        //{
        //    ColorCanvas colorCanvas = o as ColorCanvas;
        //    if (colorCanvas != null)
        //        colorCanvas.OnAChanged((byte)e.OldValue, (byte)e.NewValue);
        //}

        //protected virtual void OnAChanged(byte oldValue, byte newValue)
        //{
        //    if (!_surpressColorPropertyChanged)
        //        UpdateSelectedColor();
        //}

        //#endregion //A

        #region R

        public static readonly DependencyProperty RProperty = DependencyProperty.Register("R", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnRChanged));
        public byte R
        {
            get
            {
                return (byte)GetValue(RProperty);
            }
            set
            {
                SetValue(RProperty, value);
            }
        }

        private static void OnRChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnRChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnRChanged(byte oldValue, byte newValue)
        {
            if (!_surpressColorPropertyChanged)
                UpdateSelectedColor();
        }

        #endregion //R

        #region G

        public static readonly DependencyProperty GProperty = DependencyProperty.Register("G", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnGChanged));
        public byte G
        {
            get
            {
                return (byte)GetValue(GProperty);
            }
            set
            {
                SetValue(GProperty, value);
            }
        }

        private static void OnGChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnGChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnGChanged(byte oldValue, byte newValue)
        {
            if (!_surpressColorPropertyChanged)
                UpdateSelectedColor();
        }

        #endregion //G

        #region B

        public static readonly DependencyProperty BProperty = DependencyProperty.Register("B", typeof(byte), typeof(ColorCanvas), new UIPropertyMetadata((byte)0, OnBChanged));
        public byte B
        {
            get
            {
                return (byte)GetValue(BProperty);
            }
            set
            {
                SetValue(BProperty, value);
            }
        }

        private static void OnBChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnBChanged((byte)e.OldValue, (byte)e.NewValue);
        }

        protected virtual void OnBChanged(byte oldValue, byte newValue)
        {
            if (!_surpressColorPropertyChanged)
                UpdateSelectedColor();
        }

        #endregion //B

        #endregion //RGB

        #region HexadecimalString

        public static readonly DependencyProperty HexadecimalStringProperty = DependencyProperty.Register("HexadecimalString", typeof(string), typeof(ColorCanvas), new UIPropertyMetadata("", OnHexadecimalStringChanged, OnCoerceHexadecimalString));
        public string HexadecimalString
        {
            get
            {
                return (string)GetValue(HexadecimalStringProperty);
            }
            set
            {
                SetValue(HexadecimalStringProperty, value);
            }
        }

        private static void OnHexadecimalStringChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnHexadecimalStringChanged((string)e.OldValue, (string)e.NewValue);
        }

        protected virtual void OnHexadecimalStringChanged(string oldValue, string newValue)
        {
            string newColorString = GetFormatedColorString(newValue);
            string currentColorString = GetFormatedColorString(SelectedColor);
            if (!currentColorString.Equals(newColorString))
            {
                Color? col = null;
                if (!string.IsNullOrEmpty(newColorString))
                {
                    col = (Color)ColorConverter.ConvertFromString(newColorString);
                }

                _updateSaturationPosition = true;
                UpdateSelectedColor(col);
                _updateSaturationPosition = false;
            }

            SetHexadecimalTextBoxTextProperty(newValue);
        }

        private static object OnCoerceHexadecimalString(DependencyObject d, object basevalue)
        {
            var colorCanvas = (ColorCanvas)d;
            if (colorCanvas == null)
                return basevalue;

            return colorCanvas.OnCoerceHexadecimalString(basevalue);
        }

        private object OnCoerceHexadecimalString(object newValue)
        {
            var value = newValue as string;
            string retValue = value;

            try
            {
                if (!string.IsNullOrEmpty(retValue))
                {
                    int outValue;
                    // User has entered an hexadecimal value (without the "#" character)... add it.
                    if (Int32.TryParse(retValue, System.Globalization.NumberStyles.HexNumber, null, out outValue))
                    {
                        retValue = "#" + retValue;
                    }
                    ColorConverter.ConvertFromString(retValue);
                }
            }
            catch
            {
                //When HexadecimalString is changed via Code-Behind and hexadecimal format is bad, throw.
                throw new InvalidDataException("Color provided is not in the correct format.");
            }

            return retValue;
        }

        #endregion //HexadecimalString

        #region UsingAlphaChannel

        public static readonly DependencyProperty UsingAlphaChannelProperty = DependencyProperty.Register("UsingAlphaChannel", typeof(bool), typeof(ColorCanvas), new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, new PropertyChangedCallback(OnUsingAlphaChannelPropertyChanged)));
        public bool UsingAlphaChannel
        {
            get
            {
                return (bool)GetValue(UsingAlphaChannelProperty);
            }
            set
            {
                SetValue(UsingAlphaChannelProperty, value);
            }
        }

        private static void OnUsingAlphaChannelPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ColorCanvas colorCanvas = o as ColorCanvas;
            if (colorCanvas != null)
                colorCanvas.OnUsingAlphaChannelChanged();
        }

        protected virtual void OnUsingAlphaChannelChanged()
        {
            SetHexadecimalStringProperty(GetFormatedColorString(SelectedColor), false);
        }

        #endregion //UsingAlphaChannel

        #endregion //Properties

        #region Constructors

        static ColorCanvas()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ColorCanvas), new FrameworkPropertyMetadata(typeof(ColorCanvas)));
        }

        public ColorCanvas()
        {
            Intensity = (byte?) 255;
        }

        #endregion //Constructors

        #region Base Class Overrides

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (_saturationCanvas != null)
            {
                _saturationCanvas.MouseLeftButtonDown -= SaturationCanvasMouseLeftButtonDown;
                _saturationCanvas.MouseLeftButtonUp -= SaturationCanvasMouseLeftButtonUp;
                _saturationCanvas.MouseMove -= SaturationCanvasMouseMove;
                _saturationCanvas.SizeChanged -= SaturationCanvasSizeChanged;
            }

            _saturationCanvas = GetTemplateChild(PART_SaturationCanvas) as Canvas;

            if (_saturationCanvas != null)
            {
                _saturationCanvas.MouseLeftButtonDown += SaturationCanvasMouseLeftButtonDown;
                _saturationCanvas.MouseLeftButtonUp += SaturationCanvasMouseLeftButtonUp;
                _saturationCanvas.MouseMove += SaturationCanvasMouseMove;
                _saturationCanvas.SizeChanged += SaturationCanvasSizeChanged;
            }

            _saturationSelector = GetTemplateChild(PART_SaturationSelector) as Canvas;

            if (_saturationSelector != null)
                _saturationSelector.RenderTransform = _saturationSelectorTransform;






            if (_intensityCanvas != null)
            {
                _intensityCanvas.MouseLeftButtonDown -= IntensityCanvas_MouseLeftButtonDown;
                _intensityCanvas.MouseLeftButtonUp -= IntensityCanvas_MouseLeftButtonUp;
                _intensityCanvas.MouseMove -= IntensityCanvas_MouseMove;
                _intensityCanvas.SizeChanged -= IntensityCanvas_SizeChanged;
            }

            _intensityCanvas = GetTemplateChild(PART_IntensityCanvas) as Canvas;

            if (_intensityCanvas != null)
            {
                _intensityCanvas.MouseLeftButtonDown += IntensityCanvas_MouseLeftButtonDown;
                _intensityCanvas.MouseLeftButtonUp += IntensityCanvas_MouseLeftButtonUp;
                _intensityCanvas.MouseMove += IntensityCanvas_MouseMove;
                _intensityCanvas.SizeChanged += IntensityCanvas_SizeChanged;
            }

            _intensitySelector = GetTemplateChild(PART_IntensitySelector) as Canvas;

            if (_intensitySelector != null)
                _intensitySelector.RenderTransform = _intensitySelectorTransform;





            if (_spectrumCanvas != null)
            {
                _spectrumCanvas.MouseLeftButtonDown -= SpectrumCanvas_MouseLeftButtonDown;
                _spectrumCanvas.MouseLeftButtonUp -= SpectrumCanvas_MouseLeftButtonUp;
                _spectrumCanvas.MouseMove -= SpectrumCanvas_MouseMove;
                _spectrumCanvas.SizeChanged -= SpectrumCanvas_SizeChanged;
            }

            _spectrumCanvas = GetTemplateChild(PART_SpectrumCanvas) as Canvas;

            if (_spectrumCanvas != null)
            {
                _spectrumCanvas.MouseLeftButtonDown += SpectrumCanvas_MouseLeftButtonDown;
                _spectrumCanvas.MouseLeftButtonUp += SpectrumCanvas_MouseLeftButtonUp;
                _spectrumCanvas.MouseMove += SpectrumCanvas_MouseMove;
                _spectrumCanvas.SizeChanged += SpectrumCanvas_SizeChanged;
            }

            _spectrumSelector = GetTemplateChild(PART_SpectrumSelector) as Canvas;

            if (_spectrumSelector != null)
                _spectrumSelector.RenderTransform = _spectrumSelectorTransform;

            _spectrumDisplay = GetTemplateChild(PART_SpectrumDisplay) as Rectangle;

            if (_spectrumDisplay != null)
                CreateSpectrum();








            //if (_spectrumSlider != null)
            //    _spectrumSlider.ValueChanged -= SpectrumSlider_ValueChanged;

            //_spectrumSlider = GetTemplateChild(PART_SpectrumSlider) as ColorSpectrumSlider;

            //if (_spectrumSlider != null)
            //    _spectrumSlider.ValueChanged += SpectrumSlider_ValueChanged;

            if (_hexadecimalTextBox != null)
                _hexadecimalTextBox.LostFocus -= new RoutedEventHandler(HexadecimalTextBox_LostFocus);

            _hexadecimalTextBox = GetTemplateChild(PART_HexadecimalTextBox) as TextBox;

            if (_hexadecimalTextBox != null)
                _hexadecimalTextBox.LostFocus += new RoutedEventHandler(HexadecimalTextBox_LostFocus);

            UpdateRGBValues(SelectedColor);

            _updateSaturationPosition = true;
            _updateIntensityPosition = true;

            UpdateHueAndSaturationSelectorPositions(SelectedColor);
            UpdateIntensitySelectorPosition(Intensity);

            _updateSaturationPosition = false;
            _updateIntensityPosition = false;

            // When changing theme, HexadecimalString needs to be set since it is not binded.
            SetHexadecimalTextBoxTextProperty(GetFormatedColorString(SelectedColor));
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            //hitting enter on textbox will update Hexadecimal string
            if (e.Key == Key.Enter && e.OriginalSource is TextBox)
            {
                TextBox textBox = (TextBox)e.OriginalSource;
                if (textBox.Name == PART_HexadecimalTextBox)
                    SetHexadecimalStringProperty(textBox.Text, true);
            }
        }




        #endregion //Base Class Overrides

        #region Event Handlers

        void SaturationCanvasMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_saturationCanvas != null)
            {
                Point p = e.GetPosition(_saturationCanvas);
                
                UpdateSaturationSelectorPositionAndCalculateColor(p, true);
                _saturationCanvas.CaptureMouse();
                //Prevent from closing ColorCanvas after mouseDown in ListView
                e.Handled = true;
            }
        }

        void SaturationCanvasMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (_saturationCanvas != null)
            {
                _saturationCanvas.ReleaseMouseCapture();
            }
        }

        void SaturationCanvasMouseMove(object sender, MouseEventArgs e)
        {
            if (_saturationCanvas != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = e.GetPosition(_saturationCanvas);
                    UpdateSaturationSelectorPositionAndCalculateColor(p, true);
                    Mouse.Synchronize();
                }
            }
        }

        void SaturationCanvasSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_currentSaturationPosition != null)
            {
                Point _newPoint = new Point
                {
                    X = ((Point)_currentSaturationPosition).X * e.NewSize.Width,
                    Y = ((Point)_currentSaturationPosition).Y * e.NewSize.Height
                };

                UpdateSaturationSelectorPositionAndCalculateColor(_newPoint, false);
            }
        }







        void IntensityCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_intensityCanvas != null)
            {
                Point p = e.GetPosition(_intensityCanvas);
                UpdateIntensitySelectorPositionAndCalculateValue(p, true);
                _intensityCanvas.CaptureMouse();
                //Prevent from closing ColorCanvas after mouseDown in ListView
                e.Handled = true;
            }
        }

        void IntensityCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _intensityCanvas?.ReleaseMouseCapture();
        }

        void IntensityCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_intensityCanvas != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = e.GetPosition(_intensityCanvas);
                    UpdateIntensitySelectorPositionAndCalculateValue(p, true);
                    Mouse.Synchronize();
                }
            }
        }

        void IntensityCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_currentIntensityPosition != null)
            {
                Point _newPoint = new Point
                {
                    X = ((Point)_currentIntensityPosition).X * e.NewSize.Width,
                    Y = ((Point)_currentIntensityPosition).Y * e.NewSize.Height
                };

                UpdateIntensitySelectorPositionAndCalculateValue(_newPoint, false);
            }
        }



        void SpectrumCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_spectrumCanvas != null)
            {
                Point p = e.GetPosition(_intensityCanvas);
                UpdateSpectrumSelectorPositionAndCalculateValue(p, true);
                _spectrumCanvas.CaptureMouse();
                //Prevent from closing ColorCanvas after mouseDown in ListView
                e.Handled = true;
            }
        }

        void SpectrumCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _spectrumCanvas?.ReleaseMouseCapture();
        }

        void SpectrumCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (_spectrumCanvas != null)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Point p = e.GetPosition(_spectrumCanvas);
                    UpdateSpectrumSelectorPositionAndCalculateValue(p, true);
                    Mouse.Synchronize();
                }
            }
        }

        void SpectrumCanvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_currentIntensityPosition != null)
            {
                Point _newPoint = new Point
                {
                    X = ((Point)_currentIntensityPosition).X * e.NewSize.Width,
                    Y = ((Point)_currentIntensityPosition).Y * e.NewSize.Height
                };

                UpdateSpectrumSelectorPositionAndCalculateValue(_newPoint, false);
            }
        }


        void HexadecimalTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = sender as TextBox;
            SetHexadecimalStringProperty(textbox.Text, true);
        }

        #endregion //Event Handlers

        #region Events

        public static readonly RoutedEvent SelectedColorChangedEvent = EventManager.RegisterRoutedEvent("SelectedColorChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<Color?>), typeof(ColorCanvas));
        public event RoutedPropertyChangedEventHandler<Color?> SelectedColorChanged
        {
            add => AddHandler(SelectedColorChangedEvent, value);
            remove => RemoveHandler(SelectedColorChangedEvent, value);
        }

        #endregion //Events

        #region Methods

        private void UpdateSelectedColor()
        {
            SelectedColor = Color.FromArgb(255, R, G, B);
        }

        private void UpdateSelectedColor(Color? color)
        {
            SelectedColor = ((color != null) && color.HasValue)
                            ? (Color?)Color.FromArgb(color.Value.A, color.Value.R, color.Value.G, color.Value.B)
                            : null;
        }

        private void UpdateRGBValues(Color? color)
        {
            if ((color == null) || !color.HasValue)
                return;

            _surpressColorPropertyChanged = true;

            R = color.Value.R;
            G = color.Value.G;
            B = color.Value.B;

            _surpressColorPropertyChanged = false;
        }

        private double? _saturation;
        private void UpdateSaturationSelectorPositionAndCalculateColor(Point p, bool calculateColor)
        {
            if ((_saturationCanvas == null) || (_saturationSelector == null))
                return;

            if (p.Y < 0)
                p.Y = 0;

            if (p.X < 0)
                p.X = 0;

            if (p.X > _saturationCanvas.ActualWidth)
                p.X = _saturationCanvas.ActualWidth;

            if (p.Y > _saturationCanvas.ActualHeight)
                p.Y = _saturationCanvas.ActualHeight;

            _saturationSelectorTransform.X = p.X - (_saturationSelector.Width / 2);
            _saturationSelectorTransform.Y = p.Y - (_saturationSelector.Height / 2);

            p.X = p.X / _saturationCanvas.ActualWidth;
            p.Y = p.Y / _saturationCanvas.ActualHeight;

            _currentSaturationPosition = p;

            if (calculateColor)
                CalculateSaturation(p);
        }

        private void SetSelectedHue(Color? color)
        {
            if (!color.HasValue)
            {
                SelectedHue = Colors.Transparent;
            }

            var hsv = ColorUtilities.ConvertRgbToHsv(color.Value.R, color.Value.G, color.Value.B);
            _hue = hsv.H;
            SelectedHue = ColorUtilities.ConvertHsvToRgb(_hue, 1, 1);
        }

        private void UpdateHueAndSaturationSelectorPositions(Color? color)
        {
            if (_spectrumCanvas == null
                || _saturationCanvas == null
                || !color.HasValue
                || !_updateSaturationPosition)
            {
                return;
            }

            _currentSpectrumPosition = null;
            _currentSaturationPosition = null;

            var hsv = ColorUtilities.ConvertRgbToHsv(color.Value.R, color.Value.G, color.Value.B);

            //if (_updateSpectrumSliderValue)
            //{
            //    _spectrumSlider.Value = 360 - hsv.H;
            //}

            Point pHue = new Point(.5, 1 - (360 - hsv.H) / 360);

            _currentSpectrumPosition = pHue;

            _spectrumSelectorTransform.X = (pHue.X * _spectrumCanvas.Width) - 5;
            _spectrumSelectorTransform.Y = (pHue.Y * _spectrumCanvas.Height) - 5;


            Point pSaturation = new Point(.5, 1 - hsv.S);

            _currentSaturationPosition = pSaturation;

            _saturationSelectorTransform.X = (pSaturation.X * _saturationCanvas.Width) - 5;
            _saturationSelectorTransform.Y = (pSaturation.Y * _saturationCanvas.Height) - 5;
        }

        private void UpdateIntensitySelectorPositionAndCalculateValue(Point p, bool calculateIntensity)
        {
            if ((_intensityCanvas == null) || (_intensitySelector == null))
                return;

            if (p.Y < 0)
                p.Y = 0;

            if (p.X < 0)
                p.X = 0;

            if (p.X > _intensityCanvas.ActualWidth)
                p.X = _intensityCanvas.ActualWidth;

            if (p.Y > _intensityCanvas.ActualHeight)
                p.Y = _intensityCanvas.ActualHeight;

            _intensitySelectorTransform.X = p.X - (_intensitySelector.Width / 2);
            _intensitySelectorTransform.Y = p.Y - (_intensitySelector.Height / 2);

            p.X = p.X / _intensityCanvas.ActualWidth;
            p.Y = p.Y / _intensityCanvas.ActualHeight;

            _currentIntensityPosition = p;

            if (calculateIntensity)
                CalculateIntensity(p);
        }

        private void UpdateIntensitySelectorPosition(byte? val)
        {
            if (_spectrumCanvas == null
                || _intensityCanvas == null
                || !val.HasValue
                || !_updateIntensityPosition)
            {
                return;
            }
            
            _currentIntensityPosition = null;

            Point p = new Point(.5, 1 - (int) val / 255.0);

            _currentIntensityPosition = p;

            _intensitySelectorTransform.X = p.X;
            _intensitySelectorTransform.Y = p.Y;

            //_currentColorPosition = null;

            //var hsv = ColorUtilities.ConvertRgbToHsv(color.Value.R, color.Value.G, color.Value.B);

            //if (_updateSpectrumSliderValue)
            //{
            //    _spectrumSlider.Value = 360 - hsv.H;
            //}

            //Point p = new Point(hsv.S, 1 - hsv.V);

            //_currentColorPosition = p;

            //_colorShadeSelectorTransform.X = (p.X * _colorShadingCanvas.Width) - 5;
            //_colorShadeSelectorTransform.Y = (p.Y * _colorShadingCanvas.Height) - 5;
        }

        private void UpdateSpectrumSelectorPositionAndCalculateValue(Point p, bool calculateHue)
        {
            if ((_spectrumCanvas == null) || (_spectrumSelector == null))
                return;

            if (p.Y < 0)
                p.Y = 0;

            if (p.X < 0)
                p.X = 0;

            if (p.X > _spectrumCanvas.ActualWidth)
                p.X = _spectrumCanvas.ActualWidth;

            if (p.Y > _spectrumCanvas.ActualHeight)
                p.Y = _spectrumCanvas.ActualHeight;

            _spectrumSelectorTransform.X = p.X - (_spectrumSelector.Width / 2);
            _spectrumSelectorTransform.Y = p.Y - (_spectrumSelector.Height / 2);

            p.X = p.X / _spectrumCanvas.ActualWidth;
            p.Y = p.Y / _spectrumCanvas.ActualHeight;

            _currentSpectrumPosition = p;

            if (calculateHue)
            {
                CalculateHue(p);
            }
        }

        private double _hue;
        private void CalculateHue(Point p)
        {
            //_hue = p.Y * 360;
            //Color hue = ColorUtilities.ConvertHsvToRgb(_hue, 1, 1);
            //SelectedHue = hue;
            Color color = ColorUtilities.ConvertHsvToRgb(p.Y * 360, _saturation ?? 1, 1);
            SelectedColor = color;
        }

        private void UpdateSpectrumSelectorPosition(int? val)
        {
            if (_spectrumCanvas == null
                || _intensityCanvas == null
                || !val.HasValue
                || !_updateIntensityPosition)
            {
                return;
            }

            _currentIntensityPosition = null;

            Point p = new Point(.5, 1 - (int)val / 255.0);

            _currentIntensityPosition = p;

            _intensitySelectorTransform.X = p.X;
            _intensitySelectorTransform.Y = p.Y;

            //_currentColorPosition = null;

            //var hsv = ColorUtilities.ConvertRgbToHsv(color.Value.R, color.Value.G, color.Value.B);

            //if (_updateSpectrumSliderValue)
            //{
            //    _spectrumSlider.Value = 360 - hsv.H;
            //}

            //Point p = new Point(hsv.S, 1 - hsv.V);

            //_currentColorPosition = p;

            //_colorShadeSelectorTransform.X = (p.X * _colorShadingCanvas.Width) - 5;
            //_colorShadeSelectorTransform.Y = (p.Y * _colorShadingCanvas.Height) - 5;
        }

        private void CalculateSaturation(Point p)
        {
            if (_spectrumCanvas == null)
                return;

            _saturation = 1 - p.Y;
            HsvColor hsv = new HsvColor(
                _hue,
                _saturation.Value,
                1);
            var currentColor = ColorUtilities.ConvertHsvToRgb(hsv.H, hsv.S, hsv.V);
            currentColor.A = 255;
            _updateSpectrumSliderValue = false;
            SelectedColor = currentColor;
            _updateSpectrumSliderValue = true;
            SetHexadecimalStringProperty(GetFormatedColorString(SelectedColor), false);
        }

        private void CalculateIntensity(Point p)
        {
            if (_spectrumCanvas == null)
                return;

            const double epsilon = 1.0 / 255;
            const double maxThreshold = 1 - epsilon;

            var intensityVal = 1 - p.Y;

            if (intensityVal > maxThreshold)
            {
                Intensity = 255;
                return;
            }

            Intensity = (byte?) (int) Math.Max(Math.Round(intensityVal * 255), 0);
        }

        private string GetFormatedColorString(Color? colorToFormat)
        {
            if ((colorToFormat == null) || !colorToFormat.HasValue)
                return string.Empty;
            return ColorUtilities.FormatColorString(colorToFormat.ToString(), UsingAlphaChannel);
        }

        private string GetFormatedColorString(string stringToFormat)
        {
            return ColorUtilities.FormatColorString(stringToFormat, UsingAlphaChannel);
        }

        private void SetHexadecimalStringProperty(string newValue, bool modifyFromUI)
        {
            if (modifyFromUI)
            {
                try
                {
                    if (!string.IsNullOrEmpty(newValue))
                    {
                        int outValue;
                        // User has entered an hexadecimal value (without the "#" character)... add it.
                        if (Int32.TryParse(newValue, System.Globalization.NumberStyles.HexNumber, null, out outValue))
                        {
                            newValue = "#" + newValue;
                        }
                        ColorConverter.ConvertFromString(newValue);
                    }
                    HexadecimalString = newValue;
                }
                catch
                {
                    //When HexadecimalString is changed via UI and hexadecimal format is bad, keep the previous HexadecimalString.
                    SetHexadecimalTextBoxTextProperty(HexadecimalString);
                }
            }
            else
            {
                //When HexadecimalString is changed via Code-Behind, hexadecimal format will be evaluated in OnCoerceHexadecimalString()
                HexadecimalString = newValue;
            }
        }

        private void SetHexadecimalTextBoxTextProperty(string newValue)
        {
            if (_hexadecimalTextBox != null)
                _hexadecimalTextBox.Text = newValue;
        }

        #endregion //Methods








        private LinearGradientBrush _pickerBrush;
        private void CreateSpectrum()
        {
            _pickerBrush = new LinearGradientBrush();
            _pickerBrush.StartPoint = new Point(0.5, 0);
            _pickerBrush.EndPoint = new Point(0.5, 1);
            _pickerBrush.ColorInterpolationMode = ColorInterpolationMode.SRgbLinearInterpolation;

            var colorsList = ColorUtilities.GenerateHsvSpectrum();

            double stopIncrement = (double)1 / (colorsList.Count - 1);

            int i;
            for (i = 0; i < colorsList.Count; i++)
            {
                _pickerBrush.GradientStops.Add(new GradientStop(colorsList[i], i * stopIncrement));
            }

            _pickerBrush.GradientStops[i - 1].Offset = 1.0;
            if (_spectrumDisplay != null)
            {
                _spectrumDisplay.Fill = _pickerBrush;
            }
        }
    }
}
