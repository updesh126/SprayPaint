using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
//using UnityEditor.Rendering.LookDev;

//public enum Painter_BrushMode{PAINT,DECAL};
public class SprayTexturePainter : MonoBehaviour {
	public GameObject brushCursor,brushContainer; 
    public GameObject rcontroller;
	public ParticleSystem particleSystem;
	//public XRController controller;
	public Animator animator;
	public AudioSource audio;
	//public ActionBasedController controller;
    public InputActionProperty pinchAnimationAction;
    public Camera sceneCamera,canvasCam;  
	public Sprite cursorPaint,cursorDecal; 
	public RenderTexture canvasTexture; 
	public Material baseMaterial; 
	//public FlexibleColorPicker colorPicker;

	bool isgrabbed = true;
	bool isAudioPlayed = false;

	Painter_BrushMode mode; 
	public float brushSize=.1f; 
	Color brushColor; 
	int brushCounter=0,MAX_BRUSH_COUNT=1000; 
	bool saving=false;

    #region Unity Default
    private void Start()
    {
		SetBrushSize(.03f);
    }

    void Update () {
		//brushColor = colorPicker.color;
		//var inputDevices = new List<UnityEngine.XR.InputDevice>();
		//UnityEngine.XR.InputDevices.GetDevices(inputDevices);
		//float triggerValue = pinchAnimationAction.action.ReadValue<float>();
		if (isgrabbed== true)
		{
			if (pinchAnimationAction.action.triggered && !audio.isPlaying)
			{
				audio.Play();
			}
			if (pinchAnimationAction.action.ReadValue<float>() > 0.3f && pinchAnimationAction.action.ReadValue<float>() < 0.5f)
            {
				
                particleSystem.Play();
                
                Debug.Log(pinchAnimationAction.action.ReadValue<float>());

                DoAction(0.2f);
                animator.SetBool("isSpray", true);

            }
            else if (pinchAnimationAction.action.ReadValue<float>() >= 0.5f && pinchAnimationAction.action.ReadValue<float>() < 0.7f)
            {

                
                particleSystem.Play();
                

                DoAction(0.5f);
                animator.SetBool("isSpray", true);

            }
            else if (pinchAnimationAction.action.ReadValue<float>() >= 0.7f)
            {    

                

                particleSystem.Play();
                

                DoAction(1f);
                animator.SetBool("isSpray", true);

            }

            else
            {
                particleSystem.Stop();
                audio.Stop();
				isAudioPlayed = false;

                animator.SetBool("isSpray", false);

            }


        }
        //particleSystem.Stop();
        //if (Input.GetMouseButton(0)) {

        //}
        UpdateBrushCursor ();
    }

    #endregion


    #region Unity Custom Functions
    public void IsGunGrab(bool isgrab)
	{
		isgrabbed = isgrab;

		Debug.Log("isplay");
    }

	public void SelectColor(Color color)
	{
        brushColor=color;

    }
    #endregion

    #region Unity Textures
    public void DoAction(float max){	
		if (saving)
			return;
		Vector3 uvWorldPosition=Vector3.zero;		
		if(HitTestUVPosition(ref uvWorldPosition)){
			GameObject brushObj;
            //brushColor.a = brushSize * max *23; // Brushes have alpha to have a merging effect when painted over.

            brushObj = (GameObject)Instantiate(Resources.Load("TexturePainter-Instances/BrushEntity")); //Paint a brush
			brushObj.GetComponent<SpriteRenderer>().color=brushColor; //Set the brush color

            //brushColor.a = .1f;
			brushObj.transform.parent=brushContainer.transform; //Add the brush to our container to be wiped later
			brushObj.transform.localPosition=uvWorldPosition; //The position of the brush (in the UVMap)
            float zRotation = rcontroller.transform.localRotation.eulerAngles.z;
            brushObj.transform.localRotation= Quaternion.Euler(0f, 0f, zRotation); ;
			brushObj.transform.localScale=Vector3.one*brushSize;//The size of the brush

			
		}
		brushCounter++; 
		if (brushCounter >= MAX_BRUSH_COUNT) { 
			brushCursor.SetActive (false);
			saving=true;
            //Invoke("SaveTexture",0.1f);
            brushCounter = 0;

            Invoke("ShowCursor", 0.1f);
        }
    }
	
	void UpdateBrushCursor(){
		Vector3 uvWorldPosition=Vector3.zero;
		if (HitTestUVPosition (ref uvWorldPosition) && !saving) {
			brushCursor.SetActive(true);
			brushCursor.transform.position =uvWorldPosition+brushContainer.transform.position;									
		} else {
			brushCursor.SetActive(false);
		}		
	}
	
	bool HitTestUVPosition(ref Vector3 uvWorldPosition){
		RaycastHit hit;
		//Vector3 cursorPos = new Vector3 (rcontroller.transform.position.x, rcontroller.transform.position.y, 0.0f);
		//Ray cursorRay=sceneCamera.ScreenPointToRay (cursorPos);


		Ray cursorRay = new Ray(rcontroller.transform.position, rcontroller.transform.forward);
		Debug.DrawRay(rcontroller.transform.position, rcontroller.transform.up);
		if (Physics.Raycast(cursorRay,out hit,3)){
			MeshCollider meshCollider = hit.collider as MeshCollider;
			if (meshCollider == null || meshCollider.sharedMesh == null)
				return false;			
			Vector2 pixelUV  = new Vector2(hit.textureCoord.x,hit.textureCoord.y);
			uvWorldPosition.x=pixelUV.x-canvasCam.orthographicSize;
			uvWorldPosition.y=pixelUV.y-canvasCam.orthographicSize;
			uvWorldPosition.z=0.0f;
			return true;
		}
		else{		
			return false;
		}
		
	}
	
	void SaveTexture(){		
		brushCounter=0;
		System.DateTime date = System.DateTime.Now;
		RenderTexture.active = canvasTexture;
		Texture2D tex = new Texture2D(canvasTexture.width, canvasTexture.height, TextureFormat.RGB24, false);		
		tex.ReadPixels (new Rect (0, 0, canvasTexture.width, canvasTexture.height), 0, 0);
		tex.Apply ();
		RenderTexture.active = null;
		baseMaterial.mainTexture =tex;	
		foreach (Transform child in brushContainer.transform) {
			Destroy(child.gameObject);
		}
		//StartCoroutine ("SaveTextureToFile");
		Invoke ("ShowCursor", 0.1f);
	}
	public void clear()
	{
        foreach (Transform child in brushContainer.transform)
        {
            Destroy(child.gameObject);
        }
    }
	void ShowCursor(){	
		saving = false;
	}



	public void SetBrushMode(Painter_BrushMode brushMode){ 
		mode = brushMode;
		brushCursor.GetComponent<SpriteRenderer> ().sprite = brushMode == Painter_BrushMode.PAINT ? cursorPaint : cursorDecal;
	}
	public void SetBrushSize(float newBrushSize){ 
		brushSize = newBrushSize;
		brushCursor.transform.localScale = Vector3.one * brushSize;
	}
    #endregion


#if !UNITY_WEBPLAYER
    IEnumerator SaveTextureToFile(Texture2D savedTexture){		
			brushCounter=0;
			string fullPath=System.IO.Directory.GetCurrentDirectory()+"\\UserCanvas\\";
			System.DateTime date = System.DateTime.Now;
			string fileName = "CanvasTexture.png";
			if (!System.IO.Directory.Exists(fullPath))		
				System.IO.Directory.CreateDirectory(fullPath);
			var bytes = savedTexture.EncodeToPNG();
			System.IO.File.WriteAllBytes(fullPath+fileName, bytes);
			Debug.Log ("<color=orange>Saved Successfully!</color>"+fullPath+fileName);
			yield return null;
		}
	#endif
}
