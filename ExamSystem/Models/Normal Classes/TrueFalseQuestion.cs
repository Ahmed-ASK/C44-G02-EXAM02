using ExamSystem.Logic;
using ExamSystem.Models.AbstractClasses;
using ExamSystem.UI.Error_Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamSystem.Models.Normal_Classes
{
    public class TrueFalseQuestion : Question
    {
        public TrueFalseQuestion(string? body, int marks)
            : base(Models.Enums.QuestionType.TrueFalse, body, marks)
        {
            AnswersList = new List<Answer>
            {
                new Answer(1, "True"),
                new Answer(2, "False")
            };
        }
        public override object Clone()
        {
            return new TrueFalseQuestion(Body, Marks)
            {
                RightAnswerId = RightAnswerId,
                ChosenAnswerId = ChosenAnswerId,
                AnswersList = AnswersList?.Select(a => (Answer)a.Clone()).ToList()
            };
        }

        public override void SetRightAnswerId(int id)
        {
            while (!Validators.IsValidId(this, id))
            {
                Console.WriteLine(Constants.InvalidTrueFalseQuestionRightAnswerId);
                Console.Write("Please enter a valid right answer ID: ");
                int.TryParse(Console.ReadLine(), out id);
            }
            RightAnswerId = id;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Question Type: {Header}");
            sb.AppendLine($"Body: {Body}");
            sb.AppendLine($"Marks: {Marks}");
            sb.AppendLine("Answers:");
            foreach (var answer in AnswersList ?? Enumerable.Empty<Answer>())
            {
                sb.AppendLine(answer.ToString());
            }
            return sb.ToString();
        }
    }
}
