   M S B A M L   `   `   `  ���� G  CCryptography, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:Cryptography.ViewModelCryptography.ViewModel  $    Cryptography.View.SeeUsersView   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6local$clr-namespace:Cryptography.ViewModel       � DataContext  �� *
SeeUsersVM+   Locator �� Source# [   � 	Resources   booleanToVisibility��%   &	       �� .��� ����  �� ImageSource$! ../Resources/Background.jpg�� j� Stretch$
 Fill=� � RowDefinitions �� $��60 ��� $��60 ��� $��* ��� $��50 �.+�� $��0��.+��� $*��$Ученици в системата��$��Monotype Corsiva)�$��40(�$��Center=�$��Center=�
��� ���$��135,2,102,2q�$	��363��� $��1�� � ColumnDefinitions �� $	��158 ��� $	��270 ��� $	��160 ��� $	��100 �.+��� $��0��$-��'Изберете потребител: ��$��Monotype Corsiva)�$��21,16,260,16q�$��16(�$��Center=�$��2���� -   $��cmbUser��$��1��$��22��$	��295��$��
38,18,0,20q�$
��Left=�$ �� IsSynchronizedWithCurrentItem$
 TrueY�$��2������ *
Students+ �� Mode$ TwoWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� SelectedItem
 �� *SelectedStudent+$ TwoWay=�$	 PropertyChanged=��� $��2��$)��#Предишни ползвания��$��92,18,20,20q�$	��150��$��22��$��2������ *	SeeUses+� $��2��.+��� $��
15,5,15,15q�$	��666������ *UsersInfoOrdered+ �� View �� .P��� $��Потреб. име�� �� Width$	 222�� �� DisplayMemberBinding �� *
Username+�� $��Име и фамилия��$	 222�� �� *StudentNames+�� $��Учител��$	 222�� �� *TeacherNames+�� $��3��$��
Назад��$��250,0,250,12q����� *Back+