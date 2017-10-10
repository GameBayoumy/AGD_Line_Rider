using System.Collections.Generic;
using UnityEngine;

public class LineMeshGenerator: MonoBehaviour
{

    // Public variables

    public Material trailMaterial;                  // Material of the trail.  Changing this during runtime will have no effect.
    public float lifeTime = 1.0f;                   // Life time of the trail
    public float changeTime = 0.5f;                 // Time point when the trail begins changing its width (if widthStart != widthEnd)
    public float widthStart = 1.0f;                 // Starting width of the trail
    public float widthEnd = 1.0f;                   // Ending width of the trail
    public float vertexDistanceMin = 0.10f;         // Minimum distance between the center positions
    public Vector3 renderDirection = new Vector3(0, 0, -1); // Direction that the mesh of the trail will be rendered towards
    public bool colliderEnabled = true;             // Determines if the collider is enabled. Changing this during runtime will have no effect.
    public bool pausing = false;                    // Determines if the trail is pausing, that is neither creating nor destroying vertices

    // Private variables

    private Mesh mesh;
    [SerializeField]
    private float meshLength;                        // Length of the mesh
    private new PolygonCollider2D collider;
    private LinkedList<Vector3> centerPositions;    //the previous positions of the object this script is attached to
    private LinkedList<Vertex> leftVertices;        //the left vertices derived from the center positions
    private LinkedList<Vertex> rightVertices;       //the right vertices derived from the center positions

    //************
    //
    // Public Methods
    //
    //************

    /// <summary>
    /// Changes the material of the trail during runtime.
    /// </summary>
    public void ChangeTrailMaterial(Material material)
    {
        trailMaterial = material;
    }

    //************
    //
    // Private Unity Methods
    //
    //************

    private void Awake()
    {
        // Create an object and mesh for the trail
        GameObject trail = new GameObject("Trail", new[] { typeof(MeshRenderer), typeof(MeshFilter), typeof(PolygonCollider2D) });
        transform.SetParent(trail.transform);
        mesh = trail.GetComponent<MeshFilter>().mesh = new Mesh();
        trail.GetComponent<Renderer>().material = trailMaterial;

        // Get and set the polygon collider on this trail.
        collider = trail.GetComponent<PolygonCollider2D>();
        collider.SetPath(0, null);

        // Set the first center position as the current position
        centerPositions = new LinkedList<Vector3>();
        centerPositions.AddFirst(transform.position);

        leftVertices = new LinkedList<Vertex>();
        rightVertices = new LinkedList<Vertex>();
    }

    private void Update()
    {
        if (!pausing)
        {
            // Set the mesh and adjust widths if vertices were added or removed
            if (TryAddVertices() | TryRemoveVertices())
            {
                SetMesh();
                meshLength++;
            }
        }
    }

    //************
    //
    // Private Methods
    //
    //************

    /// <summary>
    /// Adds new vertices if the object has moved more than 'vertexDistanceMin' from the most recent center position.
    /// If a pair of vertices has been added, this method returns true.
    /// </summary>
    private bool TryAddVertices()
    {
        bool vertsAdded = false;

        // Check if the current position is far enough away (> 'vertexDistanceMin') from the most recent position where two vertices were added
        if ((centerPositions.First.Value - transform.position).sqrMagnitude > vertexDistanceMin * vertexDistanceMin)
        {
            // Calculate the normalized direction from the 1) most recent position of vertex creation to the 2) current position
            Vector3 dirToCurrentPos = (transform.position - centerPositions.First.Value).normalized;

            // Calculate the positions of the left and right vertices --> they are perpendicular to 'dirToCurrentPos' and 'renderDirection'
            Vector3 cross = Vector3.Cross(renderDirection, dirToCurrentPos);
            Vector3 leftPos = transform.position + (cross * -widthStart * 0.5f);
            Vector3 rightPos = transform.position + (cross * widthStart * 0.5f);

            // Create two new vertices at the calculated positions
            leftVertices.AddFirst(new Vertex(leftPos, transform.position, (leftPos - transform.position).normalized));
            rightVertices.AddFirst(new Vertex(rightPos, transform.position, (rightPos - transform.position).normalized));

            // Add the current position as the most recent center position
            centerPositions.AddFirst(transform.position);
            vertsAdded = true;
        }

        return vertsAdded;
    }

    /// <summary>
    /// Removes any pair of vertices that have been alive longer than the specified lifespan.
    /// If a pair of vertices have been removed, this method returns true.
    /// </summary>
    private bool TryRemoveVertices()
    {
        bool vertsRemoved = false;
        LinkedListNode<Vertex> leftVertNode = leftVertices.Last;

        // Continue looking at the last left vertex 1) while one exists and 2) while the last left vertex is older than its lifeTime
        while (leftVertNode != null && leftVertNode.Value.TimeAlive > lifeTime)
        {
            // Remove the left vertex from the collection
            leftVertices.RemoveLast();
            leftVertNode = leftVertices.Last;

            // Remove its partnered right vertex from the collection since they were created at the same time.
            rightVertices.RemoveLast();

            // Remove the center position that the two vertices were derived from
            centerPositions.RemoveLast();
            vertsRemoved = true;
        }

        return vertsRemoved;
    }

    /// <summary>
    /// Sets the mesh and the polygon collider of the mesh.
    /// </summary>
    private void SetMesh()
    {
        // Only continue if there are at least two center positions in the collection
        if (centerPositions.Count < 2)
        {
            return;
        }

        // Create an array for the 1) trail vertices, 2) trail uvs, 3) trail triangles, and 4) vertices on the collider path
        Vector3[] vertices = new Vector3[centerPositions.Count * 2];
        Vector2[] uvs = new Vector2[centerPositions.Count * 2];
        int[] triangles = new int[(centerPositions.Count - 1) * 6];
        Vector2[] colliderPath = new Vector2[(centerPositions.Count - 1) * 2];

        LinkedListNode<Vertex> leftVertNode = leftVertices.First;
        LinkedListNode<Vertex> rightVertNode = rightVertices.First;

        // Iterate through all the pairs of vertices (left + right)
        for (int i = 0; i < leftVertices.Count; ++i)
        {
            Vertex leftVert = leftVertNode.Value;
            Vertex rightVert = rightVertNode.Value;

            // Trail vertices
            int vertIndex = i * 2;
            vertices[vertIndex] = leftVert.Position;
            vertices[vertIndex + 1] = rightVert.Position;

            // Collider vertices 
            colliderPath[i] = leftVert.Position;
            colliderPath[colliderPath.Length - (i + 1)] = rightVert.Position;

            // Trail uvs
            uvs[vertIndex] = new Vector2(0, 0);
            uvs[vertIndex + 1] = new Vector2(1, 1);

            // Trail triangles
            if (i > 0)
            {
                int triIndex = (i - 1) * 6;
                triangles[triIndex] = vertIndex - 2;
                triangles[triIndex + 1] = vertIndex - 1;
                triangles[triIndex + 2] = vertIndex + 1;
                triangles[triIndex + 3] = vertIndex - 2;
                triangles[triIndex + 4] = vertIndex + 1;
                triangles[triIndex + 5] = vertIndex;
            }

            // Increment the left and right vertex nodes
            leftVertNode = leftVertNode.Next;
            rightVertNode = rightVertNode.Next;
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        if (colliderEnabled)
        {
            collider.SetPath(0, colliderPath);
        }
    }

    /// <summary>
    /// Gets the length of the mesh.
    /// </summary>
    public float GetLength()
    {
        return meshLength;
    }

    //************
    //
    // Private Classes
    //
    //************

    private class Vertex
    {
        private Vector3 centerPosition; // The center position in the trail that this vertex was derived from
        private Vector3 derivedDirection; // The direction from the 1) center position to the 2) position of this vertex ( A to B )
        private float creationTime;

        public Vector3 Position { get; private set; }
        public float TimeAlive { get { return Time.time - creationTime; } }

        public void AdjustWidth(float width)
        {
            Position = centerPosition + (derivedDirection * width);
        }

        public Vertex(Vector3 position, Vector3 centerPosition, Vector3 derivedDirection)
        {
            this.Position = position;
            this.centerPosition = centerPosition;
            this.derivedDirection = derivedDirection;
            creationTime = Time.time;
        }
    }
}