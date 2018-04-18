using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphDrawer : MonoBehaviour {

    public GameObject dot;
		public Vector3[] coorList;
		public float scale = 0.25f;
		public float lineWidth = 0.02f;

    private List<GameObject> dotList = new List<GameObject>();
    private LineRenderer lineRenderer;

    private void Start()
    {
			//set up the line renderer if it isnt a component already
			lineRenderer = GetComponent<LineRenderer>();
			lineRenderer.widthMultiplier = lineWidth;
			lineRenderer.positionCount = coorList.Length;
			for(int i=0; i<coorList.Length; i++){
				AddDot(coorList[i]*scale);
				coorList[i] *= scale;
			}
			lineRenderer.positionCount = coorList.Length;
			lineRenderer.SetPositions(coorList);
    }

    //Just for testing purposes probably just make a function and have it take in input of an array
    void Update () {
        // if (Input.GetKeyDown(KeyCode.Q))
        // {
        //     AddDot(new Vector3(-1, 1, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.W))
        // {
        //     AddDot(new Vector3(0, 1, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.E))
        // {
        //     AddDot(new Vector3(1, 1, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.A))
        // {
        //     AddDot(new Vector3(-1, 0, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.S))
        // {
        //     AddDot(new Vector3(0, 0, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.D))
        // {
        //     AddDot(new Vector3(1, 0, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.Z))
        // {
        //     AddDot(new Vector3(-1, -1, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.X))
        // {
        //     AddDot(new Vector3(0, -1, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.C))
        // {
        //     AddDot(new Vector3(1, -1, -1));
        // }
        // else if (Input.GetKeyDown(KeyCode.R))
        // {
        //     AddDot(new Vector3(-1, 1, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.T))
        // {
        //     AddDot(new Vector3(0, 1, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.Y))
        // {
        //     AddDot(new Vector3(1, 1, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.F))
        // {
        //     AddDot(new Vector3(-1, 0, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.G))
        // {
        //     AddDot(new Vector3(0, 0, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.H))
        // {
        //     AddDot(new Vector3(1, 0, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.V))
        // {
        //     AddDot(new Vector3(-1, -1, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.B))
        // {
        //     AddDot(new Vector3(0, -1, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.N))
        // {
        //     AddDot(new Vector3(1, -1, 0));
        // }
        // else if (Input.GetKeyDown(KeyCode.U))
        // {
        //     AddDot(new Vector3(-1, 1, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.I))
        // {
        //     AddDot(new Vector3(0, 1, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.P))
        // {
        //     AddDot(new Vector3(1, 1, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.K))
        // {
        //     AddDot(new Vector3(-1, 0, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.L))
        // {
        //     AddDot(new Vector3(0, 0, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.Semicolon))
        // {
        //     AddDot(new Vector3(1, 0, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.Comma))
        // {
        //     AddDot(new Vector3(-1, -1, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.Period))
        // {
        //     AddDot(new Vector3(0, -1, 1));
        // }
        // else if (Input.GetKeyDown(KeyCode.Slash))
        // {
        //     AddDot(new Vector3(1, -1, 1));
				//
        // }
				//
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     Reset();
        // }
    }

    //This is what draws a dot/line
    void AddDot(Vector3 vec)
    {
        // //check if you already have a dot in that position
        // for (int i = 0; i < dotList.Count; ++i)
        // {
        //     if (dotList[i].transform.position == vec)
        //     {
        //         return;
        //     }
        // }
        //if there is no dot at vec position then make one there
				GameObject d = Instantiate(dot) as GameObject;
				d.transform.parent = transform;
				d.transform.localPosition = vec;
        dotList.Add(d);
        //add the dots position to the line renderer
        // lineRenderer.positionCount++;
        // lineRenderer.SetPosition(dotList.Count - 1, vec+transform.position);

    }

    // //This is what clears the dots/lines
    // private void Reset()
    // {
    //     Destroy(gameObject.GetComponent<LineRenderer>());
    //     for (int i = 0; i < dotList.Count; ++i)
    //     {
    //         Destroy(dotList[i]);
    //     }
    //     dotList.Clear();
    // }
		//

    /* TODO: test draw spell function
    private void DrawSpell(vector[] spellCoords) //pass in array of vectors for spell coordinates I dont remember if that was the actual syntax tho
    {
        for(int i = 0; i < spellCoords.length; ++i)
        {
            AddDot(spellCoords[i]);
        }
    }
    */
}
