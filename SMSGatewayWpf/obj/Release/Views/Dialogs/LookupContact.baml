   M S B A M L   `   `   `  ���� G  Ctexto, Version=0.3.5988.25018, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics ;"clr-namespace:SMSGatewayWpf.StylesSMSGatewayWpf.Styles  /    )SMSGatewayWpf.Views.Dialogs.LookupContact   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  /styles"clr-namespace:SMSGatewayWpf.Styles    � WindowStartupLocation$  CenterScreen=�(  "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � 
ResizeMode$ NoResize=�   AccentColorBrush   	GlowBrush# �    � Title$ Lookup��$	��420��$	��480�� � 	Resources  �� MergedDictionaries �� Source��  �� $# ../../Styles/ButtonStyle.xaml?��� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�%    .��� 	 �� ClearTypeHint$	 Enabled=�&   System.Windows.Media.TextOptions
   TextFormattingMode$
 Display=�$��Stretch=�.+��� 
����ح�$��1q�$��0,0,0,0q�$��Stretch=�$
��Auto��._�8  2MahApps.Metro.Controls.TransitioningContentControl  -     Name$ transitionComposeContact��$
��Auto��   
Transition$
 Left=�$��	9,-1,-9,1q�.��� $��20,20,30,20q�  �� 
TargetType �� Property �� Value��%f   '��       '�� )     �� " ��.���  � ��$ 0 0 0 10q��� " ��.���  � ��$ 0 0 0 10q� � ColumnDefinitions ��  � RowDefinitions �� $
��Auto ��� $��10 ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Search��  SearchMetroTextBox#���  1  +System.Diagnostics.PresentationTraceSources   
TraceLevel$
 High=�$��0,0,0,0q�����  �� Path$ 
SearchText� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ KeyUp��.  (System.Windows.Interactivity.TriggerBase   Actions. 6	  0System.Windows.Interactivity.InvokeCommandAction	   	  Command �� $ SearchCommand�.���� 
����ح�$��1q�$��2��$��Stretch=�$	��240��._�&
   System.Windows.Controls.DataGrid
  -    
 Name$ listContact�� 
  AutoGenerateColumns .   
  CanUserAddRows .   
  GridLinesVisibility$ 
Horizontal=�|�.   �� IsTextSearchEnabled . ���� $ Source�$ PropertyChanged=�  
  Columns  4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn!   CanUserResize! .  "   HeaderTemplate" �� .R��� # d� 	IsChecked# �� $ 	SelectAll�$ PropertyChanged=�$   CellTemplate$ �� .R��� $��9 0 0 0q�# �� $ 
IsSelected�$ PropertyChanged=�0  *System.Windows.Controls.DataGridTextColumn  %   Width5  /System.Windows.Controls.DataGridLengthConverter$% 50 &   
IsReadOnly& . '   Header$' ID��(   
Visibility$( Hidden=�1  +System.Windows.Controls.DataGridBoundColumn)   Binding) �� *ID+  $	% 200 & . $
' Name��) �� *Name+  $	% 120 & . $' Number��) �� *PhoneNumber+�� $��3��* �� Orientation$* 
Horizontal=�$��0 15 0 0q�.+��� -   + ��Name$+ btnOK��$��OK��$	��215��$��60��$��0 0 5 0q�$��Stretch=�$��Stretch=����� *	OkCommand+���� �� -   $+ 	btnCancel��$��Cancel��$	��215��$��60��$��0 0 5 0q�$��Stretch=�$��Stretch=�)  #MahApps.Metro.Controls.ProgressRing  $��25��$��25��$��20,0,0,0q�$
��Left=�,   IsActive, �� $ IsBusy�$ PropertyChanged=�