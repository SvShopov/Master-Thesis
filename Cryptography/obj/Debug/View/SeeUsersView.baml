   M S B A M L   `   `   `  ����G  CCryptography, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Cryptography.ViewModelCryptography.ViewModel  5      $    Cryptography.View.SeeUsersView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6local$clr-namespace:Cryptography.ViewModel     5        � DataContext  5	      �� *
SeeUsersVM+   Locator �� Source# [   � 	Resources 5        booleanToVisibility��%   &	       �� 5   
   6'   5      .��5      � ��5   
   �� 5       �� ImageSource$! ../Resources/Background.jpg��6    j� Stretch$
 Fill=�6C    � RowDefinitions 5   
   �� 5      $��60 �6   �� 5      $��60 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   � $��0��6   .+�5      �� $*��$Ученици в системата��6   $��Monotype Corsiva)�62   $��40(�6P   $��Center=�6^   $��Center=�6{   
��� ���$��135,2,102,2q�6�   $	��363��6�   6�   5      � 5   
   $��1��6    � ColumnDefinitions 5       �� 5!      $	��158 �6#   �� 5"      $	��270 �6#   �� 5#      $	��160 �6#   �� 5$      $	��100 �6#   .+�5&      �� $��0��6   $-��'Изберете потребител: ��6$   $��Monotype Corsiva)�6D   $��21,16,260,16q�6b   $��16(�6x   $��Center=�6�   $��2��6�   6�   �� 5'      -   5        $��cmbUser��5'   '   $��1��6   $��22��66   $	��295��6B   $��
38,18,0,20q�6N   $
��Left=�6b   $ �� IsSynchronizedWithCurrentItem$
 TrueY�5)   r   $��2��6�   ��5(      �� *
Students+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� SelectedItem
 5)      �� *SelectedStudent+$ TwoWay=�$	 PropertyChanged=�6�   �� 5*      $��2��6   $)��#Предишни ползвания��6%   $��92,18,20,20q�6B   $	��150��6W   $��22��6c   $��2��6�   ��6o   �� *	SeeUses+6�   5+      � 5-   
   $��2��6   .+�5.      �� $��
15,5,15,15q�6@   $	��666��6T   ��6   �� *UsersInfoOrdered+ �� View 5/      �� 50      .P�51      �� $��Потреб. име��6)    �� Width$	 222��6>    �� DisplayMemberBinding 6J   �� *
Username+�� 52      $��Име и фамилия��6)   $	 222��6@    6L   �� *StudentNames+�� 53      $��Учител��6)   $	 222��69    6E   �� *TeacherNames+54      56      58      �� 5:   
   $��3��6   $��
Назад��6   $��250,0,250,12q�6.   ��6D   �� *Back+6_   5<      5=      