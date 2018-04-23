using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ChessBoardManager : MonoBehaviour {

	public static ChessBoardManager Instance {set;get;}
	public CinemachineVirtualCamera vCamWhite;
	private bool[,] allowedMoves {set;get;}
	public Chessman[,] chessmans;
	public Chessman selectedChessman;
	private const float TILE_SIZE = 1.0f;
	private const float TILE_OFFSET = 0.5f;

	private int selectionX = -1;
	private int selectionY = -1;

	private Material prevMat;
	public Material selectedMat;

	public Transform parentChessmans;
	public bool isWhiteTurn = true;
	public bool check = false;
	public List<GameObject> chessmanPrefabs;
	private List <GameObject> activeChessman = new List<GameObject> ();

	private void Start () {
		spawnAllChessmen ();
		Instance = this;
	}

	private void Update () {
		UpdateSelection ();
		DrawChessBoard ();

		if (Input.GetMouseButtonDown(0)) {
			if (selectionX >= 0 && selectionY >= 0) {
				if (selectedChessman == null) {
					//Select chessman
					SelectChessman(selectionX, selectionY);
				} else {
					//Move chessman
					MoveChessman(selectionX, selectionY);
				}
			}
		}
	}

	private void SelectChessman(int x, int y) {
		//Check if there's nothing
		if (chessmans[x,y] == null) {
			return;
		}

		//Check Chessman on the right turn
		if (chessmans[x,y].isWhite != isWhiteTurn) {
			return;
		}

		//Force Player to move his King
		if (check && chessmans[x,y].GetType() != typeof(King)) {
			print("Move your King");
			return;
		}

		//Get the all possible movements
		allowedMoves = chessmans[x,y].PossibleMove();
		
		//Check has at least have one move
		bool hasAtLeastOneMove = false;
		allowedMoves = chessmans[x,y].PossibleMove();
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 8; j++)
			{
				if (allowedMoves[i,j])
					hasAtLeastOneMove = true;
			}
		}
		
		if (!hasAtLeastOneMove)
			return;

		//Select Chessman
		selectedChessman = chessmans[x,y];
		// prevMat = selectedChessman.GetComponent<MeshRenderer>().material;
		// selectedMat.mainTexture = prevMat.mainTexture;
		// selectedChessman.GetComponent<MeshRenderer>().material = selectedMat;
		BoardHighlight.Instance.HighlightAllowedMoves(allowedMoves);
	}

	//Move the Chessman to selected position
	private void MoveChessman(int x, int y) {
		if (allowedMoves[x, y]) {
			Chessman c = chessmans[x, y];
			if (c != null && c.isWhite != isWhiteTurn) {
				//If King captured
				if (c.GetType() == typeof(King)) {
					//End the game
					if (c.isWhite) {
						print ("Black team win the game");
					} else {
						print ("White team win the game");						
					}
				}
				//Capture a piece
				activeChessman.Remove(c.gameObject);
				Destroy(c.gameObject);
			}

            if (selectedChessman.GetType () == typeof(Pawn))
            {
                if (y == 7)
                {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    spawnChessman(1, x, y, Quaternion.Euler(0, 90f, 0));
                    selectedChessman = chessmans[x, y];
                } else if (y == 0) {
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);
                    spawnChessman(7, x, y, Quaternion.Euler(0, -90f, 0));
                    selectedChessman = chessmans[x, y];
                }
            }

			chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
			selectedChessman.transform.position = GetTileCenter(x, y);
			selectedChessman.SetPosisiton(x,y);
			chessmans[x, y] = selectedChessman;
			isWhiteTurn = !isWhiteTurn;

			if (isWhiteTurn)
				vCamWhite.gameObject.SetActive(true);
			else
				vCamWhite.gameObject.SetActive(false);
		}
		BoardHighlight.Instance.HideHighlights();
		selectedChessman = null;
	}

	private void spawnChessman (int index, int x, int y, Quaternion rotation) {
		GameObject go = Instantiate (chessmanPrefabs[index], GetTileCenter(x,y), rotation, parentChessmans) as GameObject;
		go.transform.SetParent (parentChessmans);
		chessmans[x,y] = go.GetComponent<Chessman>();
		chessmans[x,y].SetPosisiton(x,y);
		activeChessman.Add (go);
	}

	private void spawnAllChessmen () {
		
		activeChessman = new List<GameObject>();
		chessmans = new Chessman[8,8];
		//Spawn White Team

		//King
		spawnChessman (0, 3, 0, Quaternion.Euler(0, 90f, 0));
	
		//Queen
		spawnChessman (1, 4, 0, Quaternion.Euler(0, 90f, 0));

		//Bishops
		spawnChessman (2, 2, 0, Quaternion.Euler(0, 90f, 0));
		spawnChessman (2, 5, 0, Quaternion.Euler(0, 90f, 0));

		//Knights
		spawnChessman (3, 1, 0, Quaternion.Euler(0, 90f, 0));
		spawnChessman (3, 6, 0, Quaternion.Euler(0, 90f, 0));

		//Rooks
		spawnChessman (4, 0, 0, Quaternion.Euler(0, 90f, 0));
		spawnChessman (4, 7, 0, Quaternion.Euler(0, 90f, 0));

		//Pawns
		for (int i = 0; i < 8; i++)
			spawnChessman (5, i, 1, Quaternion.Euler(0, 90f, 0));

		//Spawn Black Team

		//King
		spawnChessman (6, 3, 7, Quaternion.Euler(0, -90f, 0));

		//Queen
		spawnChessman (7, 4, 7, Quaternion.Euler(0, -90f, 0));

		//Bishops
		spawnChessman (8, 2, 7, Quaternion.Euler(0, -90f, 0));
		spawnChessman (8, 5, 7, Quaternion.Euler(0, -90f, 0));

		//Knights
		spawnChessman (9, 1, 7, Quaternion.Euler(0, -90f, 0));
		spawnChessman (9, 6, 7, Quaternion.Euler(0, -90f, 0));

		//Rooks
		spawnChessman (10, 0, 7, Quaternion.Euler(0, -90f, 0));
		spawnChessman (10, 7, 7, Quaternion.Euler(0, -90f, 0));

		//Pawns
		for (int i = 0; i < 8; i++)
			spawnChessman (11, i, 6, Quaternion.Euler(0, -90f, 0));

	}

	private Vector3 GetTileCenter (int x, int y) {
	
		Vector3 origin = Vector3.zero;
		origin.x += (TILE_SIZE * x) + TILE_OFFSET;
		origin.z += (TILE_SIZE * y) + TILE_OFFSET;
		return origin;

	}

	private void UpdateSelection () {
	
		if (!Camera.main)
			return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hit, 25.0f, LayerMask.GetMask ("ChessPlane"))) {
		
			selectionX = (int)hit.point.x;
			selectionY = (int)hit.point.z;
		
		} else {
		
			selectionX = -1;
			selectionY = -1;
		
		}
	
	}

	private void DrawChessBoard () {
		Vector3 widthLine = Vector3.right * 8;
		Vector3 heightLine = Vector3.forward * 8;

		for (int i = 0; i <= 8; i++) {

			Vector3 start = Vector3.forward * i;
			Debug.DrawLine (start, start + widthLine);

			for (int j = 0; j <= 8; j++) {

				start = Vector3.right * j;
				Debug.DrawLine (start, start + heightLine);
			
			}

		}

		//Draw Selections
		if (selectionX >= 0 && selectionY >= 0) {

			Debug.DrawLine (
				Vector3.forward * selectionY + Vector3.right * selectionX,
				Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1));

			Debug.DrawLine (
				Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
				Vector3.forward * selectionY + Vector3.right * (selectionX + 1));

		}

	}

}
