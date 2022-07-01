using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SelfLearningAIDrawingToDigit
{
    public class AiImage
    {
        public const int SizeX = 100;
        public const int SizeY = 100;
        public int Scale = 4;
        public bool[,] ImageTable = null;
        public bool[,] ImageTableBuffer = null;
        public AiImage()
        {
            ImageTable = new bool[SizeX, SizeY];
            ImageTableBuffer = new bool[SizeX, SizeY];
            for(int i = 0; i < SizeX; i++)
            {
                for(int j = 0; j < SizeY; j++)
                {
                    ImageTable[i, j] = false;
                    ImageTableBuffer[i, j] = false;
                }
            }
        }
        public void Render(Canvas canvas)
        {
            for(int i = 0; i < SizeX; i++)
            {
                for(int j = 0; j < SizeY; j++)
                {
                    if (ImageTableBuffer[i,j] != ImageTable[i, j])
                    {
                        if (ImageTable[i, j] == true)
                        {
                            Rectangle rectBlack = new System.Windows.Shapes.Rectangle();
                            rectBlack.Stroke = new SolidColorBrush(Colors.LightBlue);
                            rectBlack.Fill = new SolidColorBrush(Colors.LightBlue);
                            rectBlack.StrokeThickness = 1;
                            rectBlack.Width = 4;
                            rectBlack.Height = 4;
                            Canvas.SetLeft(rectBlack, i * 4);
                            Canvas.SetTop(rectBlack, j * 4);
                            canvas.Children.Add(rectBlack);
                        }
                        else
                        {
                            Rectangle rectBlue = new System.Windows.Shapes.Rectangle();
                            rectBlue.Stroke = new SolidColorBrush(Colors.LightBlue);
                            rectBlue.Fill = new SolidColorBrush(Colors.LightBlue);
                            rectBlue.StrokeThickness = 1;
                            rectBlue.Width = 4;
                            rectBlue.Height = 4;
                            Canvas.SetLeft(rectBlue, i * 4);
                            Canvas.SetTop(rectBlue, j * 4);
                            canvas.Children.Add(rectBlue);
                        }
                        ImageTableBuffer[i,j] = ImageTable[i,j];
                    }
                    
                }
            }
        }
    }
}
