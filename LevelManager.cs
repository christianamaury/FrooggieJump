using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public static LevelManager Instance { set; get; }

    //It will spawn.. certain colliders bro lol
    private const bool SHOW_COLLIDER = true;

    //Level Spawning.. how far we should be able to see.. Before 30.0f;
    private const float DISTANCE_BEFORE_SPAWN = 30.0f;

    //How many segments we would like to have when the game start.8
    private const int INITIAL_SEGMENTS = 5;

    //Initial Transition..Puedo poner 2
    private const int INITIAL_TRANSITION_SEGMENTS = 1;

    //Max segments on screen... Before 10
    private const int MAX_SEGMENTS = 10;

    //Where exactly we're on the map.. 
    private Transform cameraContainer;
    private int ammountOfActiveSegments;
    private int continuosSeg;

    //Our current location.. 
    private int currentSpawnZ;

    //What current level we're.. 
    private int currentLevel;

    private int y1, y2, y3;

    //List of pieces..
    public List<Piece> smallRock = new List<Piece>();
    public List<Piece> smallCortez = new List<Piece>();
    public List<Piece> rock = new List<Piece>();
    public List<Piece> smallPlant = new List<Piece>();
    public List<Piece> cortez = new List<Piece>();
    public List<Piece> sandRock = new List<Piece>();

    //All the pieces in the pool..
    [HideInInspector]
    public List<Piece> pieces = new List<Piece>();

    //List of Segments.. 
    public List<Segment> availableSegments = new List<Segment>();
    public List<Segment> availableTransitions = new List<Segment>();

    [HideInInspector]
    public List<Segment> segments = new List<Segment>();

    //Gameplay.. 
    private bool isMoving = false;

    private void Awake()
    {
        //Test..
        //DontDestroyOnLoad(gameObject);

        Instance = this;
        cameraContainer = Camera.main.transform;

        //Starting everything fresh.. 
        currentSpawnZ = 0;
        currentLevel = 0;
    }

    //Just after the awake call.. 
    private void Start()
    {
        //If less than the initial ammount of segments.. 
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
        {
            //If it less than.. TESTING THIS SECTION
            if(i < INITIAL_TRANSITION_SEGMENTS)
            {
                SpawnTransition();
            }
            else
            {
                //Generate a couple segments.. 
                GenerateSegment();
            }

        }
    }

    private void Update()
    {
        //If the player is too close, respawn another segment.. 
        if(currentSpawnZ - cameraContainer.position.z < DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
        }

        //Despawning platforms.. 
        if(ammountOfActiveSegments >= MAX_SEGMENTS)
        {
            //Index 0
            segments[ammountOfActiveSegments - 1].DeSpawn();
            //Ammount of segments at this point.. 
            ammountOfActiveSegments--;
        }

    }

    private void GenerateSegment()
    {
        SpawnSegment();

        //Random range.. 
        if(Random.Range(0f, 1f) < (continuosSeg * 0.25f))
        {
            continuosSeg = 0;
            SpawnTransition();
        }
        else
        {
            continuosSeg++;
        }

    }

    private void SpawnSegment()
    {
        List<Segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleSeg.Count);

        Segment s = GetSegment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;

        s.transform.SetParent(transform);

        //Spawning it in from of the player.. 
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        //Meters of the GameObject.. Length of this object
        currentSpawnZ += s.lenght;

        ammountOfActiveSegments++;
        s.Spawn();

    }

    private void SpawnTransition()
    {
        List<Segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = Random.Range(0, possibleTransition.Count);

        Segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;

        s.transform.SetParent(transform);

        //Spawning it in from of the player.. 
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        //Meters of the GameObject.. Length of this object
        currentSpawnZ += s.lenght;

        ammountOfActiveSegments++;
        s.Spawn();

    }

    public Segment GetSegment(int id, bool transition)
    {
        Segment s = null;
        s = segments.Find(x => x.segId == id && x.transition == transition && !x.gameObject.activeSelf);

        //If its null we have to spawn.. 
        if(s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;

            //Getting the component of it.. 
            s = go.GetComponent<Segment>();

            s.segId = id;
            s.transition = transition;

            segments.Insert(0, s);
        }

        //If we're already have it, we would remove it from the list.. 
        else
        {
            segments.Remove(s);

            //Adding it again, but at the first index.. 
            segments.Insert(0, s);
        }

        return s;
    }

    public Piece GetPiece(PieceType pt, int visualIndex)
    {
        //If we find an object that has all of these.. 
        Piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);
      
        //If we didn't find, we have to spawn it.. 
        if(p == null)
        {
            GameObject go = null;
            if(pt == PieceType.rock)
            {
                go = rock[visualIndex].gameObject;
            }
            else if(pt == PieceType.smallCortez)
            {
                go = smallCortez[visualIndex].gameObject;
            }
           else if(pt == PieceType.smallPlant)
            {
                go = smallPlant[visualIndex].gameObject;
            }
            else if(pt == PieceType.cortez)
            {
                go = cortez[visualIndex].gameObject;
            }
            //Instantiate gameObject.. 
            go = Instantiate(go);

            //Grabbing the piece on top of that object.. 
            p = GetComponent<Piece>();

            //Adding it to the pool..
            pieces.Add(p);
        }
        return p;
    }
}
