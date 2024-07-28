using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Puzzle3Manager : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;
    
    // Create the game setup with size x size pieces
    private void CreateGamePieces(float gapThickness){
        // Set the width of each tile
        float width = 1 / (float)size;
        for (int row = 0; row < size; row++) {
            for (int col = 0; col < size; col ++) {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);
                // Pieces will be in a game board going from -1 to +1
                piece.localPosition = new Vector3 (-1 + (2 * width * col) + width,
                                                    1 - (2 * width * row) - width,
                                                    0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";
                // We want an empty space in the bottom right
                if ((row == size - 1) && (col == size - 1)) {
                    emptyLocation = size * size - 1;
                    piece.gameObject.SetActive(false);
                } else {
                    // To change the quad texture with subcomponents of the picture
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                    Vector2[] uv = new Vector2[4];
                    uv[0] = new Vector2(width * col + gap, 1 - (width * (row + 1) - gap));
                    uv[1] = new Vector2(width * (col + 1) - gap, 1 - (width * (row + 1) - gap));
                    uv[2] = new Vector2(width * col + gap, 1 - (width * row + gap));
                    uv[3] = new Vector2((width * (col + 1)) + gap, 1 - (width * row - gap));
                    mesh.uv = uv;

                }

            }
        }

    }
    void Start()
    {
        pieces = new List<Transform>();
        size = 4;
        CreateGamePieces(0.01f);
        Shuffle();

    }

    // Update is called once per frame
    void Update()
    {
        // On click send out ray to see if we click a piece
        if (Input.GetMouseButtonDown(0)) {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit) {
                // Go through the list, the index tells us the position
                for (int i = 0; i < pieces.Count; i++) {
                    if (pieces[i] == hit.transform) {
                        // Check each direction to see if valid move
                        // break out on success so we don't carry on and swap back again
                        if (SwapIfValid(i, -size, size)) {break;}
                        if (SwapIfValid(i, +size, size)) {break;}
                        if (SwapIfValid(i, -1, 0)) {break;}
                        if (SwapIfValid(i, +1, size - 1)) {break;}
                    }
                }
            }
            // Check for completion after each move
            if (!shuffling && CheckCompletion()) {
                shuffling = true;
                PuzzleManager.Instance.SetPuzzleCompleted("Puzzle3");
                // Scene switch back the main game after completing the puzzle
                SceneTransitionManager.Instance.TransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

    }

    private bool SwapIfValid(int i, int offset, int colCheck) {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation)) {
            // Swap them in game state
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);
            // Swap their transforms
            (pieces[i].localPosition, pieces[i + offset].localPosition) = (pieces[i + offset].localPosition, pieces[i].localPosition);
            // Update empty location
            emptyLocation = i;
            return true;
        }
        return false;
    }

    private bool CheckCompletion() {
        for (int i = 0; i < pieces.Count; i++) {
            if (pieces[i].name != $"{i}") {
                return false;
            }
        }
        return true;
    }
     
     // Brute force shuffling
    private void Shuffle() {
        int count = 0;
        int last = 0;
        while (count < (size * size * size)) {
            // Pick a random location
            int rnd = Random.Range(0, size * size);
            // Only thing we forbid is undoing the last move
            if (rnd == last) { continue; }
            last = emptyLocation;
            // Try surrounding spaces looking for valid move
            if (SwapIfValid(rnd, -size, size)) {
                count++;
            } else if (SwapIfValid(rnd, +size, size)) {
                count++;
            } else if (SwapIfValid(rnd, -1, 0)) {
                count++;
            } else if (SwapIfValid(rnd, +1, size - 1)) {
                count++;
            }
        }
    }
}
