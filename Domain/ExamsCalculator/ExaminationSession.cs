using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Domain.ExamsCalculator
{
    public class ExaminationSession
    {
        
        private IList<ExamVertex> _vertexes;
        private bool[,] _adjacencyMatrix;
        private DateTime _firstExamDate;
        
        public int Capacity { get { return _vertexes.Count; } }

        
        public ExaminationSession(IList<ExamVertex> vertexes, DateTime FirstExamDate)
        {
            _vertexes = vertexes;
            this._firstExamDate = FirstExamDate;
        }

        
        public IList<ExamVertex> ColorOurGraph()
        {
            CreateAdjacencyMatrix(); 
            bool connected; // this variable checks if current vertex has the same color as his neighbour (in adjacency matrix)

            setStartDate();

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

        public void CreateJSdata(string pathy)
        {
            var path =  HttpContext.Current.Server.MapPath(pathy);

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path, false))
            {
                file.WriteLine(writeEdge());
                file.WriteLine(writeNodes());
            }
        }


        private void CreateAdjacencyMatrix()
        {
            _adjacencyMatrix = new bool[this.Capacity, this.Capacity];
            for (int i = 0; i < this.Capacity; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    _adjacencyMatrix[i, j] = ExamVertex.FindConflicts(_vertexes[i], _vertexes[j]);
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

        private void setStartDate()
        {
            foreach (var item in _vertexes)
            {
                item.StartHour = _firstExamDate;
                item.Color = 0;
            }
        }

        private string writeNodes()
        {
            string nodes = "var nodes = [";

            for (int i = 0; i < this.Capacity; i++)
                nodes += node(_vertexes[i], i) + ",";

            return nodes + "]";
        }
        private string writeEdge()
        {
            string edges = "var edges = [";

            for (int i = 0; i < this.Capacity; i++)
                for (int j = 0; j < i; j++)
                    if (_adjacencyMatrix[i, j] == true)
                        edges += edge(i, j) + ",";

            return edges + "]";
        }

        private string node(ExamVertex Examnode, int i) // Example: {id:0,label:Analiza,group:1} -> group is the Vertex color
        {
            return "{" + $@"id:{i},label:""{Examnode.ExamName}"",group:{Examnode.Color}" + "}";
        }
        private string edge(int i, int j) // Example: {from:2,to:4,color:"ffffff"} -> {from adjacencyMatrix[2], to adjacencyMatrix[4]}
        {
            return "{" + $"from:{i},to:{j},color:\"ffffff\"" + "}";
        }

    }
}
