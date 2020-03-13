
namespace ExpressionTreesBasics.Classes
{
    public class DemoClass
    {
        public int IntValue { get; set; }


        public DemoClass() : this(123)
        {
        }

        public DemoClass(int v) => IntValue = v;


        public string DemoMethod(int x, int y)
            => (x + y).ToString();
    }
}
