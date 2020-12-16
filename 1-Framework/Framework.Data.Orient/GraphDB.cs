using Framework.Domain.Enum;

namespace Framework.Data.Orient
{
    public class GraphDB : IGraphDB
    {   
        private string _address = "localhost:2424";
        private string _databaseName = "person";
        private string _user = "root";
        private string _passwd = "root_passwd";

        public GraphDB(string host,string port, string user,string pass,string DbName)
        {
            _address = host + ":" + port;
            _user = user;
            _passwd = pass;
            _databaseName = DbName;
        }
        public string RunQuery(string json)
        {
                string webAddr = @"http://" + _address + "/command/" + _databaseName + "/sql/-/25000?format=rid,type,version,class,ORID,OClassName,graph";

                var httpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(webAddr);
                httpWebRequest.Accept = "*/*";
                httpWebRequest.ContentType = "application/json;charset=utf-8";
                httpWebRequest.Method = "Post";
                httpWebRequest.Headers["Authorization"] = "Basic " + System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", _user, _passwd)));
                httpWebRequest.Timeout = 600000;
                byte[] postBytes = System.Text.Encoding.UTF8.GetBytes(json);
                System.IO.Stream requestStream = httpWebRequest.GetRequestStream();
                requestStream.Write(postBytes, 0, postBytes.Length);
                requestStream.Close();
                var httpResponse = (System.Net.HttpWebResponse)httpWebRequest.GetResponse();
                //return httpResponse;
                requestStream = httpResponse.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(requestStream);
                string htmlString = reader.ReadToEnd();
                reader.Close();
                httpResponse.Close();
                return htmlString;
        }

    }
}
