   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  G Ctexto, Version=0.3.5988.25018, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity $   SMSGatewayWpf.Views.Device.SMS   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     jinteractivityPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     .���� 
������ $��0,1,0,0q�   BorderContent#��[  ._��� $��10 0 0 0q�.���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��  �� $��30 ��� $
��Auto ��� $��80 �.+��� $��0��$��0��$'��!Send Message From Storage (+CMGW)��$��Normal)�$��16(� �� TextAlignment$
 Left=�$
��Left=� J� RenderTransformOrigin$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $
��Auto ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� .+��� ���#838383$;��5This command sends a message stored at location value��� $��1��$��0 10 0 5q�  �� $��30 ��� $��30 ��� $��50 ���  � ColumnDefinitions �� $	��100 ��� $
��Auto �.+��� $��0��$��0��$��Phone number���� -    ��Name$ PhoneNumber��$��1��$��0��$��25��$	��100��$��1,0,0,0q�$
��Left=�.������  �� Path$ SendSMS.PhoneNumber� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . .��.  (System.Windows.Interactivity.Interaction   	Behaviors Q  KSMSGatewayWpf.ViewModels.ValidationRules.AllowableCharactersTextBoxBehavior  	   RegularExpression$	 ^[0-9]+$��
   	MaxLength$
 20��.���� $��0��$��1��$��Message���� -   $ Message��$��1��$��1��$��2�� �� AcceptsReturn .  �� TextWrapping$
 Wrap=�$��Stretch=�$	��300������ *SendSMS.Message+ . � $��3��$��1��.+��� -    ��Name$ SendSMS��  SquareButtonStyle#���  $��16(�$��Invoke��$��80��$��Center=�$
��Left=�{��� $ SendSMS.IsEnabled�$ PropertyChanged=����� *SendSMS.SendCommand+)   #MahApps.Metro.Controls.ProgressRing  $��25��$��25��$��100,10,0,10q�$
��Left=�   IsActive �� $ SendSMS.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q� �� 
IsReadOnly . $��Stretch=�$
 Wrap=����� $ SendSMS.Response�$ PropertyChanged=�� $��1��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��List Message��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��50 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� �� .+��� ���#838383$:��4This allows the application to read stored messages.���� $��1�����#838383$J��DThe messages are read from the memory selected by the +CPMS command.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ ReadSMS.IsEnabled�$ PropertyChanged=����� *ReadSMS.ReadCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ ReadSMS.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=����� $ ReadSMS.Response�� $��2��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��
Delete All��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��25 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� .+��� ���#838383$L��FUsed to delete one or several messages from preferred message storage.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ DeleteAllSMS.IsEnabled�$ PropertyChanged=����� *DeleteAllSMS.DeleteAllCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ DeleteAllSMS.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=����� $ DeleteAllSMS.Response�