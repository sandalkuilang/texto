   M S B A M L   `   `   `  ����G  Ctexto, Version=0.3.5988.25822, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules  5   	   c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics 5   	   ;"clr-namespace:SMSGatewayWpf.StylesSMSGatewayWpf.Styles  5
   	   /    )SMSGatewayWpf.Views.Dialogs.LookupContact   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     5   	   H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5	   	   /styles"clr-namespace:SMSGatewayWpf.Styles  5
   	     � WindowStartupLocation$  CenterScreen=�5   	   (  "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � 
ResizeMode$ NoResize=�5   	      AccentColorBrush   	GlowBrush# �   5   	    � Title$ Lookup��5   	   $	��420��6   $	��480��6%    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $# ../../Styles/ButtonStyle.xaml?�6%   �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   %    5      .��5      � 	 �� ClearTypeHint$	 Enabled=�6   &   System.Windows.Media.TextOptions
   TextFormattingMode$
 Display=�61   $��Stretch=�5      .+�5   
   �� 
����ح�$��1q�6)   $��0,0,0,0q�6=   $��Stretch=�6N   $
��Auto��6j   ._�5      8  2MahApps.Metro.Controls.TransitioningContentControl  -   5          Name$ transitionComposeContact��5   @   $
��Auto��63      
Transition$
 Left=�5      $��	9,-1,-9,1q�6#   .��5!      � $��20,20,30,20q�6    5"       �� 
TargetType �� Property �� Value��%�   '��       '�� N     �� 5#      " ��6    .�5$      ��  � ��$ 0 0 0 10q�67   5%      �� 5&      " ��6    .�5'      ��  � ��$ 0 0 0 10q�67   5(      5)       � ColumnDefinitions 5*      �� 5+       � RowDefinitions 5-      �� 5.      $
��Auto �6(   �� 5/      $��10 �6(   �� 50      $
��Auto �6(   �� 51      $
��Auto �6(   .+�54      �� $��0��55      $��0��6&   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Search��57        SearchMetroTextBox#���  58      1  +System.Diagnostics.PresentationTraceSources   
TraceLevel$
 High=�59      $��0,0,0,0q�6I   ��56      ��  �� Path$ 
SearchText� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��59   Z   .  (System.Windows.Interactivity.Interaction   Triggers 5:      /  )System.Windows.Interactivity.EventTrigger  5;         	EventName$ KeyUp��6-   .  (System.Windows.Interactivity.TriggerBase   Actions. 5<   "   6	  0System.Windows.Interactivity.InvokeCommandAction	   	  Command 68   �� $ SearchCommand�5=      .��5>   2   5?      �� 5A      
����ح�$��1q�65   $��2��6I   $��Stretch=�6V   $	��240��6r   ._�5B      &
   System.Windows.Controls.DataGrid
  -   5         
 Name$ listContact��5B   #    
  AutoGenerateColumns .   
  CanUserAddRows .   
  GridLinesVisibility$ 
Horizontal=�5C      |�.   �� IsTextSearchEnabled . ��5D      �� $ Source�$ PropertyChanged=�  
  Columns  5T      4  .System.Windows.Controls.DataGridTemplateColumn  5U   "   ,  &System.Windows.Controls.DataGridColumn!   CanUserResize! .  "   HeaderTemplate" 5V   &   �� 5W   *   .R�5X   .   �� # d� 	IsChecked# 67   �� $ 	SelectAll�$ PropertyChanged=�5e   /   5f   +   $   CellTemplate$ 5h   &   �� 5i   *   .R�5j   .   �� $��9 0 0 0q�67   # 5k   '   �� $ 
IsSelected�$ PropertyChanged=�5x   /   5y   +   5{   #   0  *System.Windows.Controls.DataGridTextColumn  5}   "   %   Width5  /System.Windows.Controls.DataGridLengthConverter$% 50 65   &   
IsReadOnly& . '   Header$' ID��6R   (   
Visibility$( Hidden=�6^   1  +System.Windows.Controls.DataGridBoundColumn)   Binding) 6r   �� *ID+  5~   "   $	% 200 65   & . $
' Name��6S   ) 6a   �� *Name+  5   "   $	% 120 65   & . $' Number��6S   ) 6c   �� *PhoneNumber+5�      5�      �� 5�      $��3��6!   * �� Orientation$* 
Horizontal=�5�   !   $��0 15 0 0q�6:   .+�5�      �� -   5        + ��Name$+ btnOK��5�   !   $��OK��60   $	��215��5�   !   $��60��6-   $��0 0 5 0q�5�   !   $��Stretch=�62   $��Stretch=�6P   ��5�   !   �� *	OkCommand+��5�   !   �� 5�   P   �� 5�      -   5        $+ 	btnCancel��5�   !   $��Cancel��64   $	��215��5�   !   $��60��6-   $��0 0 5 0q�5�   !   $��Stretch=�62   $��Stretch=�6P   5�   !   )  #MahApps.Metro.Controls.ProgressRing  5�      $��25��60   $��25��6;   $��20,0,0,0q�5�   4   $
��Left=�5�   4   ,   IsActive, 5�   4   �� $ IsBusy�$ PropertyChanged=�5�   4   5�      5�      5�      5�      5�      5�      