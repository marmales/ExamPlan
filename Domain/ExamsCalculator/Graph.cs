using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Domain.ExamsCalculator
{
    public class Graph
    {
        // =============================================    PRIVATE VARIABLES    ============================================= //
        private IList<Vertex> _vertexes;
        private bool[,] _adjacencyMatrix;
        private DateTime _beggining;
        // =============================================       PROPERTIES        ============================================= //
        public int Capacity { get { return _vertexes.Count; } }

        // =============================================      CONSTRUCTORS       ============================================= //
        public Graph(IList<Vertex> vertexes, DateTime FirstExamTime)
        {
            _vertexes = vertexes;
            _beggining = FirstExamTime;
        }

        // =============================================     PUBLIC METHODS     ============================================= //
        public IList<Vertex> ColorOurGraph()
        {
            CreateMatrix(); // Set adjacencyMatrix
            bool connected; // this variable checks if current vertex has the same color as his neighbour (in adjacency matrix)

            foreach (var item in _vertexes)
            {
                item.StartHour = _beggining;
                item.Color = 0;
            }

            for (int i = 0; i < this.Capacity; i++)
            {
                connected = true;
                while (connected)
                {
                    ++_vertexes[i].Color;
                    connected = checkNeighbour(i);
                }
                
            }
            return _vertexes;
        }

        public void GetJSdata(string pathy) // Zapisywanie wyniku działania programu do pliku z rozszerzeniem "*.JS
        {
            //tables.Add("exams", writeList());

            var path =  HttpContext.Current.Server.MapPath(pathy);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
            {
                file.WriteLine(writeEdge());
                file.WriteLine(writeNodes());
            }
        }


        // =============================================     PRIVATE METHODS     ============================================= 
        private void CreateMatrix() // Conflict Matrix
        {
            _adjacencyMatrix = new bool[this.Capacity, this.Capacity];
            for (int i = 0; i < this.Capacity; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    _adjacencyMatrix[i, j] = Vertex.FindConflicts(_vertexes[i], _vertexes[j]);
                    _adjacencyMatrix[j, i] = _adjacencyMatrix[i, j];
                }
            }
        }
        private bool checkNeighbour(int currentVertex)
        {

            int CurrentVertexColor = _vertexes[currentVertex].Color;

            for (int i = 0; i < this.Capacity; i++)
            {
                if (_adjacencyMatrix[currentVertex, i])
                {
                    int iterationColor = _vertexes[i].Color;
                    TimeSpan iterationExamLength = TimeSpan.FromMinutes(_vertexes[i].LengthInMinutes);

                    if ((CurrentVertexColor > iterationColor && _vertexes[currentVertex].StartHour < _vertexes[i].StartHour + iterationExamLength)
                        || (CurrentVertexColor == iterationColor)
                        || (CurrentVertexColor < iterationColor && _vertexes[currentVertex].StartHour + TimeSpan.FromMinutes(_vertexes[currentVertex].LengthInMinutes) > _vertexes[i].StartHour))
                    {
                        _vertexes[currentVertex].StartHour = _vertexes[i].StartHour.Value.AddMinutes(_vertexes[i].LengthInMinutes);
                        return true;
                    } // IF exam will pass this longs terms new beggining hour will set and current vertex color will increase
                }
            }

            return false;
        }

        

        private string writeNodes() // Creates variable for .js file ( var node =.... )
        {
            string nodes = "var nodes = [";

            for (int i = 0; i < this.Capacity; i++)
                nodes += node(_vertexes[i], i) + ",";

            return nodes + "]";
        }
        private string writeEdge() // Creates variable for .js file ( var vertex =.... )
        {
            string edges = "var edges = [";

            for (int i = 0; i < this.Capacity; i++)
                for (int j = 0; j < i; j++)
                    if (_adjacencyMatrix[i, j] == true)
                        edges += edge(i, j) + ",";

            return edges + "]";
        }
        //private string writeList() // Creates variable for .js file ( var exams =.... )
        //{
        //    string exams = "";

        //    for (int i = 0; i < this.Capacity; i++)
        //        exams += exam(_vertexes[i]);

        //    return exams;
        //}

        private string node(Vertex node, int i) // Example: {id:0,label:Analiza,group:1} -> group is the Vertex color
        {
            //return "{id:" + i + ",label:\"" + node.ExamName + "\",group:" + node.Color + "}";
            return "{" + $@"id:{i},label:""{node.ExamName}"",group:{node.Color}" + "}";
        }
        private string edge(int i, int j) // Example: {from:2,to:4,color:"ffffff"} -> {from adjacencyMatrix[2], to adjacencyMatrix[4]}
        {
            //return "{from:" + Convert.ToString(i) + ",to:" + Convert.ToString(j) + ",color:\"#ffffff\"}";
            return "{" + $"from:{i},to:{j},color:\"ffffff\"" + "}";
        }
        //private string exam(Vertex node) // Example {myLabel:Analiza, myTime:"9:00 - 10:45}
        //{
        //    Vertex node1 = node;
        //    //return "{myLabel:\"" + node.ExamName + "\", myTime:\"" + (7 + node.Color * 2) + ":00 - " + (8 + node.Color * 2) + ":45\"},";
        //    return "{" +  $"myLabel:{node.ExamName}, myTime:{node.StartHour.ToString()} - {node.StartHour.Value.AddMinutes(node.LengthInMinutes).ToString()}" + "},";
        //}

    }
}
