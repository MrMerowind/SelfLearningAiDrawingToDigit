   M S B A M L   `   `   `  ���� W  SSelfLearningAIDrawingToDigit, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:SelfLearningAIDrawingToDigitSelfLearningAIDrawingToDigit  -    'SelfLearningAIDrawingToDigit.MainWindow   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     <local*clr-namespace:SelfLearningAIDrawingToDigit       � Title$(  "Self Learnging AI Drawing To Digit��$	��450��$	��650�����	#FF40AD23 � 
ResizeMode$ NoResize=�.���  � ColumnDefinitions �� $	��77* ��� $	��43* �.+��� -    ��Name$ DigitCanvas��$
��Left=�$	��Top=�$��400                 ��$	��400�����	#FF2CD4E8$��2���� -    ��Name$ label��$��Detected digit:��$
��Left=�$��	15,10,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.477,0.672&�$	��190��$��24(�$��1���� -    ��Name$ SetNumberButton0��$��0��$
��Left=�$��
25,133,0,0q�$	��Top=�$��22(�$��31��$��1���� -   $ SetNumberButton1��$��1��$
��Left=�$��
61,133,0,0q�$	��Top=�$��22(�$��31��$ 2.512,0.502&�$��1���� -   $ SetNumberButton2��$��2��$
��Left=�$��
97,133,0,0q�$	��Top=�$��22(�$��31��$��1��$ 0.234,0.563&��� -   $ SetNumberButton3��$��3��$
��Left=�$��133,133,0,0q�$	��Top=�$��22(�$��31��$��1���� -   $ SetNumberButton4��$��4��$
��Left=�$��169,133,0,0q�$	��Top=�$��22(�$��31��$��1���� -   $ SetNumberButton5��$��5��$
��Left=�$��
25,171,0,0q�$	��Top=�$��22(�$��31��$��1���� -	   $ SetNumberButton6��$��6��$
��Left=�$��
61,171,0,0q�$	��Top=�$��22(�$��31��$��1���� -
   $ SetNumberButton7��$��7��$
��Left=�$��
97,171,0,0q�$	��Top=�$��22(�$��31��$ 2.512,0.502&�$��1���� -   $ SetNumberButton8��$��8��$
��Left=�$��133,171,0,0q�$	��Top=�$��22(�$��31��$��1���� -   $ SetNumberButton9��$��9��$
��Left=�$��169,171,0,0q�$	��Top=�$��22(�$��31��$��1���� -   $ TrainAiButton��$��Train AI��$
��Left=�$��
20,250,0,0q�$	��Top=�$��43��$	��185��$��1���� -   $ StopTrainingAiButton��$��Stop Training��$
��Left=�$��
20,293,0,0q�$	��Top=�$��43��$	��185��$��1��${�False���� -    ��Name$ DigitDetectedText��$
��Left=�$��	20,57,0,0q� �� TextWrapping$
 Wrap=�$
��NULL��$	��Top=�$	��185��$��71��$��48(�	 �� TextAlignment$	 Center=�$��1���� -   $ GenerationText��$
��Left=�$��
20,217,0,0q�$
 Wrap=�$��Generation: 0��$	��Top=�$	��185��$��18��$��1���� -   $ 	ScoreText��$
��Left=�$��
20,232,0,0q�$
 Wrap=�$��
Score: 0/0��$	��Top=�$	��185��$��18��$ 0.5,0.5&�$��1���� -   $ 
SaveButton��$��1��$
��Save��$
��Left=�$��
20,336,0,0q�$	��Top=�$��43��$	��185��${�False��