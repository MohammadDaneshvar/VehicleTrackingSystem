namespace Framework.Domain.Domain
{
    public class VehiclePoint
    {
        public VehiclePoint(double x,double y,int SRID)
        {
            X = x;
            Y = y;
            this.SRID = SRID;
        }
        //public VehiclePoint(double x, double y)
        //{
        //    X = x;
        //    Y = y;
        //}
        public double X { get;  set; }
        public double Y { get;  set; }
        public int SRID { get;  set; }
    }
   
}
