using System.Text;
public class Plural
{
    public string MakePlural(int number, string[] formats)
    {
        string answer = "";
        int remainder = number % 100;
        if(11 <= number && number <= 14)
        {
            answer += formats[2];
        }
        else
        {
            remainder = remainder % 10;
            if(remainder == 1)
            {
                answer += formats[0];
            }
            else if(remainder >= 2 && remainder <= 4)
            {
                answer += formats[1];
            }
            else
            {
                answer += formats[2];
            }
        }
        return answer;
    }
}