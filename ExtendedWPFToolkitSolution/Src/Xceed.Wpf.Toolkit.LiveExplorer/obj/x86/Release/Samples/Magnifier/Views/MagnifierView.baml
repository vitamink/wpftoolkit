   M S B A M L   `   `   `  ���� l  hXceed.Wpf.Toolkit.LiveExplorer, Version=3.4.6747.17709, Culture=neutral, PublicKeyToken=3e4669d2f30244f4O,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorerXceed.Wpf.Toolkit.LiveExplorer  O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem �Iclr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.Magnifier.Converters;Xceed.Wpf.Toolkit.LiveExplorer.Samples.Magnifier.Converters  J    DXceed.Wpf.Toolkit.LiveExplorer.Samples.Magnifier.Views.MagnifierView   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     >local,clr-namespace:Xceed.Wpf.Toolkit.LiveExplorer     X TXceed.Wpf.Toolkit, Version=3.4.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  0sys&clr-namespace:System;assembly=mscorlib  TconvIclr-namespace:Xceed.Wpf.Toolkit.LiveExplorer.Samples.Magnifier.Converters  -   'Xceed.Wpf.Toolkit.LiveExplorer.DemoView    Title$  	Magnifier��   Description J� $��14(�$��Segoe)�.*�KIThe Magnifier control allows the user to magnify parts of an application. � 	Resources G  AXceed.Wpf.Toolkit.Core.Converters.SolidColorBrushToColorConverter #  SolidColorBrushToColorConverterU  OXceed.Wpf.Toolkit.LiveExplorer.Samples.Magnifier.Converters.VisibilityConverter  visibilityConverter$  Xceed.Wpf.Toolkit.RtfFormatter  rtfFormatterQ  KXceed.Wpf.Toolkit.Core.Converters.BorderThicknessToStrokeThicknessConverter - )BorderThicknessToStrokeThicknessConverter  enumData S� 
MethodName S� 
ObjectType  System.Enum S� MethodParameters M� TypeName��%~   &	        &	      &	 
     &	      &	              S� $ 	GetValues��"   M� $ xctk:FrameType��.���  � RowDefinitions �� $
��Auto ��� $
��Auto �.+��� $��Features��$��0��$��5q�.��� $��5q�$
��Left=� �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��100 ��� $
��Auto ��� $	��100 ��� $
��Auto ��� $	��100 �.+��� $��Zoom Factor:��$��Center=�$��5q�$  Xceed.Wpf.Toolkit.DoubleUpDown  -   	  Name$	 _zoomFactor��$��1���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]
   Minimum$
 0X��	  �Xceed.Wpf.Toolkit.NumericUpDown`1[[System.Nullable`1[[System.Double, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] 	  	Increment$	 0.1X�   Maximum$ 3X� 	  FormatString$ N1��$��5q�   Value �� *
ZoomFactor+ �� ElementName$ 
_magnifier���� $��IsUsingZoomOnMouseWheel:��$��1��$��Center=�$��5q��� -    ��Name$ _isUsingZoomOnMouseWheel��$��1��$��1��$��5q� d� 	IsChecked �� *IsUsingZoomOnMouseWheel+$ 
_magnifier���� $��ZoomFactorOnMouseWheel:��$��2��$��Center=�$��5q�  -   $	 _zoomFactorOnMouseWheel��$��2��$��1��$
 0X�$
 0.05X�$ 1X�$ N2��$��5q� �� *ZoomFactorOnMouseWheel+$ 
_magnifier���� $��2��$��Frame Type:��$��Center=�$��10,0,0,0q��� -    ��Name$ 
_frameType��$��3��$��5q� �� SelectedItem �� *	FrameType+$ 
_magnifier������  �� Source# [ �� $��Radius:��$��1��$��2��$��Center=�$��10,0,0,0q�y��� $ 
_frameType�� �� Path$ SelectedItem� �� 	Converter# [  �� ConverterParameter$ 0��  -   $	 _radius��$��1��$��3��$
 0X�$	 150X�$��5q�y��� $ 
_frameType��$ SelectedItem�# [ $ 0�� �� *Radius+$ 
_magnifier���� $��RectangleWidth:��$��1��$��2��$��Center=�$��10,0,0,0q�y��� $ 
_frameType��$ SelectedItem�# [ $ 1��  -   $	 _width��$��1��$��3��$
 0X�$	 300X�$��5q� �� *Width+$ 
_magnifier��y��� $ 
_frameType��$ SelectedItem�# [ $ 1���� $��RectangleHeight:��$��2��$��2��$��Center=�$��10,0,0,0q�y��� $ 
_frameType��$ SelectedItem�# [ $ 1��  -   $	 _height��$��2��$��3��$
 0X�$	 300X�$��5q� �� *Height+$ 
_magnifier��y��� $ 
_frameType��$ SelectedItem�# [ $ 1���� $��Border Thickness:��$��4��$��Center=�$��5q�%
  Xceed.Wpf.Toolkit.IntegerUpDown
  -    
 Name$ _borderThickness��$��5���  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Minimum$ 0X�   Maximum$ 20X�$��5q�   Value �� *BorderThickness+$ 
_magnifier��# [ �� $��Border Brush:��$��1��$��4��$��Center=�$��5q�#  Xceed.Wpf.Toolkit.ColorPicker  -	     Name$ _colorPicker��$��1��$��5��$��5q�   SelectedColor �� *BorderBrush+$ 
_magnifier��# [  �� $��1��$��10q�$
��Left=�.+��� $��Usage:�� 
 Header#��[ #  Xceed.Wpf.Toolkit.RichTextBox  -
     Name$ _txtContent�� �� 
IsReadOnly .     TextFormatter#  [ $��10(�(  "Xceed.Wpf.Toolkit.MagnifierManager!   	Magnifier! !  Xceed.Wpf.Toolkit.Magnifier  -   "  Name$" 
_magnifier��
�������#   Radius$# 80Q�$   
ZoomFactor$	$ 0.4Q�%   	FrameType$% Circle=����	#FFC72035$��2q�$	��160��$	��160��&   IsUsingZoomOnMouseWheel& . '   ZoomFactorOnMouseWheel$	' 0.1Q�