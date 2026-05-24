# OOP Board Games - IFN 584 Group Project

Welcome to the **OOP Board Games**, a modular and extensible console-based implementation of five two-player board games- developed by Team for **IFN 584: Object-Oriented Programming** at QUT.

---

## Project Overview

This framework helps multiple two-player board games in a single unified system.

It was designed with extensibility in mind, applying object-oriented design principles and patterns to allow new games to be added with minimal changes to existing code.

---

## Features

- Supports multiple games (Tic-Tac-Toe, Gomoku, Connect Four, etc.)

- Human vs Human and Human vs Computer modes

- Undo/redo functionality

- Save/load game state (.txt and .json)

---

## Game Variants Supported

| Game Variant | Description |

| Tic-Tac_Toe | Classic 3\*3 grid; first to get 3 in a row wins |

| Numerical Tic-Tac-Toe | Players place numbers on an n\*n grid; first line summing to target wins |

| Notakto | Both players place X on three 3\*3 boards. |

| Gomoku | 15\*15 board. First to get 5 in a row |

| Connect Four | 6\*7 grid with gravity. First to connect 4 in a row wins|

---

## How to Run

Requirements

.NET 8 SDK or above

No additional external libraries required

1.  Clone the repository

        git clone https://github.com/Juli27co/OOP-BoardGames-Framework.git

2.  Navigate to the project folder

        cd OOP-BoardGames-Framework

3.  Build the project

        dotnet build

4.  Run the project

        dotnet run

---

## How to play

The program will ask whether to start a new game or load a saved game.

For a new game, select a game type and game mode - Human vs Human or Human vs Computer-

Then follow the instructions to enter moves.

---

## Test Data

Data ID Input type Game / area Example input Valid or invalid
TD01 Menu input General menu 1 Valid

TD02 Menu input General menu 2 Valid

TD03 Command input During a turn save Valid

TD04 Command input During a turn undo Valid

TD05 Command input During a turn redo Valid

TD06 Command input During a turn help Valid

TD07 TicTacToe move TicTacToe 1 Valid

TD08 TicTacToe move TicTacToe 5 Valid

TD09 TicTacToe move TicTacToe 9 Valid

TD10 TicTacToe move TicTacToe 0 Invalid

TD11 TicTacToe move TicTacToe 10 Invalid

TD12 Connect Four move Connect Four 1 Valid

TD13 Connect Four move Connect Four 4 Valid

TD14 Connect Four move Connect Four 7 Valid

TD15 Connect Four move Connect Four 8 Invalid

TD16 Connect Four move Connect Four A Invalid

TD17 Gomoku move Gomoku A1 Valid

TD18 Gomoku move Gomoku J8 Valid

TD19 Gomoku move Gomoku O15 Valid

TD20 Gomoku move Gomoku P16 Invalid

TD21 Numerical TicTacToe move Numerical TicTacToe 5,3 Valid

TD22 Numerical TicTacToe move Numerical TicTacToe 4,2 Valid/Invalid by player

TD23 Numerical TicTacToe move Numerical TicTacToe 1,1 Valid

TD24 Numerical TicTacToe move Numerical TicTacToe 20,3 Invalid

TD25 Notakto move Notakto 1,5 Valid

TD26 Notakto move Notakto 2,3 Valid

TD27 Notakto move Notakto 3,9 Valid

TD28 Notakto move Notakto 4,1 Invalid

TD29 Notakto move Notakto 1,10 Invalid

TD30 Unexpected text Any game abc Invalid

TD31 Unexpected symbol Any game @ Invalid

TD32 Blank input Any game Invalid

TD33 Repeated move Any game same cell twice Invalid

---
