   M S B A M L   `   `   `  ����H  DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	   	 texto(   "SMSGatewayWpf.Views.Dialogs.Plugin   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � WindowStartupLocation$  CenterScreen=�5   	   (   "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � 
ResizeMode$ NoResize=�5   	      AccentColorBrush   	GlowBrush# �   5	   	    � Title$ Plugin��5
   	   $	��207��6   $	��482��6%    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�6%   �� 5      $# ../../Styles/ButtonStyle.xaml?�6%   %    5      .��5      � $��Stretch=�6   .+�5   
   �� 
����ح�$��1q�6)   $��0,0,0,0q�6=   $��Stretch=�6N   $
��Auto��6j   ._�5      8   2MahApps.Metro.Controls.TransitioningContentControl  -   5        	  Name$	 transitionComposeMessage��5   @   $
��Auto��63   
   
Transition$

 Left=�5      .��5      � $��
40,40,0,19q�6   $
��Left=�6+   $	��400��6F    � RowDefinitions 5      �� 5      �� 5       � ColumnDefinitions 5      �� 5      $
��Auto �6+   �� 5       $��10 �6+   �� 5!      .+�5$      �� $��0��6!    5%       �� 
TargetType �� Property �� Value��%X   '��       �� 5&      " ��6$   .�5'   "   ��  � ��$ 0 0 0 10q�6;   5(      5)      .+�5+      �� $��Assembly name��6    �� 5,      $��1��6    $��	Type name��6-   5.      �� 50      $��2��6!    51      ��%X   '��       �� 52      " ��6$   .�53   "   ��  � ��$ 0 5 0 10q�6;   54      55      .+�57      �� �� 58      5:      � 5<      $��2��6   $��2��6(    5=      �� 5>      $	��100 �6/   �� 5?      .+�5B      �� -   5         ��Name$ OK��5B   !   $��0��5C      $��OK��5D      $��80��6   $��30��6)   $��12(�5E      $
��Left=�6)   �� 5G      -   5        $ Cancel��5G   !   $��1��5H      $��Cancel��5I      $��80��6"   $��30��6-   $��12(�5J      $
��Left=�6)   5K      5M      5N      5O      5P      5Q      