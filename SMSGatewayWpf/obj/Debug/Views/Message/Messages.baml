   M S B A M L   `   `   `  ����G  Ctexto, Version=0.3.5988.25822, Culture=neutral, PublicKeyToken=nullI)clr-namespace:SMSGatewayWpf.Views.MessageSMSGatewayWpf.Views.Message  5
      M+clr-namespace:SMSGatewayWpf.Styles.ControlsSMSGatewayWpf.Styles.Controls  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics 5      "    SMSGatewayWpf.Views.Messages   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5	      ;Views)clr-namespace:SMSGatewayWpf.Views.Message     5
      @d2http://schemas.microsoft.com/expression/blend/2008     5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  5      7ctrls+clr-namespace:SMSGatewayWpf.Styles.Controls  5      XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  5      N JFirstFloor.ModernUI, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  5      @diag5clr-namespace:System.Diagnostics;assembly=WindowsBase  5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $G Apack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml ?�6%   �� 5      $# ../../Styles/ButtonStyle.xaml?�6%   �� 5      $) #../../Styles/MetroListBoxStyle.xaml?�6%   %    5      .��5      � $��Stretch=�6    � LayoutTransform 5   
   �� 5        �� ScaleX$ 1Q�6    �� ScaleY$ 1Q�6(    � RowDefinitions 5"   
   �� 5#      $��45 �6   �� 5$       � ColumnDefinitions 5&   
   �� 5'      $	��200 �6   �� 5(      $��* �6   .+�5+   
   ��    
HeaderMenu#���   6   $��0,1,0,1q�6;     BorderContent#��[ 6V   ._�5,      �� -   5        $��	txtSearch��5-      $��5,8,0,8q�5,      $��Stretch=�5.      $	��188��5/      $
��Left=�50      $��0��51      $��0��6   *  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Search��53        SearchMetroTextBox#���  54      1  +System.Diagnostics.PresentationTraceSources	   
TraceLevel$
	 High=�55      ��52      �� 
 �� Path$
 
SearchText� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��55   A   .  (System.Windows.Interactivity.Interaction   Triggers 56      /  )System.Windows.Interactivity.EventTrigger  57         	EventName$ KeyUp��6%   .  (System.Windows.Interactivity.TriggerBase   Actions. 58      6  0System.Windows.Interactivity.InvokeCommandAction     Command 60   �� $
 SearchCommand�59      .��5:   *   5;      5<      �� 5>   
   $��0��6   $��1��6   #���   5?      $��0,1,0,1q�5@      #��[ 5A      ._�5B      � $��10 0 0 0q�6    5C      �� 5D      .+�5F      ��  �� Orientation$ 
Horizontal=�6   .+�5H      �� -   5         ��Name$ ComposeMessage��5H      $��0 5 10 5q�65   $��0q�6G   $	��100��6[   $��40��5I      
������ ���#D15948$
	 High=�5J      .��5K      �� $ 
Horizontal=�6%   .+�5L      �� $��30��6$   $��30��6/   $��Center=�6;   $��Center=�6V   $'��!../../Images/dark.new.message.png��6t   �� 5M      $��12(�6(   $��Center=�66   $��Center=�6Q   $��Normal"�6n   	Compose6�   5N       5O        5P      $ Click��6-   . 5Q   "      68   �� $&
  ShowComposeDialog.ComposeCommand�5R      5T      �� 5U      $��0 5 15 5q�6   $��0q�6/   $��80��6C   $��40��6N   
������ ���#D15948$y�	Collapsed=�6�   .��5V      �� $ 
Horizontal=�6%   .+�5W      �� $��30��6$   $��30��6/   $��Center=�6;   $��Center=�6V   $"��../../Images/dark.delete.png��6t   �� 5X      $��12(�6(   $��Center=�66   $��Center=�6Q   $��Normal"�6n   Delete6�   5Y      5Z      �� 5[      $��0 5 10 5q�6   $��0q�6/   $��80��6C   $��40��6N   
������ ���#D15948$y�	Collapsed=�6�   .��5\      �� $ 
Horizontal=�6%   .+�5]      �� $��30��6$   $��30��6/   $��Center=�6;   $��Center=�6V   $'��!../../Images/dark.folder.move.png��6t   �� 5^      $��12(�6(   $��Center=�66   $��Center=�6Q   $��Normal"�6n   Move6�   5_      5`      5a      5b      5c      �� 5e   
   $��1��6   $��0��6   $��2��6.   
������ $��0,0,1,0q�5f   *   #��[ 5g      ._�5h      � .+�5i      =  7MahApps.Metro.Controls.MetroAnimatedSingleRowTabControl  $��0��6<   .@�5j      �� ��5k      �� 5l      .R�5m   "   �� $��inbox��6,   $��18(�69   5n      ��5p      �� 5q      .R�5r   "   '   !SMSGatewayWpf.Views.Message.Inbox   � DataContext 6.   �� $
 InboxSource�5s      5u      �� 5v      ��5w      �� 5x      .R�5y   "   �� $��outbox��6,   $��18(�6:   5z      ��5|      �� 5}      .R�5~   "   (	   "SMSGatewayWpf.Views.Message.Outbox	   6/   �� $
 OutboxSource�5      5�      �� 5�      ��5�      �� 5�      .R�5�   "   �� $��draft��6,   $��18(�69   5�      ��5�      �� 5�      .R�5�   "   '
   !SMSGatewayWpf.Views.Message.Draft
   6.   �� $
 DraftSource�5�      5�      �� 5�      ��5�      �� 5�      .R�5�   "   �� $
��spam��6,   $��18(�68   5�      ��5�      �� 5�      .R�5�   "   &    SMSGatewayWpf.Views.Message.Spam   6-   �� $
 
SpamSource�5�      5�      �� 5�      $��trash��6   ��5�      �� 5�      .R�5�   "   �� $��trash��6,   $��18(�69   5�      ��5�      �� 5�      .R�5�   "   '   !SMSGatewayWpf.Views.Message.Trash   6.   �� $
 TrashSource�5�      5�      �� 5�      $��queue��6   ��5�      �� 5�      .R�5�   "   �� $��queue��6,   $��18(�69   5�      ��5�      �� 5�      .R�5�   "   '   !SMSGatewayWpf.Views.Message.Queue   6.   �� $
 QueueSource�5�      5�      5�      5�      5�      5�      5�      