using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman {

    public override bool[,] PossibleMove() {
        
        bool[,] r = new bool[8,8];
        Chessman c;
        int i;
        int j;

        //Right
        i = CurrentX;
        while(true) {
            i++;
            if (i >= 8 || i == CurrentX + 2) {
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
            if (i < 0 || i ==  CurrentX - 2) {
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
            if (i >= 8 || i == CurrentY + 2) {
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
            if (i < 0 || i == CurrentY - 2) {
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
        
        //UpLeft
        i = CurrentX;
        j = CurrentY;
        while (true) {
            i--;
            j++;
            if ((i < 0 || j >= 8) || (i == CurrentX - 2 || j == CurrentY + 2))
                break;
            
            c = ChessBoardManager.Instance.chessmans[i, j];
            if (c == null) {
                r[i, j] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, j] = true;
                }
                break;
            }
        }

        //UpRight
        i = CurrentX;
        j = CurrentY;
        while (true) {
            i++;
            j++;
            if ((i >= 8 || j >= 8) || (i == CurrentX + 2 || j == CurrentY + 2))
                break;
            
            c = ChessBoardManager.Instance.chessmans[i, j];
            if (c == null) {
                r[i, j] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, j] = true;
                }
                break;
            }
        }
        
        //DownLeft
        i = CurrentX;
        j = CurrentY;
        while (true) {
            i--;
            j--;
            if ((i < 0 || j < 0) || (i == CurrentX - 2 || j == CurrentY - 2))
                break;
            
            c = ChessBoardManager.Instance.chessmans[i, j];
            if (c == null) {
                r[i, j] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, j] = true;
                }
                break;
            }
        }

        //DownRight
        i = CurrentX;
        j = CurrentY;
        while (true) {
            i++;
            j--;
            if ((i >= 8 || j < 0) || (i == CurrentX + 2 || j == CurrentY - 2))
                break;
            
            c = ChessBoardManager.Instance.chessmans[i, j];
            if (c == null) {
                r[i, j] = true;
            } else {
                if (c.isWhite != isWhite) {
                    r[i, j] = true;
                }
                break;
            }
        }
        
        return r;

    }
	
    // public bool CheckKing() {

    //     bool check = false;

        

    //     return check;

    // }

}
