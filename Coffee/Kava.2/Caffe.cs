namespace Kava._2
{
    public class Caffe
    {
        public ObservableQueue<Visitor> VisitorsQueue { get; private set; }

        public Table[] Tables { get; private set; }

        public Caffe()
        {
            VisitorsQueue = new ObservableQueue<Visitor>();
            Tables = new Table[Constants.tablesCount];
            for (var i = 0; i < Tables.Length; i++)
            {
                Tables[i] = new Table(string.Format("столик №{0}", i + 1));
            }
        }
    }
}