using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlyphDrawer : MonoBehaviour {

    public GameObject dot;
		public Vector3[] coorList;
		public float scale = 0.25f;
		public float lineWidth = 0.02f;
    public Material firstDotMaterial;
    
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
      if(coorList.Length>0){
        GameObject fd = dotList[0];
        fd.GetComponent<MeshRenderer>().material = firstDotMaterial;
      }
			lineRenderer.positionCount = coorList.Length;
			lineRenderer.SetPositions(coorList);
    }

    //This is what draws a dot/line
    void AddDot(Vector3 vec)
    {
				GameObject d = Instantiate(dot) as GameObject;
				d.transform.parent = transform;
				d.transform.localPosition = vec;
        dotList.Add(d);
    }
}
