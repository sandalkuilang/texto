   M S B A M L   `   `   `  ���� G  Ctexto, Version=0.3.5988.25018, Culture=neutral, PublicKeyToken=null9!clr-namespace:SMSGatewayWpf.ViewsSMSGatewayWpf.Views  C&clr-namespace:SMSGatewayWpf.ViewModelsSMSGatewayWpf.ViewModels  H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls c6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity "    SMSGatewayWpf.Views.Settings   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     3Views!clr-namespace:SMSGatewayWpf.Views     8ctrls&clr-namespace:SMSGatewayWpf.ViewModels     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     jinteractivityPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     $��Stretch=�$��Stretch=�$��0,0,0,0q�  � 	Resources   �� MergedDictionaries �� Source��  �� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?��� $" ../Styles/CheckBoxStyle.xaml?��� $) #../Styles/BackButtonMetroStyle.xaml?��� $! ../Styles/TextBoxStyle.xaml?��� $% ../Styles/PasswordBoxStyle.xaml?��� $  ../Styles/ButtonStyle.xaml?�%    .���  � RowDefinitions �� $��17 ��� $
��458* � � ColumnDefinitions �� $	��20* ��� $
��221* �.+��� -   $��btnBack��$��0��$��1��$��50��$��50��$	��Top=�$
��Left=�$��15,0,0,0q�   MetroBackButton#���   .��F� -    F�Name$ 	CheckMark��$��40��$��40�� J� SnapsToDevicePixels .    
ArrowBrush#��[ $	��2.5��$��Center=�$��Center=�"������� @T� ����  6nр�� 6n	 J� RenderTransformOrigin$ 0.5,0.5&�z�V� .~��� �� ��  �� Angle$ 45Q�T� �� $��1��$��1��.���  �� $	��110 ��� $	��175 ��� $
��Auto ��� $	��220 ��� $	��260 ��� $	��100 � �� $	��350 ��� .+�� $��0��$��2�� �� $��35 ��� .+��� $��0��$��0��$��Notifications��$��Normal"�$��20(�$��-5,0,0,0q�	 �� TextAlignment$
	 Left=�$
��Left=��� $��0��$��1��$��80��$	��Top=�.+��� $
��Auto��$
��Left=�$��Desktop notifications��$��0, 10, 0, 0q�{�.  
 d� 	IsChecked
 ��  �� Path$" General.DesktopNotifications� �� UpdateSourceTrigger$ PropertyChanged=��� $
��Auto��$
��Left=�$��Sound��$��0, 10, 0, 0q�
 �� $ General.Sounds�$ PropertyChanged=�� $��1��$��2�� �� $��35 ��� .+��� $��0��$��0��$��General��$��Normal"�$��20(�$��-5,0,0,0q�$
	 Left=�$
��Left=��� $��0��$��1��.+��� $
��Auto��$
��Left=�$��Show tray icon��$��0, 10, 0, 0q�{�.  
 �� $ General.ShowTrayIcon�$ PropertyChanged=��� $
��Auto��$
��Left=�$��Show taskbar icon��$��0, 10, 0, 0q�
 �� $ General.ShowTaskbarIcon�$ PropertyChanged=��� $
��Auto��$
��Left=�$��Launch when system starts��$��0, 10, 0, 0q�
 �� $$ General.LaunchWhenSystemStarts�$ PropertyChanged=��� $
��Auto��$
��Left=�$��Launch minimized��$��0, 10, 0, 0q�
 �� $ General.LaunchMinimized�$ PropertyChanged=�� $��2��$��2�� �� $��40 ��� .+��� $��0��$��0��$��Encryption Key��$��Normal"�$��20(�$��-5,0,0,0q�$
	 Left=�$
��Left=��� $��0��$��1��$	��Top=�.+��  �� $��* � �� $
��100* �.+��� $��0��$��0,10,0,0q�.+��� $��0,0,0,10q�$
��Left=�$	��450��$	��140�� �� AcceptsReturn . " �� VerticalScrollBarVisibility$
 Auto=� �� TextWrapping$
 Wrap=�$��16(�
 � Tag$	 Key������ $ Encryption.Key�$ PropertyChanged=�.��.  (System.Windows.Interactivity.Interaction   	Behaviors Q   KSMSGatewayWpf.ViewModels.ValidationRules.AllowableCharactersTextBoxBehavior     RegularExpression$ ^[0-9\, ]+$��   	MaxLength$	 700��.��� $��3��$��2�� �� $��40 ��� .+��� $��0��$��0��$��Gateway Connection��$��Normal"�$��20(�$��-5,0,0,0q�$
	 Left=�$
��Left=��� $��0��$��1��$	��Top=�.+��  �� $��* � �� $
��100* �.+��� $��0��$��0,10,0,0q�.+��� $��0,0,0,10q�$
��Left=�$	��190��$��16(�$ 
IP Address������ $ Gateway.IPAddress�$ PropertyChanged=�.��   $ 	^[0-9.]+$��$ 15��.���� $��0,0,0,10q�$
��Left=�$	��190��$��16(�$
 Port������ $ Gateway.Port�$ PropertyChanged=�.��   $ ^[0-9]+$��$ 10��.��� .+��� $
��Left=�{��� $ Commands.IsEnabled�$ PropertyChanged=����� $ Commands.ButtonContent�$ PropertyChanged=����� $% Commands.RouteConnectionCommand�)  #MahApps.Metro.Controls.ProgressRing  $��30��$��30��$��140,10,0,10q�$
��Left=�   IsActive �� $ Commands.IsBusy�$ PropertyChanged=�� $��4��$��2�� �� $��35 ��� .+��� $��0��$��0��$��Database��$��Normal"�$��20(�$��-5,0,0,0q�$
	 Left=�$
��Left=��� $��0��$��1��$	��Top=�.+��  �� $
��Auto � �� .+��� $��0��$��0,10,0,0q�.+��� $��0,0,0,10q�$
��Left=�$	��190��$��16(�$ Server name������ $ Database.Server�$ PropertyChanged=��� $��0,0,0,10q�$
��Left=�$	��190��$��16(�$
 Name������ $ Database.Name�$ PropertyChanged=��� $��0,0,0,10q�$
��Left=�$	��190��$��16(�$ Login������ $ Database.Username�$ PropertyChanged=�G� $��0,0,0,10q�$��16(�-   'SMSGatewayWpf.ViewModels.PasswordHelper   PlaceHolderText$ Password��$
��Left=�$	��190��� $��5��$��2��.+��� $
��Save��$
��Left=����� $ Commands.SaveCommand�