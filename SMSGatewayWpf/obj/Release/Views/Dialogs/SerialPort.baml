   M S B A M L   `   `   `  ���� H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  	 texto,   &SMSGatewayWpf.Views.Dialogs.SerialPort   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Serial Port��$	��450��$	��367�� � WindowStartupLocation$ CenterScreen=�(   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � 
ResizeMode$ NoResize=�   AccentColorBrush   	GlowBrush# �    � 	Resources  �� MergedDictionaries �� Source��  �� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?��� $# ../../Styles/ButtonStyle.xaml?�%    .��� $��Stretch=�.+��� 
����ح�$��1q�$��0,0,0,0q�$��Stretch=�$
��Auto��._�8   2MahApps.Metro.Controls.TransitioningContentControl  -   	  Name$	 transitionSerialPort��$
��Auto��
   
Transition$

 Left=�.��� $��
40,40,0,40q�$
��Left=�$	��280��  �� 
TargetType �� Property �� Value��%�   '��       '�� X     �� " ��.���  � ��$	 150����  � ��$
 Left=���  � ��$ 0 0 0 20q��� " ��.���  � ��$ Right=� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��10 ��� .+��� $��0��$��0��$��	Port name���� $��0��$��2���� $��1��$��0��$��Bits per second���� $��1��$��2���� $��2��$��0��$��Parity���� $��2��$��2���� $��3��$��0��$��	Data bits���� $��3��$��2���� $��4��$��0��$��	Stop bits���� $��4��$��2���� $��5��$��0��$��	Handshake���� $��5��$��2���� $��6��$��0��$��PDU Mode���� $��6��$��2��� $��7��$��3�� �� �� .+��� -    ��Name$ OK��$��0��$��OK��$��80��$��30��$��12(�$
��Left=��� -   $ Cancel��$��1��$��Cancel��$��80��$��30��$��12(�$
��Left=�