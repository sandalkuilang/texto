   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5      	 texto,   &SMSGatewayWpf.Views.Dialogs.SerialPort   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  Serial Port��5      $	��450��6-   $	��367��6:    � WindowStartupLocation$ CenterScreen=�5      (   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � 
ResizeMode$ NoResize=�5	         AccentColorBrush   	GlowBrush# �   5
       � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $# ../../Styles/ButtonStyle.xaml?�6%   %    5      .��5      � $��Stretch=�6   .+�5   
   �� 
����ح�$��1q�6)   $��0,0,0,0q�6=   $��Stretch=�6N   $
��Auto��6j   ._�5      8   2MahApps.Metro.Controls.TransitioningContentControl  -   5        	  Name$	 transitionSerialPort��5   @   $
��Auto��63   
   
Transition$

 Left=�5      .��5      � $��
40,40,0,40q�6   $
��Left=�6+   $	��280��6F    5       �� 
TargetType �� Property �� Value��%  '��       '�� �     �� 5      " ��6    .�5      ��  � ��$	 150��66   6D   �� 5       � ��$
 Left=�6D   6S   �� 5       � ��$ 0 0 0 20q�67   6J   5      �� 5       " ��6    .�5!      ��  � ��$ Right=�6D   6T   5"      5#       � RowDefinitions 5%      �� 5&      $
��Auto �6(   �� 5'      $
��Auto �6(   �� 5(      $
��Auto �6(   �� 5)      $
��Auto �6(   �� 5*      $
��Auto �6(   �� 5+      $
��Auto �6(   �� 5,      $
��Auto �6(   �� 5-      $
��Auto �6(    � ColumnDefinitions 5/      �� 50      $
��Auto �6+   �� 51      $��10 �6+   �� 52      .+�55      �� $��0��6   $��0��6)   $��	Port name��69   �� 56      $��0��6   $��2��6,   �� 59      $��1��6   $��0��6)   $��Bits per second��69   �� 5:      $��1��6   $��2��6,   �� 5=      $��2��6   $��0��6)   $��Parity��69   �� 5>      $��2��6   $��2��6,   �� 5A      $��3��6   $��0��6)   $��	Data bits��69   �� 5B      $��3��6   $��2��6,   �� 5E      $��4��6   $��0��6)   $��	Stop bits��69   �� 5F      $��4��6   $��2��6,   �� 5I      $��5��6   $��0��6)   $��	Handshake��69   �� 5J      $��5��6   $��2��6,   �� 5M      $��6��6   $��0��6)   $��PDU Mode��69   �� 5N      $��6��6   $��2��6,   � 5P      $��7��6   $��3��6(    5Q      �� 5R      �� 5S      .+�5V      �� -   5         ��Name$ OK��5V   !   $��0��5W      $��OK��5X      $��80��6   $��30��6)   $��12(�5Y      $
��Left=�6)   �� 5[      -   5        $ Cancel��5[   !   $��1��5\      $��Cancel��5]      $��80��6"   $��30��6-   $��12(�5^      $
��Left=�6)   5_      5a      5b      5c      5d      5e      