using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman {

    public override bool[,] PossibleMove() {

        bool[,] r = new bool[8,8];
        int i;
        Chessman c;

        //Right
        i = CurrentX;
        while(true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = ChessBoardManager.Instance.chessmans[i, CurrentY];
            if (c == null) {
                r[i, CurrentY] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i,CurrentY] = true;
                }
                
                if(c.isWhite == isWhite) {
                    r[i,CurrentY] = false;
                }

                break;
            }
        }

        //Left
        i = CurrentX;
        while(true) {
            i--;
            if (i < 0) {
                break;
            }

            c = ChessBoardManager.Instance.chessmans[i, CurrentY];
            if (c == null) {
                r[i, CurrentY] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i,CurrentY] = true;
                }
                
                if(c.isWhite == isWhite) {
                    r[i,CurrentY] = false;
                }

                break;
            }
        }

        //Up
        i = CurrentY;
        while(true) {
            i++;
            if (i >= 8) {
                break;
            }

            c = ChessBoardManager.Instance.chessmans[CurrentX, i];
            if (c == null) {
                r[CurrentX, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[CurrentX,i] = true;
                }
                
                if(c.isWhite == isWhite) {
                    r[CurrentX,i] = false;
                }

                break;
            }
        }

        //Down
        i = CurrentY;
        while(true) {
            i--;
            if (i < 0) {
                break;
            }

            c = ChessBoardManager.Instance.chessmans[CurrentX, i];
            if (c == null) {
                r[CurrentX, i] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[CurrentX,i] = true;
                }
                
                if(c.isWhite == isWhite) {
                    r[CurrentX,i] = false;
                }

                break;
            }
        }

        return r;

    }

}
