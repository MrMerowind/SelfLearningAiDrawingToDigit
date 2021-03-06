using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SelfLearningAIDrawingToDigit
{
    public class FirstNeuron
    {
        public int? DrawingX { get; set; }
        public int? DrawingY { get; set; }
        public float OnTrue { get; set; } = 0.0f;
        public FirstNeuron GetCloned()
        {
            return (FirstNeuron) this.MemberwiseClone();
        }
        public void GetSave(StreamWriter sw)
        {
            sw.Write(DrawingX.ToString() + " " + DrawingY.ToString() + " " + OnTrue.ToString());
        }
        public void LoadFirstNeuron(string data)
        {
            if(data == null || data.Length == 0) return;
            string[] dataLines = data.Split(' ');
            DrawingX = Int32.Parse(dataLines[0]);
            DrawingY = Int32.Parse(dataLines[1]);
            OnTrue = float.Parse(dataLines[2]);
        }
        public float Calculate(AiImage img)
        {
            if (DrawingX == null || DrawingY == null || DrawingX < 0 || DrawingX >= 100 | DrawingY < 0 || DrawingY >= 100)
            {
                return 0.0f;
            }
            else if (img.ImageTable[(int) DrawingX, (int) DrawingY] == true)
            {
                return OnTrue;
            }
            else
            {
                return 0.0f;
            }
        }
    }
    public class SecondNeuron
    {
        public List<FirstNeuron> firstNeuronRefList = new List<FirstNeuron>();
        public float OnTrue { get; set; } = 0.0f;
        public void GetSave(StreamWriter sw)
        {
            if(firstNeuronRefList.Count > 0)
            {
                sw.Write(OnTrue.ToString());
                for (int i = 0; i < firstNeuronRefList.Count; i++)
                {
                    sw.Write(";");
                    firstNeuronRefList[i].GetSave(sw);
                }
            }
        }
        public void LoadSecondNeuron(string data)
        {
            if (data == null || data.Length == 0 || data == "") return;
            string[] dataLines = data.Split(';');
            OnTrue = float.Parse(dataLines[0]);
            for(int i = 1; i < dataLines.Length; i++)
            {
                firstNeuronRefList.Add(new FirstNeuron());
                firstNeuronRefList[firstNeuronRefList.Count - 1].LoadFirstNeuron(dataLines[i]);
            }
        }
        public SecondNeuron GetCloned()
        {
            SecondNeuron result = new SecondNeuron();
            result.OnTrue = OnTrue;
            for(int i = 0; i < firstNeuronRefList.Count; i++)
            {
                result.firstNeuronRefList.Add(firstNeuronRefList[i].GetCloned());
            }
            return result;
        }
        public float Calculate(AiImage img)
        {
            float sum = 0.0f;
            for(int i = 0; i < firstNeuronRefList.Count; i++)
            {
                sum += firstNeuronRefList[i].Calculate(img);
            }
            if (sum > 0.0f) return OnTrue;
            else return 0.0f;
        }
    }
    public class ThirdNeuron
    {
        public SecondNeuron[] secondNeuronRefTab = new SecondNeuron[10];
        public ThirdNeuron GetCloned()
        {
            ThirdNeuron result = new ThirdNeuron();
            for(int i = 0; i < 10; i++)
            {
                if(secondNeuronRefTab[i] != null) result.secondNeuronRefTab[i] = secondNeuronRefTab[i].GetCloned();
            }
            return result;
        }
        public void GetSave(StreamWriter sw)
        {
            for (int i = 0; i < secondNeuronRefTab.Length; i++)
            {
                if (i > 0) sw.Write("|");
                if (secondNeuronRefTab[i] != null) secondNeuronRefTab[i].GetSave(sw);
            }
        }
        public void LoadThirdNeuron(string data)
        {
            string[] dataLines = data.Split('|');
            for(int i = 0; i < 10; i++)
            {
                if(dataLines[i] != null && dataLines[i] != "")
                {
                    secondNeuronRefTab[i] = new SecondNeuron();
                    secondNeuronRefTab[i].LoadSecondNeuron(dataLines[i]);
                }
            }
        }
        public int GetHighestDigit(AiImage img)
        {
            if(secondNeuronRefTab[0] == null)
            {
                return -1;
            }
            float max = secondNeuronRefTab[0].Calculate(img);
            int maxDigit = 0;
            for(int i = 1; i < 10; i++)
            {
                if (secondNeuronRefTab[i] != null)
                {
                    float newMax = secondNeuronRefTab[i].Calculate(img);
                    if (newMax > max)
                    {
                        max = newMax;
                        maxDigit = i;
                    }
                }
                
            }
            return maxDigit;
        }
    }
    public class Brain
    {
        public TextBox textBox;
        public TextBox scoreTextBox;
        public TextBox generationTextBox;
        public int score = 0;
        public int maxScore = 0;
        public int bestBrainGeneration = 0;
        public AiImage img;
        public Random rnd = new Random();
        public int ans = -1;
        public ThirdNeuron thirdNeuron = new ThirdNeuron();
        public Brain(TextBox t, AiImage a, TextBox ts, TextBox tg)
        {
            textBox = t;
            img = a;
            scoreTextBox = ts;
            generationTextBox = tg;
        }
        public void SaveBrain()
        {
            File.WriteAllTextAsync("SavedNeuralNetwork.ini", "");

            using (StreamWriter sw = File.AppendText("SavedNeuralNetwork.ini"))
            {
                sw.Write(bestBrainGeneration.ToString());
                sw.Write("\n");
                thirdNeuron.GetSave(sw);
            }
        }
        public void LoadBrain()
        {
            string[] lines;
            try
            {
                lines = File.ReadAllLines("SavedNeuralNetwork.ini");
            }
            catch (Exception ex)
            {
                lines = null;
            }
            if(lines != null)
            {
                bestBrainGeneration = Int32.Parse(lines[0].ToString());
                thirdNeuron = new ThirdNeuron();
                thirdNeuron.LoadThirdNeuron(lines[1]);

            }
        }
        public Brain GetCloned()
        {
            Brain result = new Brain(textBox, img, scoreTextBox, generationTextBox);
            result.ans = ans;
            result.thirdNeuron = thirdNeuron.GetCloned();
            return result;
        }
        public void SetAnswer()
        {
            if (ans == -1) textBox.Text = "NULL";
            else textBox.Text = ans.ToString();

            generationTextBox.Text = "Generation: " + bestBrainGeneration.ToString();
            scoreTextBox.Text = "Score: " + score.ToString() + "/" + maxScore.ToString();
        }
        public int GetAnswer(AiImage img)
        {
            ans = thirdNeuron.GetHighestDigit(img);
            return ans;
        }
        public int calculateScore(List<DigitAnswer> dA)
        {
            int score = 0;

            for (int i = 0; i < dA.Count; i++)
            {
                if (dA[i].digit == GetAnswer(dA[i].image))
                {
                    score++;
                }
            }

            return score;
        }
        public void GeneticModification()
        {
            int randomNumber = rnd.Next(10);
            SecondNeuron a = thirdNeuron.secondNeuronRefTab[randomNumber];
            if (a == null) a = thirdNeuron.secondNeuronRefTab[randomNumber] = new SecondNeuron();
            a.OnTrue = (float)rnd.NextDouble();
            if (rnd.Next(2) == 1) a.OnTrue = -a.OnTrue;

            FirstNeuron b;

            if (a.firstNeuronRefList.Count == 1000)
            {
                b = a.firstNeuronRefList[rnd.Next(1000)];
            }
            else
            {
                a.firstNeuronRefList.Add(new FirstNeuron());
                b = a.firstNeuronRefList[a.firstNeuronRefList.Count - 1];
            }

            b.OnTrue = (float)rnd.NextDouble();
            if (rnd.Next(2) == 1) b.OnTrue = -b.OnTrue;

            b.DrawingX = rnd.Next(100);
            b.DrawingY = rnd.Next(100);

        }
    }

    public class BigBrain
    {
        public List<DigitAnswer> DigitAnswersList;
        public TextBox textBox;
        public TextBox scoreTextBox;
        public TextBox generationTextBox;
        public int bestBrainGeneration = 0;
        private bool abortLearnignBool = false;
        public AiImage img;
        public Brain bestBrain = null;

        public void SaveBestBrain()
        {
            if (bestBrain == null) return;
            bestBrain.SaveBrain();
        }
        
        public void LoadBestBrain()
        {
            bestBrain = new Brain(textBox,img,scoreTextBox,generationTextBox);
            bestBrain.LoadBrain();
            bestBrainGeneration = bestBrain.bestBrainGeneration;
        }

        public BigBrain(List<DigitAnswer> digitAnswersList, AiImage a, TextBox t, TextBox tg, TextBox ts)
        {
            DigitAnswersList = digitAnswersList;
            textBox = t;
            img = a;
            scoreTextBox = ts;
            generationTextBox = tg;
        }
        public List<Brain> brains = new List<Brain>();

        public void LearnOneGeneration()
        {
            if (bestBrain == null)
            {
                bestBrain = new Brain(textBox, img, scoreTextBox, generationTextBox);
            }
            brains = new List<Brain>();
            for(int i = 0; i < 1000; i++)
            {
                brains.Add((Brain) this.bestBrain.GetCloned());
                brains[brains.Count - 1].GeneticModification();
                if (abortLearnignBool == true)
                {
                    abortLearnignBool = false;
                    return;
                }
            }

            for(int i = 0; i < brains.Count; i++)
            {
               
                if (bestBrain.calculateScore(DigitAnswersList) <= brains[i].calculateScore(DigitAnswersList))
                {
                    bestBrain = brains[i];
                }
                if(abortLearnignBool == true)
                {
                    abortLearnignBool = false;
                    bestBrainGeneration++;
                    bestBrain.bestBrainGeneration = bestBrainGeneration;
                    bestBrain.score = bestBrain.calculateScore(DigitAnswersList);
                    bestBrain.maxScore = DigitAnswersList.Count;
                    return;
                }
            }
            bestBrainGeneration++;
            bestBrain.bestBrainGeneration = bestBrainGeneration;
            bestBrain.score = bestBrain.calculateScore(DigitAnswersList);
            bestBrain.maxScore = DigitAnswersList.Count;
        }
        public void AbortLearning()
        {
            abortLearnignBool = true;
        }
        public int GetCalculatedDigitByBestBrain()
        {
            int result = -1;
            if(bestBrain != null)
            {
                result = bestBrain.GetAnswer(img);
                bestBrain.SetAnswer();
            }

            return result;
        }
    }
}
