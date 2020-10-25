using GradeBook.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException();
            }

            var letterGradeLevelSize = (int)Math.Ceiling(Students.Count * .2);

            var orderedAverageGrades = Students.OrderBy(x => x.AverageGrade).Select(x => x.AverageGrade);

            var percentLevel = 0;
            for (int i = 0; i < Students.Count; i += letterGradeLevelSize)
            {
                var levelGrades = orderedAverageGrades.Skip(i).Take(letterGradeLevelSize);

                var minGrade = levelGrades.Min();
                var maxGrade = levelGrades.Max();

                if (averageGrade >= minGrade && averageGrade <= maxGrade)
                {
                    switch (percentLevel)
                    {
                        case 0:
                            return 'F';
                        case 1:
                            return 'D';
                        case 2:
                            return 'C';
                        case 3:
                            return 'B';
                        case 4:
                            return 'A';
                    }
                }

                percentLevel += 1;
            }

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
             else
            {
                base.CalculateStatistics();
            }
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            } 
            else
            {
                base.CalculateStudentStatistics(name);
            }
            
        }
    }
}
