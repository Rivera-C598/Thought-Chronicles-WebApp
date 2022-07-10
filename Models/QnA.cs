
namespace ThoughtChroniclesWebApp.Models

{
    public class QnA
    {
        
        public int ID { get; set; }
        public string TypeOfQuestion { get; set; }
        public string Questions{ get; set; }
        public string Answers { get; set; }
        public string Author { get; set; }
        public QnA()
        {

        }
    }
}
