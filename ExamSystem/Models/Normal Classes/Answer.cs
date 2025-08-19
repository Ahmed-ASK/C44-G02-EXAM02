using System.Text;

namespace ExamSystem.Models.AbstractClasses
{
    public class Answer : ICloneable
    {
        public int Id { get; set; }
        public StringBuilder AnswerText { get; set; } = new StringBuilder();

        public Answer(int id, string answerText)
        {
            Id = id;
            AnswerText.Append(answerText ?? string.Empty);
        }
        public override string ToString() =>
            $"{AnswerText}";

        public object Clone()
        {
            return new Answer(Id, AnswerText.ToString());
        }
    }
}