   M S B A M L   `   `   `  ����G  Ctexto, Version=0.3.5988.25822, Culture=neutral, PublicKeyToken=null9!clr-namespace:SMSGatewayWpf.ViewsSMSGatewayWpf.Views  5      C&clr-namespace:SMSGatewayWpf.ViewModelsSMSGatewayWpf.ViewModels  5      H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls 5      c6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules  5      c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity 5      "    SMSGatewayWpf.Views.Settings   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5	      @d2http://schemas.microsoft.com/expression/blend/2008     5
      3Views!clr-namespace:SMSGatewayWpf.Views     5      8ctrls&clr-namespace:SMSGatewayWpf.ViewModels     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     5      jinteractivityPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      $��Stretch=�5      $��Stretch=�5      $��0,0,0,0q�5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $" ../Styles/CheckBoxStyle.xaml?�6%   �� 5      $) #../Styles/BackButtonMetroStyle.xaml?�6%   �� 5      $! ../Styles/TextBoxStyle.xaml?�6%   �� 5      $% ../Styles/PasswordBoxStyle.xaml?�6%   �� 5      $  ../Styles/ButtonStyle.xaml?�6%   %    5!      .��5$      �  � RowDefinitions 5%   
   �� 5&      $��17 �6   �� 5'      $
��458* �6    � ColumnDefinitions 5)   
   �� 5*      $	��20* �6   �� 5+      $
��221* �6   .+�5.   
   �� -   5        $��btnBack��5.      $��0��5/      $��1��6!   $��50��50      $��50��51      $	��Top=�52      $
��Left=�53      $��15,0,0,0q�54         MetroBackButton#���   55      .��57      F� -   5         F�Name$ 	CheckMark��59      $��40��58      $��40��6    J� SnapsToDevicePixels .    
ArrowBrush#��[ 5;      $	��2.5��5<      $��Center=�5=      $��Center=�5>      "������� @T� ����  6nр�� 6n	 J� RenderTransformOrigin$ 0.5,0.5&�5?   )   z�5@      V� 5A      .~�5B      �� �� 5C      �� 5D       �� Angle$ 45Q�6*   T� 5E      5F      5H      5I      �� 5K   
   $��1��6   $��1��6'   .��5L      �  5M      �� 5N      $	��110 �6$   �� 5O      $	��175 �6$   �� 5P      $
��Auto �6$   �� 5Q      $	��220 �6$   �� 5R      $	��260 �6$   �� 5S      $	��100 �6$    5U      �� 5V      $	��350 �6'   �� 5W      .+�5Z      � $��0��6   $��2��6$    5[      �� 5\      $��35 �6(   �� 5]      .+�5_      �� $��0��6   $��0��6,   $��Notifications��5`      $��Normal"�5a      $��20(�5b      $��-5,0,0,0q�5c      	 �� TextAlignment$
	 Left=�5d      $
��Left=�5e      �� 5f      $��0��6!   $��1��61   $��80��6>   $	��Top=�6J   .+�5g      �� $
��Auto��6#   $
��Left=�5h   #   $��Desktop notifications��5i   #   $��0, 10, 0, 0q�5j   #   {�.  
 d� 	IsChecked
 5k   #   ��  �� Path$" General.DesktopNotifications� �� UpdateSourceTrigger$ PropertyChanged=��� 5l      $
��Auto��5m      $
��Left=�5n      $��Sound��5o      $��0, 10, 0, 0q�5p      
 5q      �� $ General.Sounds�$ PropertyChanged=�5r      5s      � 5u      $��1��6   $��2��6$    5v      �� 5w      $��35 �6(   �� 5x      .+�5{      �� $��0��6   $��0��6,   $��General��5|      $��Normal"�5}      $��20(�5~      $��-5,0,0,0q�5      $
	 Left=�5�      $
��Left=�5�      �� 5�      $��0��6!   $��1��61   .+�5�      �� $
��Auto��5�      $
��Left=�5�      $��Show tray icon��5�      $��0, 10, 0, 0q�5�      {�.  
 5�      �� $ General.ShowTrayIcon�$ PropertyChanged=��� 5�      $
��Auto��5�      $
��Left=�5�      $��Show taskbar icon��5�      $��0, 10, 0, 0q�5�      
 5�      �� $ General.ShowTaskbarIcon�$ PropertyChanged=��� 5�      $
��Auto��5�      $
��Left=�5�      $��Launch when system starts��5�      $��0, 10, 0, 0q�5�      
 5�      �� $$ General.LaunchWhenSystemStarts�$ PropertyChanged=��� 5�      $
��Auto��5�      $
��Left=�5�      $��Launch minimized��5�      $��0, 10, 0, 0q�5�      
 5�      �� $ General.LaunchMinimized�$ PropertyChanged=�5�      5�      � 5�      $��2��6   $��2��6$    5�      �� 5�      $��40 �6(   �� 5�      .+�5�      �� $��0��6   $��0��6,   $��Encryption Key��5�      $��Normal"�5�      $��20(�5�      $��-5,0,0,0q�5�      $
	 Left=�5�      $
��Left=�5�      �� 5�      $��0��6!   $��1��61   $	��Top=�6>   .+�5�      �  5�      �� 5�   "   $��* �60    5�      �� 5�   "   $
��100* �63   .+�5�      �� $��0��6)   $��0,10,0,0q�66   .+�5�   "   �� $��0,0,0,10q�6*   $
��Left=�6<   $	��450��5�   *   $	��140��66    �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=�5�   *    �� TextWrapping$
 Wrap=�6M   $��16(�6a   
 � Tag$	 Key��6o   ��5�   *   �� $ Encryption.Key�$ PropertyChanged=�.��6t   .  (System.Windows.Interactivity.Interaction   	Behaviors 5�   &   Q   KSMSGatewayWpf.ViewModels.ValidationRules.AllowableCharactersTextBoxBehavior  5�   *      RegularExpression$ ^[0-9\, ]+$��6S      	MaxLength$	 700��6s   .��5�   K   5�   #   5�      5�      5�      5�      � 5�      $��3��6   $��2��6$    5�      �� 5�      $��40 �6(   �� 5�      .+�5�      �� $��0��6   $��0��6,   $��Gateway Connection��5�      $��Normal"�5�      $��20(�5�      $��-5,0,0,0q�5�      $
	 Left=�5�      $
��Left=�5�      �� 5�      $��0��6!   $��1��61   $	��Top=�6>   .+�5�      �  5�      �� 5�   "   $��* �60    5�      �� 5�   "   $
��100* �63   .+�5�      �� $��0��6)   $��0,10,0,0q�66   .+�5�   "   �� $��0,0,0,10q�6*   $
��Left=�5�   *   $	��190��5�   *   $��16(�5�   *   $ 
IP Address��5�   *   ��5�   *   �� $ Gateway.IPAddress�$ PropertyChanged=�.��6w    5�   &     5�   *   $ 	^[0-9.]+$��6S   $ 15��6q   .��5�   K   5�   #   �� 5�   "   $��0,0,0,10q�6*   $
��Left=�5�   *   $	��190��5�   *   $��16(�5�   *   $
 Port��5�   *   ��5�   *   �� $ Gateway.Port�$ PropertyChanged=�.��6r    5�   &     5�   *   $ ^[0-9]+$��6S   $ 10��6p   .��5�   K   5�   #   5�      5�      � 5�      .+�5�      �� $
��Left=�5�   !   {�5�   !   �� $ Commands.IsEnabled�$ PropertyChanged=���5�   !   �� $ Commands.ButtonContent�$ PropertyChanged=���5�   !   �� $% Commands.RouteConnectionCommand�)  #MahApps.Metro.Controls.ProgressRing  5�      $��30��64   $��30��6?   $��140,10,0,10q�5�   4   $
��Left=�5�   4      IsActive 5�   4   �� $ Commands.IsBusy�$ PropertyChanged=�5�   4   5�      5�      5�      � 5�      $��4��6   $��2��6$    5�      �� 5�      $��35 �6(   �� 5      .+�5     �� $��0��6   $��0��6,   $��Database��5     $��Normal"�5     $��20(�5     $��-5,0,0,0q�5     $
	 Left=�5     $
��Left=�5     �� 5	     $��0��6!   $��1��61   $	��Top=�6>   .+�5
     �  5     �� 5  "   $
��Auto �60    5     �� 5  "   .+�5     �� $��0��6)   $��0,10,0,0q�66   .+�5  "   �� $��0,0,0,10q�6*   $
��Left=�5  *   $	��190��5  *   $��16(�5  *   $ Server name��5  *   ��5  *   �� $ Database.Server�$ PropertyChanged=��� 5  "   $��0,0,0,10q�6*   $
��Left=�5  *   $	��190��5  *   $��16(�5  *   $
 Name��5  *   ��5  *   �� $ Database.Name�$ PropertyChanged=��� 5  "   $��0,0,0,10q�6*   $
��Left=�5   *   $	��190��5!  *   $��16(�5"  *   $ Login��5#  *   ��5$  *   �� $ Database.Username�$ PropertyChanged=�G� 5%  "   $��0,0,0,10q�6.   $��16(�5&  )   -   'SMSGatewayWpf.ViewModels.PasswordHelper   PlaceHolderText$ Password��5'  )   $
��Left=�5(  )   $	��190��6D   5)     5*     5+     5,     � 5.     $��5��6   $��2��6$   .+�5/     �� $
��Save��50     $
��Left=�51     ��52     �� $ Commands.SaveCommand�53     54     55     57     59     