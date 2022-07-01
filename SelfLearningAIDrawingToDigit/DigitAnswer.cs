using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfLearningAIDrawingToDigit
{
    public  class DigitAnswer
    {
        public AiImage image = null;
        public int digit = -1;
        public DigitAnswer(AiImage image, int digit)
        {
            this.image = image;
            this.digit = digit;
        }
        public DigitAnswer()
        {
        }
    }
}
