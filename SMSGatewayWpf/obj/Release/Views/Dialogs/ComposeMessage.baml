   M S B A M L   `   `   `  ���� G  Ctexto, Version=0.3.5988.25018, Culture=neutral, PublicKeyToken=nullS.clr-namespace:SMSGatewayWpf.ViewModels.Message SMSGatewayWpf.ViewModels.Message  c6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules(SMSGatewayWpf.ViewModels.ValidationRules  c _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity ;"clr-namespace:SMSGatewayWpf.StylesSMSGatewayWpf.Styles  0    *SMSGatewayWpf.Views.Dialogs.ComposeMessage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     Bmessage.clr-namespace:SMSGatewayWpf.ViewModels.Message     Hrules6clr-namespace:SMSGatewayWpf.ViewModels.ValidationRules     jinteractivityPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     H DMahApps.Metro, Version=1.1.3.0, Culture=neutral, PublicKeyToken=null;Controls,http://metro.mahapps.com/winfx/xaml/controls  /styles"clr-namespace:SMSGatewayWpf.Styles    � WindowStartupLocation$  CenterScreen=�(  "MahApps.Metro.Controls.MetroWindow   ShowMaxRestoreButton .     ShowMinButton .   � 
ResizeMode$ NoResize=�   AccentColorBrush   	GlowBrush# �    � Title$ New message��$	��650��$	��550�� � 	Resources ?   9SMSGatewayWpf.ViewModels.ValidationRules.CheckedConverter  RadioButtonCheckedConverter �� MergedDictionaries �� Source��  �� $# ../../Styles/ButtonStyle.xaml?��� $I Cpack://application:,,,/MahApps.Metro.Resources;component/Icons.xaml?�%   &	         .��� 	 �� ClearTypeHint$	 Enabled=�&   System.Windows.Media.TextOptions
   TextFormattingMode$
 Display=�$��Stretch=�.+��� 
����ح�$��1q�$��0,0,0,0q�$��Stretch=�$
��Auto��._�8  2MahApps.Metro.Controls.TransitioningContentControl  -     Name$ transitionComposeMessage��$
��Auto��   
Transition$
 Left=�$��	9,-1,-9,1q�.��� $��	20,20,0,0q�  �� 
TargetType �� Property �� Value��%f   '��       '�� )     �� " ��.���  � ��$ 0 0 0 10q��� " ��.���  � ��$ 0 0 0 10q� � ColumnDefinitions �� $	��120 ���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��Phone number���� $	��300��$��25��$	��Top=�$
��Left=�$��0��$��1��*  $MahApps.Metro.Controls.TextBoxHelper   	Watermark$ Enter phone number��  SearchMetroTextBox#���  ����  �� Path$ Phonenumber� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors .    ButtonCommand �� *LookupContact+$ OneWay=�.��.  (System.Windows.Interactivity.Interaction   	Behaviors Q   KSMSGatewayWpf.ViewModels.ValidationRules.AllowableCharactersTextBoxBehavior     RegularExpression$ 	^[0-9;]+$��   	MaxLength$	 870��.���� $��1��$��0��$��Message���� -   $��
txtMessage��$��1��$��1��$	��100�� �� AcceptsReturn . $��3q�$��Stretch=�$��	0,0,40,15q�$ Enter your message������ $ Message�$ PropertyChanged=��� $��2��$��0��$
��Time���� $��2��$��1�� �� Orientation$ 
Horizontal=�.+�(  "System.Windows.Controls.DatePicker  $	��130��$��25��$��2��$��1��$	��Top=�$
��Left=�$ Select a date��   Text �� $
 Date�$ TwoWay=�$ PropertyChanged=��� $��2��$��1��$��15 0 0 0q�$
��hour���� -   $��txtTimeDate��$��3q� �� 	MaxLength$ 5��$��10, 0, 0, 10q�$��50��%   IsSpellCheckContextMenuEnabled . ���� $
 Time�$ PropertyChanged=�.��   $ 	^[0-9:]+$��$ 5��.���� $��3��$��0��$��Settings��� $��1��$��3�� ��%3   '	�       �� " 	�.���  � ��$ 0 0 0 10q� �� �� $
��Auto ��� �� .+��� -     ��Name$  
pnlOneTime��$��0��$��0��.+�	� -   ! 	�Name$! 
rdoOneTime��" 	� 	GroupName$" Trigger��$��0��$��0��$��One Time��# d� 	IsChecked# �� *TriggerOptions+$ �� 	Converter#$ [ 5	   /SMSGatewayWpf.ViewModels.Message.TriggerOptions% 	  OneTime& �� ConverterParameter#& Z% �� -   $  pnlDaily��$��1��$��0��.+�	� -   $! rdoDaily��$" Trigger��$��Daily��# �� *TriggerOptions+#$ [ ' 	  Daily#& Z' � $��15 0 0 0q�  �� $��45 ��� $
��Auto ��� .+��� $��0��$��every��$��40��$
��Left=��� $��1��$��35��$��3q�$ 3��$��Right=�{��� ( �� ElementName$( rdoDaily��$ 	IsChecked����� *	RecursDay+.��   $ ^[0-9]+$��$ 3��.���� $��2��$
��days��$
��Left=��� $��2��$��0��.+�	� -   $! 	rdoWeekly��$" Trigger��$��Weekly��# �� *TriggerOptions+#$ [ ) 	  Weekly#& Z) � $��15 0 0 0q� ��%-   '��       �� " ��.���  � ��$ 25�� �� �� ��  �� $��45 ��� $
��Auto ��� .+��� $��0��$��every��$��40��$
��Left=��� $��1��$��35��$��3q�$ 3��$
��Left=�{��� $( 	rdoWeekly��$ 	IsChecked����� *
RecursWeek+.��   $ ^[0-9]+$��$ 2��.���� $��2��$��weeks on��$
��Left=��� $��0��$��1��$��3��$ 
Horizontal=�{��� $( 	rdoWeekly��$ 	IsChecked� ��%C   '��       �� " ��.���  � ��$ 90����  � ��$ 20��.+��� $��Sunday��* d� IsThreeState* .  # �� *WeeklyOptions.Sunday+$ PropertyChanged=��� $��Monday��* .  # �� *WeeklyOptions.Monday+$ PropertyChanged=��� $��Tuesday��* .  # �� *WeeklyOptions.Tuesday+$ PropertyChanged=��� $��	Wednesday��* .  # �� *WeeklyOptions.Wednesday+$ PropertyChanged=��� $��0��$��2��$��3��$ 
Horizontal=�{��� $( 	rdoWeekly��$ 	IsChecked� ��%C   '��       �� " ��.���  � ��$ 90����  � ��$ 20��.+��� $��Thursday��* .  # �� *WeeklyOptions.Thursday+$ PropertyChanged=��� $��Friday��* .  # �� *WeeklyOptions.Friday+$ PropertyChanged=��� $��Saturday��* .  # �� *WeeklyOptions.Saturday+$ PropertyChanged=��� $��3��$��0��$��0 15 0 0q�.+�	� -	   $! 
rdoMonthly��$" Trigger��$��Monthly��# �� *TriggerOptions+#$ [ + 	  Monthly#& Z+ � $��15 0 0 0q� ��%s   '��       �� " ��.���  � ��$	 Top=���  � ��$
 Left=���  � ��$ 25����  � ��$	 180�� �� ��  �� $��80 ��� .+��� $��Months��.
   (SMSGatewayWpf.Styles.ComboWithCheckboxes
  $��0��$��1��$
��Auto��$
��Left=�, 
  DefaultText$, Select month��$��0,4,0,5q�$	��180��- 
  ItemsSource- �� *Months+{��� $( 
rdoMonthly��$ 	IsChecked��� $��1��$��0��$
��Days��
  $��1��$��1��$
��Auto��$
��Left=�$, Select days��$��0,0,0,6q�$	��180��- �� *Days+{��� $( 
rdoMonthly��$ 	IsChecked��� -
   . ��Name$. 
chkEnabled��$��4��$��0��* .  $��Enabled��/ � ToolTip$3/ -Disabled means message will be save as draft.��# �� *	Enabled+�� $��5��$��1��$��2��$ 
Horizontal=�$��0 30 0 0q�.+��� -   0 ��Name$0 btnOK��$��OK��$	��150��$��0 0 15 0q����� *SaveCommand+���� �� -   $0 	btnCancel��$��Cancel��$	��150��)  #MahApps.Metro.Controls.ProgressRing  $��25��$��25��$��20,0,0,0q�$
��Left=�1   IsActive1 �� $ IsBusy�$ PropertyChanged=�