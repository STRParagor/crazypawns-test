using System.Collections.Generic;
using UnityEngine;

namespace CrazyPawns.Checkerboard
{
    public static class CheckerboardGenerator
    {
        public static Checkerboard Generate(CheckerboardGeneratorArgs args)
        {
            var boardName = $"Chessboard [{args.BoardSize}x{args.BoardSize}]";
            var checkerboardGO = new GameObject(boardName);
            var checkerboard = checkerboardGO.AddComponent<Checkerboard>();
            var meshFilter = checkerboardGO.AddComponent<MeshFilter>();
            var meshRenderer = checkerboardGO.AddComponent<MeshRenderer>();

            meshFilter.mesh = GenerateMesh(args.BoardSize, args.CellSize);
            meshRenderer.materials = new Material[]
            {
                args.WhiteCellMaterial,
                args.BlackCellMaterial,
            };

            checkerboard.Initialize(meshRenderer);

            return checkerboard;
        }

        private static Mesh GenerateMesh(int boardSize, float cellSize)
        {
            boardSize = Mathf.Max(1, boardSize);
            cellSize = Mathf.Max(0.01f, cellSize);

            var mesh = new Mesh
            {
                name = $"Checkerboard {boardSize}x{boardSize}"
            };

            mesh.CreateVertices(boardSize, cellSize);
            mesh.CreateTriangles(boardSize);
            mesh.RecalculateBounds();

            return mesh;
        }

        private static void CreateVertices(this Mesh mesh, int boardSize, float cellSize)
        {
            var vertexCount = (boardSize + 1) * (boardSize + 1);
            var vertices = new Vector3[vertexCount];
            var normals = new Vector3[vertexCount];
            var uv = new Vector2[vertexCount];

            var halfBoardSize = (boardSize * cellSize) * 0.5f;
            var index = 0;

            for (var y = 0; y <= boardSize; y++)
            {
                for (var x = 0; x <= boardSize; x++)
                {
                    var position = new Vector3
                    {
                        x = x * cellSize - halfBoardSize,
                        z = y * cellSize - halfBoardSize
                    };
                    
                    var uv0 = new Vector2()
                    {
                        x = (float)x / boardSize,
                        y = (float)y / boardSize
                    };

                    vertices[index] = position;
                    normals[index] = Vector3.up;
                    uv[index++] = uv0;
                } 
            }
            
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uv;
        }
        
        private static void CreateTriangles(this Mesh mesh, int boardSize)
        {
            var whiteTriangles = new List<int>();
            var blackTriangles = new List<int>();

            var verticesPerRow = boardSize + 1;
            
            for (var y = 0; y < boardSize; y++)
            {
                for (var x = 0; x < boardSize; x++)
                {
                    var bottomLeft = y * verticesPerRow + x;
                    var bottomRight = bottomLeft + 1;
                    var topLeft = bottomLeft + verticesPerRow;
                    var topRight = topLeft + 1;

                    var isWhiteCell = (x + y) % 2 == 0;

                    var currentTriangles = isWhiteCell ? whiteTriangles : blackTriangles;

                    currentTriangles.Add(bottomLeft);
                    currentTriangles.Add(topLeft);
                    currentTriangles.Add(topRight);
                    
                    currentTriangles.Add(bottomLeft);
                    currentTriangles.Add(topRight);
                    currentTriangles.Add(bottomRight);
                } 
            }
            
            mesh.subMeshCount = 2;
            mesh.SetTriangles(whiteTriangles, 0);
            mesh.SetTriangles(blackTriangles, 1);
        }
    }
}