namespace Exercies
{
    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            { 0, 1, 0, 1, 0, 0 },
            { 1, 0, 1, 1, 0, 0 },
            { 0, 1, 0, 0, 0, 0 },
            { 1, 1, 0, 0, 1, 0 },
            { 0, 0, 0, 1, 0, 1 },
            { 0, 0, 0, 0, 1, 0 },
        };

        List<int>[] adj2 = new List<int>[]
        {
            new List<int> { 1, 3 },
            new List<int> { 0, 2, 3 },
            new List<int> { 1 },
            new List<int> { 0, 1, 4, },
            new List<int> { 3, 5 },
            new List<int> { 4 },
        };

        bool[] visited = new bool[6];
        // 1) now 방문
        // 2) now와 연결된 정점들을 하나씩 확인해서 아직 미방문 상태라면 방문
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;

            for(int next = 0;next < 6; next++)
            {
                //연결되어 있지 않으면 스킵
                if (adj[now, next] == 0)
                    continue;

                //이미 방문했으면 스킵
                if (visited[next]) 
                    continue;

                DFS(next);
            }
        }

        public void DFS2(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;

            foreach(int next in adj2[now])
            {
                //이미 방문했으면 스킵
                if (visited[next])
                    continue;

                DFS2(next);
            }
        }

        //끊어져 있는 Edge가 있더라도 전체 탐색 가능        
        public void SearchAll()
        {
            visited = new bool[6];

            //모든 방문 가능한 정점은 모두 방문한다.
            for(int now = 0; now < 6; now++)
            {                
                if (visited[now] == false)
                    DFS(now);
            }
        }
    }

    class Program
    {       
        static void Main(string[] args)
        {
            // DFS (Depth First Search : 깊이 우선 탐색)
            // BFS (Breadth First Search : 너비 우선 탐색)
            Graph graph = new Graph();
            graph.SearchAll();
        }
    }
}