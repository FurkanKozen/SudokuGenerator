# Sudoku Generator
A command-line application to generate completed sudoku.

This application generates a completed sudoku grid for given table length and block length parameters. It runs on a console window and displays the generated grid and generation times to the user. It doesn't solve given sudoku puzzles.

The table must be in the shape of square and table length should not be a prime number. Block width and block height can be customized provided they are the exact divisor of the table length.

The program can successfully generate sudokus up to 30x30 in a reasonable time (takes ~5s) but the console width can cause an error when the table to be displayed is larger than the window width. If the program throws an error, that probably means it could generate the sudoku grid but it cannot render it to the console. Sudoku generating speed may vary according to the computer's hardware.

You can download the executable file from [here](https://github.com/FurkanKozen/SudokuGenerator/releases).

## To-dos

1. All these to-dos should be moved to the 'Issues'.
1. Console size should not cause an error.
1. `PopImpossibleCells()` method seems to pop too many cells, should be fixed.
1. Live preview of cells (writing and erasing cells when creating and deleting) should be added. Actually this is managed with `Write...()` methods but disabling this feature requires commenting out these methods because conditional expressions before these methods would be executed even when they do not satisfy the condition. So they should not be included in the build if the live preview is not desired because writing to the console at each attempt is making the generating so much slower.
1. Dialogs in interface and comments and exception messages in code should be in English.
1. Refactoring in variable and method names can be considered. Like `Layout` => `Grid`, `Worth` => `Value`, `BlockIndex` => `b`.
1. The process of generating sudoku should be repeatable without restarting the program.
1. The batch generating process should be set and the average generating time should be displayed to the user.
1. The resulting grid can be created to a text file in the disk.
1. A separate validation process must be added to be run after the generating process for only validating the validity of the table because though the generating process also is a validation process, a logic that is only focused on validation would be a more safe way.
1. Linux release files should be provided.
1. The methods in the `Layout` class used to write cells to the console should be moved to another class. And it would be better these methods do not write to console directly but return the values related to visualization like position and color in the console. A function writing all generated grid to the console can be executed asynchronously and recursively in a period of time like 100ms. This method, of course, must clear the grid area in each operation of writing the grid. Otherwise deleted cells because of invalidity would not be erased from the screen. Running the operation of writing the cells to console asynchronously prevents performance issues too because the sudoku generating process uses a single thread.