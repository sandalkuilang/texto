   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics 5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      	 texto"   SMSGatewayWpf.Views.Contacts   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      @d2http://schemas.microsoft.com/expression/blend/2008     5
      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      Fdiag5clr-namespace:System.Diagnostics;assembly=WindowsBase     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $  ../Styles/ButtonStyle.xaml?�6%   %    5      .��5      �  � RowDefinitions 5   
   �� 5      $��45 �6   �� 5       � ColumnDefinitions 5   
   �� 5       $	��200 �6   �� 5!      .+�5$   
   ��    
HeaderMenu#���   6   $��0,1,0,1q�5%        BorderContent#��[ 5&      ._�5(      �� -   5        $��	txtSearch��5)      $��5,8,0,8q�5(      $��Stretch=�5*      $	��188��5+      $
��Left=�5,      $��0��5-      $��0��6   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Search��5/        SearchMetroTextBox#���  50      1  +System.Diagnostics.PresentationTraceSources   
TraceLevel$
 High=�51      ��5.      ��  �� Path$ 
SearchText� �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�.��51   A   .   (System.Windows.Interactivity.Interaction
   Triggers
 52      /   )System.Windows.Interactivity.EventTrigger  53         	EventName$ KeyUp��6%   .   (System.Windows.Interactivity.TriggerBase   Actions. 54      6   0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� $ SearchCommand�55      .��56   *   57      58      �� 5:   
   $��0��6   $��1��6   #���   5;      $��0,1,0,1q�5<      #��[ 5=      ._�5>      � $��10 0 0 0q�6    5?      �� 5@      .+�5B      ��  �� Orientation$ 
Horizontal=�6   .+�5D      �� $��0 5 10 5q�6   $��0q�6/   $	��115��6C   $��40��6O   ���#D15948
������ .��5E      �� $ 
Horizontal=�6%   .+�5F      �� $��30��6$   $��30��6/   $��Center=�6;   $��Center=�6V   $$��../Images/dark.contact.add.png��6t   �� 5G      $��12(�6(   $��Center=�66   $��Center=�6Q   $��Normal"�6n   Add Contact6�   5H      
 5I        5J      $ Click��6-   . 5K   "      68   �� $&  ShowComposeDialog.ComposeCommand�5L      5N      �� 5O      $��0 5 10 5q�6   $��0q�6/   $��80��6C   $��40��6N   ���#D15948
������ .��5P      �� $ 
Horizontal=�6%   .+�5Q      �� $��30��6$   $��30��6/   $��Center=�6;   $��Center=�6V   $��../Images/dark.delete.png��6t   �� 5R      $��12(�6(   $��Center=�66   $��Center=�6Q   $��Normal"�6n   Delete6�   5S      
 5T        5U      $ Click��6-   . 5V   "      5W   %   �� $ DeleteCommand�   CommandParameter 5X   %   ��  �� ElementName$ listContact��$ SelectedItem�5Y      5[      5\      5]      5^      &   System.Windows.Controls.DataGrid  5`   
   -   5          Name$ listContact��5`         AutoGenerateColumns .     CanUserAddRows .  $��1��6[   $��2��6h      GridLinesVisibility$ 
Horizontal=�5a      |�.   �� IsTextSearchEnabled . ��5b      �� $ Source�$	 PropertyChanged=�
 5c        5d      $ MouseDoubleClick��6!   . 5e         6,   �� $ MouseDoubleClickCommand� 6]   �� $ listContact��$ SelectedItem�5f       � ContextMenu 5h      �� 5i      .@�5j      m� $
��Edit��6    m� Command 5k      �� $ EditCommand� m� CommandParameter 5l      ��  �� RelativeSource �� *FindAncestor+ �� AncestorType" ��$" PlacementTarget.SelectedItem�m� 5m      $��Delete��6    5n      �� $ DeleteCommand� 5o      ��  �� *FindAncestor+" ��$" PlacementTarget.SelectedItem�5p         Columns 5r      4  .System.Windows.Controls.DataGridTemplateColumn  5s      ,	  &System.Windows.Controls.DataGridColumn 	  CanUserResize .   	  HeaderTemplate 5t      �� 5u      .R�5v      ��  d� 	IsChecked 6'   �� $ 	SelectAll�$	 PropertyChanged=�
 5w   "     5x   &   $ Checked��65   . 5y   *      6@   �� $&  DataContext.CheckedHeaderCommand� �� " >� 5{   -   �� $ DataContext.Source� �� " >�5|   '     5}   &   $ 	Unchecked��65   . 5~   *      6@   �� $&  DataContext.CheckedHeaderCommand� �� " >� 5�   -   �� $ DataContext.Source� �� " >�5�   '   5�      5�         CellTemplate 5�      �� 5�      .R�5�      �� $��9 0 0 0q�6'    5�   '   �� $ 
IsSelected�$	 PropertyChanged=�
 5�   "     5�   &   $ Checked��65   . 5�   *      6@   �� $# DataContext.CheckedRowCommand� �� " >� 5�   -   �� $ DataContext.Source� �� " >�5�   '     5�   &   $ 	Unchecked��65   . 5�   *      6@   �� $# DataContext.CheckedRowCommand� �� " >� 5�   -   �� $ DataContext.Source� �� " >�5�   '   5�      5�      5�      0
  *System.Windows.Controls.DataGridTextColumn
  5�        	  Width5  /System.Windows.Controls.DataGridLengthConverter$  50 6%   ! 	  
IsReadOnly! . " 	  Header$" ID��6B   # 	  
Visibility$# Hidden=�6N   1  +System.Windows.Controls.DataGridBoundColumn$   Binding$ 6b   �� *ID+
  5�      $	  220 6%   ! . $
" Name��6C   $ 6Q   �� *Name+
  5�      $	  120 6%   ! . $" Number��6C   $ 6S   �� *PhoneNumber+5�      5�      5�      