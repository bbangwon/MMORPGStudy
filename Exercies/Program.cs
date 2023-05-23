namespace Exercies
{
    class TreeNode<T>
    {
        public T Data { get; set; }
        public List<TreeNode<T>> Children { get; set; } = new List<TreeNode<T>>();
    }

    class Graph
    {
        int[,] adj = new int[6, 6]
        {
            { -1, 15, -1, 35, -1, -1 },
            { 15, -1, 05, 10, -1, -1 },
            { -1, 05, -1, -1, -1, -1 },
            { 35, 10, -1, -1, 05, -1 },
            { -1, -1, -1, 05, -1, 05 },
            { -1, -1, -1, -1, 05, -1 },
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
        #region DFS
        // 1) now 방문
        // 2) now와 연결된 정점들을 하나씩 확인해서 아직 미방문 상태라면 방문
        public void DFS(int now)
        {
            Console.WriteLine(now);
            visited[now] = true;

            for (int next = 0; next < 6; next++)
            {
                //연결되어 있지 않으면 스킵
                if (adj[now, next] == -1)
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

            foreach (int next in adj2[now])
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
            for (int now = 0; now < 6; now++)
            {
                if (visited[now] == false)
                    DFS(now);
            }
        } 
        #endregion

        public void BFS(int start)
        {
            bool[] found = new bool[6];

            int[] parent = new int[6];
            int[] distance = new int[6];
            
            Queue<int> q = new Queue<int>();
            q.Enqueue(start);
            found[start] = true;
            parent[start] = start;
            distance[start] = 0;

            while (q.Count > 0)
            {
                int now = q.Dequeue();
                Console.WriteLine(now);

                for (int next = 0; next < 6;next++)
                {
                    //인접하지 않았으면 스킵
                    if (adj[now, next] == -1)
                        continue;

                    //이미 발견했다면 스킵
                    if (found[next])
                        continue;

                    q.Enqueue(next);
                    found[next] = true;
                    parent[next] = now;
                    distance[next] = distance[now] + 1;
                }
            }

        }

        public void Dijikstra(int start)
        {
            bool[] visited = new bool[6];
            int[] distance = new int[6];    //최단거리
            int[] parent = new int[6];
            Array.Fill(distance, int.MaxValue); //distance를 0으로 초기화하면 실제거리 0과 헷갈림

            distance[start] = 0;
            parent[start] = start;

            while (true)
            {
                //제일 좋은 후보를 찾는다 (가장 가까이에 있는)

                //가장 유력한 후보의 거리와 번호를 저장한다.
                int closest = int.MaxValue;
                int now = -1;

                for (int i = 0;i<6;i++)
                {
                    if (visited[i])
                        continue;

                    //아직 발견된 적이 없거나, 기존 후보보다 멀리 있으면 스킵
                    if (distance[i] == int.MaxValue || distance[i] >= closest)
                        continue;

                    //가장 좋은 후보. 정보 갱신
                    closest = distance[i];
                    now = i;
                }

                // 다음 후보가 하나도 없다 -> 종료. 모든점을 다 찾았거나, 연결이 단절되거나
                if (now == -1)
                    break;

                //제일 좋은 후보를 찾았으니 방문
                visited[now] = true;

                //방문한 정점과 인접한 정점들을 조사해서, 상황에 따라 발견한 최단거리를 갱신
                for (int next = 0; next < 6; next++)
                {
                    //연결되지 않은 정점 스킵
                    if (adj[now, next] == -1)
                        continue;

                    //이미 방문한 정점은 스킵
                    if (visited[next])
                        continue;
                    
                    //새로 조사된 정점의 최단거리를 계산
                    int nextDist = distance[now] + adj[now, next];
                    //기존에 발견한 최단거리가 새로 조사된 최단거리보다 크면, 정보를 갱신
                    if(nextDist < distance[next])
                    {
                        distance[next] = nextDist;
                        parent[next] = now;
                    }
                }
            }
        }
    }

    class Program
    {       
        static void GraphDemo()
        {

            // DFS (Depth First Search : 깊이 우선 탐색)
            // BFS (Breadth First Search : 너비 우선 탐색)
            Graph graph = new Graph();
            graph.Dijikstra(0);
        }

        static TreeNode<string> MakeTree()
        {
            TreeNode<string> root = new TreeNode<string>() { Data = "R1 개발실" };
            {
                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "디자인팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "전투" });
                    node.Children.Add(new TreeNode<string>() { Data = "경제" });
                    node.Children.Add(new TreeNode<string>() { Data = "스토리" });
                    root.Children.Add(node);
                }

                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "프로그래밍팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "서버" });
                    node.Children.Add(new TreeNode<string>() { Data = "클라" });
                    node.Children.Add(new TreeNode<string>() { Data = "엔진" });
                    root.Children.Add(node);
                }

                {
                    TreeNode<string> node = new TreeNode<string>() { Data = "아트팀" };
                    node.Children.Add(new TreeNode<string>() { Data = "배경" });
                    node.Children.Add(new TreeNode<string>() { Data = "캐릭터" });
                    root.Children.Add(node);
                }
            }

            return root;
        }

        //재귀함수
        static void PrintTree(TreeNode<string> root)
        {
            Console.WriteLine(root.Data);
            foreach (var child in root.Children)
                PrintTree(child);
        }

        //트리 높이
        static int GetHeight(TreeNode<string> root)
        {
            int height = 0;

            foreach (var child in root.Children)
            {
                int newHeight = GetHeight(child) + 1;
                height = Math.Max(height, newHeight);
            }               

            return height;
        }

        static void TreeDemo()
        {
            TreeNode<string> tree = MakeTree();
            PrintTree(tree);
            Console.WriteLine(GetHeight(tree));
        }

        static void Main(string[] args)
        {
            TreeDemo();
        }
    }
}