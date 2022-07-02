using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SelfLearningAIDrawingToDigit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public AiImage aiImage = new AiImage();
        public List<DigitAnswer> DigitAnswersList = new List<DigitAnswer>();

        public Thread bigBrainThinkingThread = null;
        public bool bigBrainThinkingBool = false;

        BigBrain bigBrain = null;
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            bigBrain = new BigBrain(DigitAnswersList, aiImage, DigitDetectedText, GenerationText, ScoreText);
            bigBrain.LoadBestBrain();

            bigBrain.GetCalculatedDigitByBestBrain();
            DigitDetectedText.FontSize = 48;
            bigBrain.bestBrain.SetAnswer();
        }

        public static async Task SaveToFile(string text)
        {
            await File.WriteAllTextAsync("SavedImagesWithAnswers.ini", text);
        }

        public void LoadData()
        {
            // Read a text file line by line.  
            string[] lines;
            try
            {
                lines = File.ReadAllLines("SavedImagesWithAnswers.ini");
            }
            catch (Exception ex)
            {
                lines = null;
            }

            if (lines != null && lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    string[] stringDigits = line.Split(' ');
                    DigitAnswer tmp = new DigitAnswer();
                    tmp.digit = Int32.Parse(stringDigits[0]);
                    tmp.image = new AiImage();
                    for (int i = 0; i < 100; i++)
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            tmp.image.ImageTable[i, j] = Int32.Parse(stringDigits[i * 100 + j]) == 1;
                        }
                    }
                    DigitAnswersList.Add(tmp);
                }
            }
                
        }

        public void SaveData()
        {
            string buffer = "";
            for (int i = 0; i < DigitAnswersList.Count; i++)
            {
                if(i > 0) buffer += "\n";
                buffer += DigitAnswersList[i].digit;
                for (int j = 0; j < 100; j++)
                {
                    for (int k = 0; k < 100; k++)
                    {
                        if (DigitAnswersList[i].image.ImageTable[j, k] == true)
                        {
                            buffer += " 1";
                        }
                        else
                        {
                            buffer += " 0";
                        }

                    }
                }
            }
            SaveToFile(buffer);
        }

        private void DigitCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                int aiImageX = (int)e.GetPosition(this).X / aiImage.Scale;
                int aiImageY = (int)e.GetPosition(this).Y / aiImage.Scale;

                if (aiImageX < 0 || aiImageX >= 100 || aiImageY < 0 || aiImageY >= 100)
                {

                }
                else
                {
                    if (aiImage.ImageTable[aiImageX, aiImageY] == false)
                    {
                        aiImage.ImageTable[aiImageX, aiImageY] = true;
                    }

                }
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                int aiImageX = (int)e.GetPosition(this).X / aiImage.Scale;
                int aiImageY = (int)e.GetPosition(this).Y / aiImage.Scale;

                if (aiImageX < 0 || aiImageX >= 100 || aiImageY < 0 || aiImageY >= 100)
                {

                }
                else
                {
                    if (aiImage.ImageTable[aiImageX, aiImageY] == true)
                    {
                        aiImage.ImageTable[aiImageX, aiImageY] = false;
                    }
                }
            }

            aiImage.Render(DigitCanvas);


        }

        private void SetNumberButton0_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 0);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton1_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 1);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton2_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 2);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton3_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 3);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton4_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 4);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton5_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 5);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton6_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 6);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton7_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 7);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton8_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 8);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void SetNumberButton9_Click(object sender, RoutedEventArgs e)
        {
            DigitAnswer tmp = new DigitAnswer(aiImage, 9);
            DigitAnswersList.Add(tmp);
            aiImage = new AiImage();
            DigitCanvas.Children.Clear();
            aiImage.Render(DigitCanvas);
        }

        private void DigitCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(bigBrainThinkingBool == true)
            {
                DigitDetectedText.Text = "Stop training";
                DigitDetectedText.FontSize = 24;
            }
            else
            {
                int result = bigBrain.GetCalculatedDigitByBestBrain();
                DigitDetectedText.FontSize = 48;
            }
            
        }

        private void TrainAiButton_Click(object sender, RoutedEventArgs e)
        {
            bigBrainThinkingBool = true;
            TrainAiButton.IsEnabled = false;
            if(true)
            {
                bigBrainThinkingThread = new Thread(BigBrainThinking);
                bigBrainThinkingThread.Start();
            }
            
        }

        private void StopTrainingAiButton_Click(object sender, RoutedEventArgs e)
        {
            // Thread is resumed after pause so no need to close
            bigBrainThinkingBool = false;
            bigBrain.AbortLearning();
            while (bigBrainThinkingThread.IsAlive) ;
            TrainAiButton.IsEnabled = true;
            

            int result = bigBrain.GetCalculatedDigitByBestBrain();
            DigitDetectedText.FontSize = 48;

            bigBrain.bestBrain.SetAnswer();
            bigBrain.SaveBestBrain();
            SaveData();
        }

        public void BigBrainThinking()
        {
            while(bigBrainThinkingBool)
            {
                bigBrain.LearnOneGeneration();
            }

        }
    }
}
