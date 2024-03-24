# Rubik's Cube Solver

This repository contains a program in C# for solving the Rubik's Cube. The program is designed to be run via the console.

## Usage

1. Clone this repository to your machine.
2. Navigate to the `Rubik's Cube Console` directory.
3. Run the `Program.cs` file.

### Available Options

When you run `Program.cs`, you have three choices:

1. **Generate a random cube scramble:**  
   Type `1` in the terminal to generate a random cube scramble. You will also be prompted to provide the number of moves in the scramble you desire. For example, if you enter `500`, the cube will be well scrambled, while entering `3` will result in less scrambling.

2. **Enter your own cube scramble:**  
   You can enter your own cube scramble to solve. Each line should represent a single move. For example: <br />
R <br />
F <br />
B' <br />
U <br />
...

Use `0` or leave the line blank to validate the scramble.

3. **Enter a custom Rubik's Cube:**  
You can directly enter a Rubik's Cube configuration. The order of the faces does not matter. Each face should be represented as a sequence of 9 colors, where the colors are denoted by the first letter of their English names (Y for Yellow, W for White, B for Blue, O for Orange, G for Green, R for Red). For example:

YOORWYOGWOYWWYWWBRBORORYBORRGWRGBOGYBBYGBWGBYGWBROYGRG

Pay attention to the white and yellow faces; the other four faces are identical, with this order :
![image](https://github.com/Wronskode/Rubik-s-cube-solver/assets/142849734/3d6a8e06-f8df-4901-bcf5-26ff7947201e)
![image](https://github.com/Wronskode/Rubik-s-cube-solver/assets/142849734/39e0516b-400f-4ce7-a232-c72945366a3f)

This project is licensed under the MIT License. See the `LICENSE` file for details.
