   M S B A M L   `   `   `  ���� G  Ctexto, Version=0.3.5988.25018, Culture=neutral, PublicKeyToken=nullI)clr-namespace:SMSGatewayWpf.Views.MessageSMSGatewayWpf.Views.Message  M+clr-namespace:SMSGatewayWpf.Styles.ControlsSMSGatewayWpf.Styles.Controls  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35L5clr-namespace:System.Diagnostics;assembly=WindowsBaseSystem.Diagnostics "    SMSGatewayWpf.Views.Messages   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     ;Views)clr-namespace:SMSGatewayWpf.Views.Message     @d2http://schemas.microsoft.com/expression/blend/2008     H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  7ctrls+clr-namespace:SMSGatewayWpf.Styles.Controls  XiPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity  N JFirstFloor.ModernUI, Version=1.0.6.0, Culture=neutral, PublicKeyToken=null0mui&http://firstfloorsoftware.com/ModernUI  @diag5clr-namespace:System.Diagnostics;assembly=WindowsBase    � 	Resources   �� MergedDictionaries �� Source��  �� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?��� $G Apack://application:,,,/MahApps.Metro;component/Styles/Fonts.xaml ?��� $# ../../Styles/ButtonStyle.xaml?��� $) #../../Styles/MetroListBoxStyle.xaml?�%    .��� $��Stretch=� � LayoutTransform ��  �� ScaleX$ 1Q� �� ScaleY$ 1Q� � RowDefinitions �� $��45 ���  � ColumnDefinitions �� $	��200 ��� $��* �.+���    
HeaderMenu#���   $��0,1,0,1q�  BorderContent#��[ ._��� -   $��	txtSearch��$��5,8,0,8q�$��Stretch=�$	��188��$
��Left=�$��0��$��0��*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Search��  SearchMetroTextBox#���  1  +System.Diagnostics.PresentationTraceSources	   
TraceLevel$
	 High=����� 
 �� Path$
 
SearchText� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�.��.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ KeyUp��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� $
 SearchCommand�.���� $��0��$��1��#���   $��0,1,0,1q�#��[ ._�� $��10 0 0 0q� �� .+���  �� Orientation$ 
Horizontal=�.+��� -    ��Name$ ComposeMessage��$��0 5 10 5q�$��0q�$	��100��$��40��
������ ���#D15948$
	 High=�.���� $ 
Horizontal=�.+��� $��30��$��30��$��Center=�$��Center=�$'��!../../Images/dark.new.message.png���� $��12(�$��Center=�$��Center=�$��Normal"�	Compose   $ Click��.    �� $&
  ShowComposeDialog.ComposeCommand��� $��0 5 15 5q�$��0q�$��80��$��40��
������ ���#D15948$y�	Collapsed=�.���� $ 
Horizontal=�.+��� $��30��$��30��$��Center=�$��Center=�$"��../../Images/dark.delete.png���� $��12(�$��Center=�$��Center=�$��Normal"�Delete�� $��0 5 10 5q�$��0q�$��80��$��40��
������ ���#D15948$y�	Collapsed=�.���� $ 
Horizontal=�.+��� $��30��$��30��$��Center=�$��Center=�$'��!../../Images/dark.folder.move.png���� $��12(�$��Center=�$��Center=�$��Normal"�Move�� $��1��$��0��$��2��
������ $��0,0,1,0q�#��[ ._�� .+�=  7MahApps.Metro.Controls.MetroAnimatedSingleRowTabControl  $��0��.@��� ���� .R��� $��inbox��$��18(����� .R�'   !SMSGatewayWpf.Views.Message.Inbox   � DataContext �� $
 InboxSource��� ���� .R��� $��outbox��$��18(����� .R�(	   "SMSGatewayWpf.Views.Message.Outbox	   �� $
 OutboxSource��� ���� .R��� $��draft��$��18(����� .R�'
   !SMSGatewayWpf.Views.Message.Draft
   �� $
 DraftSource��� ���� .R��� $
��spam��$��18(����� .R�&    SMSGatewayWpf.Views.Message.Spam   �� $
 
SpamSource��� $��trash������ .R��� $��trash��$��18(����� .R�'   !SMSGatewayWpf.Views.Message.Trash   �� $
 TrashSource��� $��queue������ .R��� $��queue��$��18(����� .R�'   !SMSGatewayWpf.Views.Message.Queue   �� $
 QueueSource�