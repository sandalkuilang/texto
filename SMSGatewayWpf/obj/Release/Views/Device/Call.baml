   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  G Ctexto, Version=0.3.5988.25018, Culture=neutral, PublicKeyToken=nullc6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity %   SMSGatewayWpf.Views.Device.Call   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     jinteractivityPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     .���� 
������ $��0,1,0,0q�   BorderContent#��[  ._��� $��10 0 0 0q�.���   � RowDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� $��0��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$-��'Unstructured Supplementary Service Data��$��Normal)�$��16(� �� TextAlignment$
 Left=�$
��Left=� J� RenderTransformOrigin$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��55 ��� $��90 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� �� .+��� ���#838383$C��=Send and receive USSD strings e.g *888# or *233*947199934050#���� $��1�����#838383$����A sequence entered is sent to the network which replies with an alphanumerical string, for display only, or for display plus request for the next sequence.��� $��1��$��0 10 0 5q�  �� $��30 ���  � ColumnDefinitions �� $	��100 ��� $
��Auto �.+��� $��0��$��0��$��	USSD Code���� $��1��$��0��$��25��$	��250��$
��Left=�����  �� Path$ SendUSSD.USSD� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . � $��3��$��1��.+��� -    ��Name$ SendUSSD��  SquareButtonStyle#���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=����� $ SendUSSD.SendUSSDCommand�{��� $ SendUSSD.IsEnabled�)   #MahApps.Metro.Controls.ProgressRing  $��25��$��25��$��	100,0,0,0q�$
��Left=�   IsActive �� $ SendUSSD.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q�	 �� 
IsReadOnly	 . $��Stretch=�
 �� TextWrapping$

 Wrap=����� $ SendUSSD.Response�� $��1��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$
��Dial��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��90 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� .+��� ���#838383$,��&Used to set a voice, data or fax call.��� $��1��$��0 10 0 5q�  �� $��30 ���  �� $	��100 ��� $
��Auto �.+��� $��0��$��0��$��Phone number���� $��1��$��0��$��25��$	��150��$
��Left=����� $ Dial.PhoneNumber�$ PropertyChanged=� . .��.  (System.Windows.Interactivity.Interaction   	Behaviors Q  KSMSGatewayWpf.ViewModels.ValidationRules.AllowableCharactersTextBoxBehavior     RegularExpression$ ^[0-9]+$��   	MaxLength$ 20��.��� $��3��$��1��.+��� -   $
 Dial��#���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=����� $ Dial.DialCommand�{��� $ Dial.IsEnabled�  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ Dial.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q�	 . $��Stretch=�$

 Wrap=����� $ Dial.Response�� $��2��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Answer a Call��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��55 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� �� .+��� ���#838383$G��AWhen the product receives a call, it sets the RingInd signal and ���� $��1�����#838383$���{sends the ASCII (RING) string to the application Then it waits for the application to accept the call with the ATA command.��� $��1��.+��� -   $ Answer��#���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=����� $ Answer.AnswerCommand�{��� $ Answer.IsEnabled�  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ Answer.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q�	 . $��Stretch=�$

 Wrap=����� $ Answer.Response�� $��3��  �� $��30 ��� $
��Auto ��� $��50 �.+��� $��0��$��0��$��Hang-Up��$��Normal)�$��16(�$
 Left=�$
��Left=�$ 0.5,0.5&�� $��1��$��10 0 0 0q�  �� $��30 ��� $��55 ��� .+��� ���#c8c7cc$��0��$��0 1 0 1q�._�� $��0 0 0 0q����#efeff4$��2��  �� .+��� ���#838383$����Used by the application to disconnect the remote user. In the case of multiple calls, all calls are released. (active, on-hold and waiting calls).��� $��1��.+��� -   $ Hangup��#���  $��Invoke��$��16(�$��80��$��Center=�$
��Left=����� $ Hangup.HangupCommand�{��� $ Hangup.IsEnabled�  $��25��$��25��$��	100,0,0,0q�$
��Left=� �� $ Hangup.IsBusy�$ PropertyChanged=��� $��2�����#c8c7cc$��0 1 0 1q�._��� $��0q�	 . $��Stretch=�$

 Wrap=����� $ Hangup.Response�