using ExamSystem.Logic;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.Models.Enums;
using ExamSystem.UI.Error_Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExamSystem.Models.Normal_Classes
{
    public class McQuestion : Question
    {
        // SMELLS : Using StringBuilder For Static text and append o n that text later 
        public McQuestion(string? body, int marks) : base(QuestionType.MCQ, body, marks)
        {
            AnswersList = new List<Answer>
            {
                new Answer(1, "A:"),
                new Answer(2, "B:"),
                new Answer(3, "C:"),
                new Answer(4, "D:")
            };
        }

        public override object Clone()
        {
            return new McQuestion(Body, Marks)
            {
                RightAnswerId = RightAnswerId,
                ChosenAnswerId = ChosenAnswerId,
                AnswersList = AnswersList?.Select(a => (Answer)a.Clone()).ToList()
            };
        }

        public override void SetRightAnswerId(int Id)
        {
            if (Validators.IsValidId(this, Id))
            {
                RightAnswerId = Id;
            }
            else
            {
                throw new InvalidOperationException(Constants.InvalidMcQuestionRightAnswerId);
            }
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Question Type: {Header}");
            sb.AppendLine($"Body: {Body}");
            sb.AppendLine($"Marks: {Marks}");
            sb.AppendLine("Answers:");
            if (AnswersList != null)
            {
                foreach (var answer in AnswersList)
                {
                    sb.AppendLine(answer.ToString());
                }
            }
            return sb.ToString();
        }
    }
}
