using System;
using System.Collections.Generic;
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
        public float OnFalse { get; set; } = 0.0f;
        public FirstNeuron GetCloned()
        {
            return (FirstNeuron) this.MemberwiseClone();
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
                return OnFalse;
            }
        }
    }
    public class SecondNeuron
    {
        public List<FirstNeuron> firstNeuronRefList = new List<FirstNeuron>();
        public float OnTrue { get; set; } = 0.0f;
        public float OnFalse { get; set; } = 0.0f;
        public SecondNeuron GetCloned()
        {
            SecondNeuron result = new SecondNeuron();
            result.OnTrue = OnTrue;
            result.OnFalse = OnFalse;
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
            return sum;
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
                float newMax = secondNeuronRefTab[i].Calculate(img);
                if (newMax > max)
                {
                    max = newMax;
                    maxDigit = i;
                }
            }
            return maxDigit;
        }
    }
    public class Brain
    {
        public TextBox textBox;
        public AiImage img;
        public Random rnd = new Random();
        public int ans = -1;
        public ThirdNeuron thirdNeuron = new ThirdNeuron();
        public Brain(TextBox t, AiImage a)
        {
            textBox = t;
            img = a;
        }
        public Brain GetCloned()
        {
            Brain result = new Brain(textBox, img);
            result.ans = ans;
            result.thirdNeuron = thirdNeuron.GetCloned();
            return result;
        }
        public void SetAnswer()
        {
            if (ans == -1) textBox.Text = "NULL";
            else textBox.Text = ans.ToString();
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
                if (dA[i].digit == GetAnswer(img))
                {
                    score++;
                }
            }

            return score;
        }
        public void GeneticModification()
        {
            SecondNeuron a = thirdNeuron.secondNeuronRefTab[rnd.Next(10)];
            if (a == null) a = new SecondNeuron();
            a.OnTrue = (float)rnd.NextDouble();
            if (rnd.Next(2) == 1) a.OnTrue = -a.OnTrue;
            a.OnFalse = (float)rnd.NextDouble();
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
            b.OnFalse = (float)rnd.NextDouble();
            if (rnd.Next(2) == 1) b.OnTrue = -b.OnTrue;

            b.DrawingX = rnd.Next(100);
            b.DrawingY = rnd.Next(100);

        }
    }

    public class BigBrain
    {
        public List<DigitAnswer> DigitAnswersList;
        public TextBox textBox;
        public AiImage img;
        public Brain bestBrain = null;
        public BigBrain(List<DigitAnswer> digitAnswersList, AiImage a, TextBox t)
        {
            DigitAnswersList = digitAnswersList;
            textBox = t;
            img = a;
        }
        public List<Brain> brains = new List<Brain>();

        public void LearnOneGeneration()
        {
            if (bestBrain == null)
            {
                bestBrain = new Brain(textBox, img);
            }
            brains.Clear();
            for(int i = 0; i < 10000; i++)
            {
                brains.Add((Brain) this.bestBrain.GetCloned());
                brains[brains.Count - 1].GeneticModification();
            }

            for(int i = 0; i < brains.Count; i++)
            {
                if(bestBrain.calculateScore(DigitAnswersList) <= brains[i].calculateScore(DigitAnswersList))
                {
                    bestBrain = brains[i];
                }
            }
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
