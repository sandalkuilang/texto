   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  	 texto(   "SMSGatewayWpf.Views.Device.General   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     @d2http://schemas.microsoft.com/expression/blend/2008     .���� 
������ $��0,1,0,0q�   BorderContent#��[  ._��� $��0 0 0 0q�.���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Hardware Version��$��Normal)�$��16(� �� TextAlignment$
 Left=�$
��Left=� J� RenderTransformOrigin$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��50 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� .+��� ���#838383$��Get the hardware version��� $��1��$��0 0 0 0q�.+���   SquareButtonStyle#���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{���  �� Path$ HardwareVersion.IsEnabled� �� UpdateSourceTrigger$ PropertyChanged=����� *(&HardwareVersion.HardwareVersionCommand+)   #MahApps.Metro.Controls.ProgressRing  $��25��$��25��$��	100,0,0,0q�$
��Left=�   IsActive �� $ HardwareVersion.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� �� 
IsReadOnly . $��Stretch=� �� TextWrapping$
 Wrap=����� $ HardwareVersion.Response�� $��1��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Serial Number��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� .+��� ���#838383$0��*Get the serial number of the wireless CPU.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ SerialNumber.IsEnabled�$ PropertyChanged=����� *" SerialNumber.SerialNumberCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ SerialNumber.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ SerialNumber.Response�� $��2��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Manufacturer��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� .+��� ���#838383$-��'Return the manufacturer identification.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ Manufacturer.IsEnabled�$ PropertyChanged=����� *" Manufacturer.ManufacturerCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ Manufacturer.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ Manufacturer.Response�� $��3��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Operator��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� .+��� ���#838383$$��Returns the selected operator.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ Operator.IsEnabled�$ PropertyChanged=����� *Operator.OperatorCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ Operator.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ Operator.Response�� $��4��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$.��(International Mobile Subscriber Identity��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� .+��� ���#838383$S��MIdentify the IMSI (International Mobile Subscriber Identity) of the SIM card.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ IMSI.IsEnabled�$ PropertyChanged=����� *IMSI.IMSICommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ IMSI.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ IMSI.Response�� $��5��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Model Information��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��55 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��55��  �� �� .+��� ���#838383$(��"Get the supported frequency bands.���� $��1�����#838383$T��NWith multi-band products the response may be a combination of different bands.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $  ModelInformation.IsEnabled�$ PropertyChanged=����� **(ModelInformation.ModelInformationCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ ModelInformation.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ ModelInformation.Response�� $��6��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Software Version��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� �� .+��� ���#838383$0��*Displays some internal software reference.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ SoftwareVersion.IsEnabled�$ PropertyChanged=����� *(&SoftwareVersion.SoftwareVersionCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ SoftwareVersion.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ SoftwareVersion.Response�� $��7��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Service Center��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� �� .+��� ���#838383$@��:Indicate to which service center the message must be sent.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ ServiceCenter.IsEnabled�$ PropertyChanged=����� *$"ServiceCenter.ServiceCenterCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ ServiceCenter.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ ServiceCenter.Response�� $��8��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Activity Status��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 �.+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��50��  �� �� .+��� ���#838383$:��4Returns the activity status of the mobile equipment.��� $��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ ActivityStatus.IsEnabled�$ PropertyChanged=����� *&$ActivityStatus.ActivityStatusCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ ActivityStatus.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ ActivityStatus.Response�� $��9��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$$��Report Mobile Equipment errors��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��55 ��� $��90 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��55��  �� �� .+��� ���#838383$J��DDisables or enables the use of the +CME ERROR: xxx or CMS ERROR: xxx���� $��1�����#838383$*��$result code instead of simply ERROR.��� $��1��$��0 10 0 5q�  �� $��30 ���  � ColumnDefinitions �� $��80 ��� $	��150 �.+��� $��0��$��0��$��Value���� $��1��$��0��$��25��$	��100��$
��Left=����� $ ReportME.Value�$ PropertyChanged=�	 �� ValidatesOnDataErrors	 . � $��1��$��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ ReportME.IsEnabled�$ PropertyChanged=����� *ReportME.ReportMECommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ ReportME.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ ReportME.Response�� $��10��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��TE Character��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��80 ��� $��90 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��80��  �� �� �� .+��� ���#838383$]��Winforms the ME which character set is used by the TE. The ME can convert each character���� $��1�����#838383$Z��Tof entered or displayed strings. This is used to send, read or write short messages.���� $��2�����#838383$9��3The possible values is (GSM, PCCP437, CUSTOM, HEX).��� $��1��$��0 10 0 5q�  �� $��30 ���  �� $��80 ��� $	��160 �.+��� $��0��$��0��$��Value���� $��1��$��0��$��25��$	��100��$
��Left=����� $ TECharacter.Value�	 . $ PropertyChanged=�� $��1��$��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ TECharacter.IsEnabled�$ PropertyChanged=����� * TECharacter.TECharacterCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ TECharacter.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ TECharacter.Response�� $��11��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Signal Quality��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��55 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��55��  �� �� .+��� ���#838383$O��IThis command is used to ascertain the received signal strength indication���� $��1�����#838383$@��:and the channel bit error rate with or without a SIM card.��� $��1��$��1��$��0 0 0 0q�.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ SignalQuality.IsEnabled�$ PropertyChanged=����� *$"SignalQuality.SignalQualityCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ SignalQuality.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ SignalQuality.Response�� $��12��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Network Registration��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��$��30��  �� .+��� ���#838383$C��=Used by the application to ascertain the registration status.��� $��1��$��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $# NetworkRegistration.IsEnabled�$ PropertyChanged=����� *0.NetworkRegistration.NetworkRegistrationCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $  NetworkRegistration.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $" NetworkRegistration.Response�� $��13��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Functionality��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��80 ��� $��90 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� �� �� .+��� ���#838383$2��,the mobile station's level of functionality.���� $��1�����#838383$9��30: Set minimum functionality, IMSI detach procedure���� $��2�����#838383$G��A1: Set the full functionality mode with a complete software reset��� $��1��$��0 10 0 5q�  �� $��30 ���  �� $��80 ��� $	��150 �.+��� $��0��$��0��$��Value���� $��1��$��0��$��25��$	��100��$
��Left=����� $ Functionality.Value�$ PropertyChanged=�	 . � $��1��$��1��.+��� #���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=�{��� $ Functionality.IsEnabled�$ PropertyChanged=����� *$"Functionality.FunctionalityCommand+  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ Functionality.IsBusy�$ PropertyChanged=��� $��3�����#c8c7cc$��0 1 0 1q�._��� $��0q� . $��Stretch=�$
 Wrap=����� $ Functionality.Response�