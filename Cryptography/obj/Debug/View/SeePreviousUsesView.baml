   M S B A M L   `   `   `  ����G  CCryptography, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Cryptography.ViewModelCryptography.ViewModel  5      +    %Cryptography.View.SeePreviousUsesView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Cryptography.ViewModel     5        � DataContext  5	      �� *SeePreviousUsesVM+   Locator �� Source# [   � 	Resources 5        booleanToVisibility��%   &	       �� 5   
   6'   5      .��5      � ��5   
   �� 5       �� ImageSource$! ../Resources/Background.jpg��6    j� Stretch$
 Fill=�6C    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   � $��0��6   .+�5      �� $-��'Предишни използвания��6   $��Monotype Corsiva)�63   $��40(�6Q   $��Center=�6_   $��Center=�6|   
��� ���$��207,2,207,2q�6�   $	��365��6�   6�   5      � 5   
   $��1��6   .+�5      �� $��
15,0,15,10q�6?   ��6   �� *UsesInfoOrdered+ �� View 5       �� 5!      .P�5"      �� $��Час и дата��6)    �� Width$	 150��6=    �� DisplayMemberBinding 6I   �� *
TimeDate+�� 5#      $+��%Криптографски метод��6)   $	 200��6F    6R   �� *
MethodName+�� 5$      $"��Режим на работа��6)   $	 110��6B    6N   �� *
WorkMode+�� 5%      $��Входни данни��6)   $	 355��6?    6K   �� *InputArguments+�� 5&      $��Резултат��6)   $	 200��6;    6G   �� *Result+5'      5)      5+      �� 5-   
   $��2��6   $��
Назад��6   $��416,0,409,10q�6.   ��6D   �� *Back+6_   5/      50      