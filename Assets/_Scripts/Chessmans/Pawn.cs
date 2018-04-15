using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman {

    public override bool[,] PossibleMove() {
        bool[,] r = new bool[8,8];
        Chessman c, c2;
        
        //White team move
        if (isWhite) {

            //Diagonal Left
            if (CurrentX != 0 && CurrentY != 7) {
                c = ChessBoardManager.Instance.chessmans[CurrentX - 1, CurrentY + 1];
                if (c != null && !c.isWhite) {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }   
            }
            
            //Diagonal Right
            if (CurrentX != 7 && CurrentY != 7) {
                c = ChessBoardManager.Instance.chessmans[CurrentX + 1, CurrentY + 1];
                if (c != null && !c.isWhite) {
                    r[CurrentX + 1, CurrentY + 1] = true;
                }              
            }
            //Middle
            if (CurrentY != 7) {
                c = ChessBoardManager.Instance.chessmans[CurrentX, CurrentY + 1];
                if (c == null) {
                    r[CurrentX, CurrentY + 1] = true;
                }
            }

            //Firts Move 2 Tile
            if (CurrentY == 1) {
                c = ChessBoardManager.Instance.chessmans[CurrentX, CurrentY + 1];
                c2 = ChessBoardManager.Instance.chessmans[CurrentX, CurrentY + 2];
                if (c == null && c2 == null) {
                    r[CurrentX, CurrentY + 2] = true;
                }
            }
        //Black team move
        } else {

            //Diagonal Left
            if (CurrentX != 0 && CurrentY != 0) {
                c = ChessBoardManager.Instance.chessmans[CurrentX - 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX - 1, CurrentY - 1] = true;                
            }
            
            //Diagonal Right
            if (CurrentX != 7 && CurrentY != 0) {
                c = ChessBoardManager.Instance.chessmans[CurrentX + 1, CurrentY - 1];
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;                
            }
            //Middle
            if (CurrentY != 0) {
                c = ChessBoardManager.Instance.chessmans[CurrentX, CurrentY - 1];
                if (c == null) {
                    r[CurrentX, CurrentY - 1] = true;
                }
            }

            //Firts Move 2 Tile
            if (CurrentY == 6) {
                c = ChessBoardManager.Instance.chessmans[CurrentX, CurrentY - 1];
                c2 = ChessBoardManager.Instance.chessmans[CurrentX, CurrentY - 2];
                if (c == null && c2 == null) {
                    r[CurrentX, CurrentY - 2] = true;
                }
            }

        }

        return r;
    }

}
