using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            string[] hms = BreakTime(aTime);
            
            int hours = Convert.ToInt32(hms[0]);
            int minutes = Convert.ToInt32(hms[1]);
            int seconds = Convert.ToInt32(hms[2]);

            return BuildFirstLamp(seconds) + 
            BuildHoursLamp(hours) +
            ClearWhiteSpace(BuildMinutesLamp(minutes));
        }

        private string[] BreakTime(string aTime)
        {
            return aTime.Split(':');
        }

        private string BuildFirstLamp(int seconds)
        {
            return (seconds % 2 == 0 ? "Y" : "O") + Environment.NewLine;
        }

        private string ConcatRows(string firstRow, string secondRow)
        {
            return firstRow + secondRow;
        }

        private string ClearWhiteSpace(string lampsString)
        {
            return lampsString.TrimEnd();
        }

        private string CompleteLampsRow(int numberOfLamps, string lampsString, char character)
        {
            return lampsString.PadRight(numberOfLamps, character) + Environment.NewLine;
        }

        private string CompleteEmptyLampsRow(int numberOfLamps, string lampsString, char character)
        {
            for(int i = 0; i < numberOfLamps; i++)
            {
                lampsString += character;
            }

            return lampsString + Environment.NewLine;
        }

        private bool IsMinuteRedLamp(int position)
        {
            return position == 2 || position == 5 || position == 8;
        }

        private string BuildHoursLamp(int hours)
        {
            string hoursLampFirstRow = "";
            string hoursLampSecondRow = "";

            int hoursFirstRow = hours / 5;
            int hoursSecondRow = hours % 5;

            if (hoursFirstRow == 0)
                hoursLampFirstRow = CompleteEmptyLampsRow(4, hoursLampFirstRow, 'O');
            else
            {
                for(int i = 0; i < hoursFirstRow; i++)
                {
                    hoursLampFirstRow += "R";
                }

                hoursLampFirstRow = CompleteLampsRow(4, hoursLampFirstRow, 'O');
            }

            if (hoursSecondRow == 0)
                hoursLampSecondRow = CompleteEmptyLampsRow(4, hoursLampSecondRow, 'O');
            else
            {
                for (int i = 0; i < hoursSecondRow; i++)
                {
                    hoursLampSecondRow += "R";
                }

                hoursLampSecondRow = CompleteLampsRow(4, hoursLampSecondRow, 'O');
            }

            return ConcatRows(hoursLampFirstRow, hoursLampSecondRow);
        }

        private string BuildMinutesLamp(int minutes)
        {
            string minutesLampFirstRow = "";
            string minutesLampSecondRow = "";

            int minutesFirstRow = minutes / 5;
            int minutesSecondRow = minutes % 5;

            if (minutesFirstRow == 0)
                minutesLampFirstRow = CompleteEmptyLampsRow(11, minutesLampFirstRow, 'O');
            else
            {
                for (int i = 0; i < minutesFirstRow; i++)
                {
                    minutesLampFirstRow += IsMinuteRedLamp(i) ? "R" : "Y";
                }

                minutesLampFirstRow = CompleteLampsRow(11, minutesLampFirstRow, 'O');
            }

            if (minutesSecondRow == 0)
                minutesLampSecondRow = CompleteEmptyLampsRow(4, minutesLampSecondRow, 'O');
            else
            {
                for (int i = 0; i < minutesSecondRow; i++)
                {
                    minutesLampSecondRow += "Y";
                }

                minutesLampSecondRow = CompleteLampsRow(4, minutesLampSecondRow, 'O');
            }

            return ConcatRows(minutesLampFirstRow, minutesLampSecondRow);
        }
    }
}
